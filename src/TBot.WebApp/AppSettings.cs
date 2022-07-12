using Microsoft.Extensions.Configuration;
using TBot.WebApp.Settings;

namespace TBot.WebApp
{
    internal interface IAppSettings
    {
    }

    internal class AppSettings : IAppSettings, IConnectionSetting
    {
        public string ConnectionString { get; }
        
        public AppSettings(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("MainDb");
        }
    }
}