using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace TechFix.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        Log.Logger = new LoggerConfiguration()
		        .WriteTo.Console()
		        .CreateBootstrapLogger();

	        Log.Information("Starting up!");

	        try
	        {
		        CreateHostBuilder(args).Build().Run();
		        Log.Information("Stopped cleanly");
	        }
	        catch (Exception ex)
	        {
		        Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
	        }
	        finally
	        {
		        Log.CloseAndFlush();
	        }
        }

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog((context, services, configuration) => configuration
					.ReadFrom.Configuration(context.Configuration)
					.ReadFrom.Services(services)
					.Enrich.FromLogContext()
					.WriteTo.Console())
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

	}
}

