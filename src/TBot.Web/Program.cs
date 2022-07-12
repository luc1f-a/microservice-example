using Serilog;

namespace TBot.Web;

public static class Program
{
    public static Task Main(string[] args)
    {
        return CreateHostBuilder(args).Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.Sources.Clear();
                var env = context.HostingEnvironment.EnvironmentName;

                configBuilder.AddJsonFile("appsettings.json");
                configBuilder.AddJsonFile($"appsettings.{env}.json", optional: true);
                configBuilder.AddJsonFile($"Configs/connectionStrings.{env}.json", optional: true);
                configBuilder.AddJsonFile($"Configs/serilog.{env}.json", optional: true);
                configBuilder.AddEnvironmentVariables();
                if (args != null)
                {
                    configBuilder.AddCommandLine(args);
                }
            })
            .UseSerilog((hostingContext, loggerConfiguration) => { loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration); })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}