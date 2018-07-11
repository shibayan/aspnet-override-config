using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(OverrideConfig.OverrideBootstrapper), "Start")]

namespace OverrideConfig
{
    public class OverrideBootstrapper
    {
        public static void Start()
        {
            var environmentVariables = Environment.GetEnvironmentVariables()
                                                  .Cast<DictionaryEntry>()
                                                  .ToDictionary(x => (string)x.Key, x => (string)x.Value);

            foreach (var appSetting in environmentVariables.Where(x => x.Key.StartsWith(AppSettingsPrefix, StringComparison.OrdinalIgnoreCase)))
            {
                ConfigurationManager.AppSettings[appSetting.Key.Substring(AppSettingsPrefix.Length)] = appSetting.Value;
            }

            var connectionStrings = new ConnectionStringSettingsCollectionWrapper(ConfigurationManager.ConnectionStrings);

            foreach (var connectionString in environmentVariables.Where(x => x.Key.StartsWith(SqlServerConnStrPrefix, StringComparison.OrdinalIgnoreCase)))
            {
                connectionStrings.AddOrUpdate(connectionString.Key.Substring(SqlServerConnStrPrefix.Length), connectionString.Value, SqlServerProviderName);
            }
        }

        private const string AppSettingsPrefix = "APPSETTING_";
        private const string SqlServerConnStrPrefix = "SQLSERVERCONNSTR_";

        private const string SqlServerProviderName = "System.Data.SqlClient";
    }
}
