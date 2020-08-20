using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace TodoAPI
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            // Set logs output directory based on runtime environment
            var environment =  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var logger = 
                environment == Microsoft.AspNetCore.Hosting.EnvironmentName.Development 
                ? NLogBuilder.ConfigureNLog("nlog.dev.config").GetCurrentClassLogger() 
                : NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                logger.Error(e, "stopped program bseacuse of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
