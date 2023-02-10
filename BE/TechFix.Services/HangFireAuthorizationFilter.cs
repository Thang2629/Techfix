using Hangfire.Dashboard;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;

namespace TechFix.Services
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
	    private IOptions<AppSettings> _appSettings;

        public HangFireAuthorizationFilter(IOptions<AppSettings> appSettings)
        {
	        _appSettings = appSettings;

        }
        public bool Authorize(DashboardContext context)
        {

            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            //var owinContext = new OwinContext(context.GetOwinEnvironment());

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return owinContext.Authentication.User.Identity.IsAuthenticated;
            //var httpContext = context.GetHttpContext();
            //return httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated;
            return _appSettings.Value.IsDevelopment;
        }
    }
}