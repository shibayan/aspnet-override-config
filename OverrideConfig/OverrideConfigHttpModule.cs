using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OverrideConfig
{
    public class OverrideConfigHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            var environmentVariables = Environment.GetEnvironmentVariables()
                                                  .Cast<DictionaryEntry>()
                                                  .ToDictionary(x => (string)x.Key, x => (string)x.Value);

            foreach (var appSetting in environmentVariables.Where(x => x.Key.StartsWith("APPSETTINGS_", StringComparison.OrdinalIgnoreCase)))
            {
                ConfigurationManager.AppSettings[appSetting.Key.Substring(12)] = appSetting.Value;
            }

            foreach (var connectionString in environmentVariables.Where(x => x.Key.StartsWith("SQLSERVERCONNSTR_", StringComparison.OrdinalIgnoreCase)))
            {
                var settings = new ConnectionStringSettings
                {
                    Name = connectionString.Key.Substring(17),
                    ConnectionString = connectionString.Value,
                    ProviderName = "System.Data.SqlClient"
                };

                ConfigurationManager.ConnectionStrings.Remove(settings.Name);
                ConfigurationManager.ConnectionStrings.Add(settings);
            }
        }

        public void Dispose()
        {
        }
    }
}
