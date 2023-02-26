using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Localization;
using Serilog;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.Services.ScheduleServices;
using TechFix.TransportModels.Cache;
using TechFix.Services.EmailServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TechFix.EntityModels.Persistence;

namespace TechFix.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<DataContext>();
            services.AddHangfire(config => config.UseMemoryStorage());
            services.AddRazorPages();
            services.AddCors();

            services.Configure<SendGridConfig>(_configuration.GetSection("SendGridConfig"));

            //services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddControllers().AddNewtonsoftJson(opts => opts.SerializerSettings.ContractResolver = new DefaultContractResolver());
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddStackExchangeRedisCache(options =>
            {
	            options.Configuration = "localhost:6379";
            });
            
            if (appSettings.IsDevelopment)
            {
	            services.AddSwaggerGen(c =>
	            {
		            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                    var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		            {
			            Name = "Authorization",
			            In = ParameterLocation.Header,
			            Type = SecuritySchemeType.ApiKey,
			            Scheme = "Bearer"
		            });
		            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
		            {
			            {
				            new OpenApiSecurityScheme
				            {
					            Reference = new OpenApiReference
					            {
						            Type = ReferenceType.SecurityScheme,
						            Id = "Bearer"
					            },
					            Scheme = "oauth2",
					            Name = "Bearer",
					            In = ParameterLocation.Header,
				            },
				            new List<string>()
			            }
		            });
	            });
	            services.AddSwaggerGenNewtonsoftSupport();
            }
            
            var limitedFileSize = 20971520; //20 MB //int.MaxValue
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = limitedFileSize;
                o.MultipartBodyLengthLimit = limitedFileSize;
                o.MemoryBufferThreshold = limitedFileSize;
            });

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
	            x.Events = new JwtBearerEvents
	            {
		            OnTokenValidated = context => HandleIdentity(services, context)
	            };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Add HangFire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());
            services.AddHangfireServer();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<IStringLocalizer, StringLocalizer<SharedResource>>();
            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization();

            AddToScoped(services);
        }

        private static void AddToScoped(IServiceCollection services)
        {
            services.AddScoped<IProductAssociatedService, ProductAssociatedService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMemberServices, MemberServices>();
            services.AddScoped<IAutomationServices, AutomationServices>();
            services.AddScoped<IEmailService, SendGridService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<SequenceService>();
            services.AddScoped<CommonService>();

            /** API CONTROLLER SERVICES **/
            services.AddScoped<ProductService>();
            services.AddScoped<FundService>();
            services.AddScoped<FixOrderService>();
            /** END **/

            services.AddScoped<DataContextInitialize>();
        }

        private Task HandleIdentity(IServiceCollection services, TokenValidatedContext context)
        {
	        var authenticateService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
	        var userId = Guid.Parse(context.Principal.Identity.Name);

	        var provider = services.BuildServiceProvider();
	        var distributedCache = provider.GetService<IDistributedCache>();
	        byte[] dataCache = null;

         //   if (!_env.IsDevelopment())
	        //{
		       // dataCache = distributedCache?.Get(userId.ToString());
         //   }

	        User user;
	        UserToken validateToken;
	        if (dataCache == null)
	        {
		        user = authenticateService.FindUser(userId);
		        if (user == null || user.Status != UserStatus.Active)
		        {
			        context.Fail("Unauthorized");
			        return Task.CompletedTask;
		        }

		        validateToken = authenticateService.GetValidToken(userId);
		        //if (!_env.IsDevelopment() && validateToken != null && user.Id.HasValue)
		        //{
			       // CacheUser(user, validateToken, distributedCache);
		        //}
	        }
	        else
	        {
		        var serializedUser = Encoding.UTF8.GetString(dataCache);
		        var cachedUser = JsonConvert.DeserializeObject<CachedUser>(serializedUser);
		        validateToken = cachedUser.ValidateToken;
		        user = cachedUser.User;
	        }

	        if (user == null || user.Status != UserStatus.Active)
	        {
		        // return unauthorized if user no longer exists
		        context.Fail("Unauthorized");
		        return Task.CompletedTask;
	        }

	        var validateTokenValue = context.Principal.Claims.FirstOrDefault(c => c.Type == "ValidateToken")?.Value;
	        if (validateTokenValue == null || validateToken == null || validateTokenValue != validateToken.Token.ToString())
	        {
		        context.Fail("Unauthorized");
	        }
	        else
	        {
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString().Trim();
		        authenticateService.SetUserInfo(user, ipAddress);
            }

	        return Task.CompletedTask;
        }

        private static void CacheUser(User user, UserToken validateToken, IDistributedCache distributedCache)
        {
	        var cachedUser = new CachedUser
	        {
		        User = user,
		        ValidateToken = validateToken,
		        LastRefresh = DateTime.Now
	        };

	        var userSerialize = JsonConvert.SerializeObject(cachedUser);
	        var userBytes = Encoding.UTF8.GetBytes(userSerialize);
	        var options = new DistributedCacheEntryOptions()
		        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
		        .SetSlidingExpiration(TimeSpan.FromMinutes(1));
	        distributedCache.Set(user.Id.Value.ToString(), userBytes, options);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext, IOptions<AppSettings> appSettings, DataContextInitialize dataContextInitialize)
        {
	        dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                dataContextInitialize.SeedData();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Directory.CreateDirectory(appSettings.Value.ImagePath);
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(appSettings.Value.ImagePath),
            //    RequestPath = new PathString("/Resources")
            //});
            app.UseSerilogRequestLogging();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            #region snippet2
            var supportedCultures = new[] { "en", "vi" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
            #endregion

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            if (appSettings.Value.IsDevelopment)
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c => {
                    c.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host}" } };
                    });
                });
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();

            }
            else
            {
	            app.UseExceptionHandler("/Error");
	            app.UseHsts();
            }
            
			app.UseHangfireDashboard("/hangfire", new DashboardOptions
			{
				Authorization = new[] { new HangFireAuthorizationFilter(appSettings) }
			});
            //hangfire
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //Daily
            //RecurringJob.AddOrUpdate<IAutomationServices>(s => s.CheckBusinessAccountExpired(), Cron.Daily(0, 5), TimeZoneInfo.Local);

            //Monthly

            //Optional

        }
    }
}
