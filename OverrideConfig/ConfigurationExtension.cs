using System.Configuration;
using System.Reflection;

namespace OverrideConfig
{
    internal class ConnectionStringSettingsCollectionWrapper
    {
        public ConnectionStringSettingsCollectionWrapper(ConnectionStringSettingsCollection connectionStrings)
        {
            _connectionStrings = connectionStrings;

            typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic)
                                                  .SetValue(_connectionStrings, false);
        }

        private readonly ConnectionStringSettingsCollection _connectionStrings;

        public void AddOrUpdate(string name, string connectionString, string providerName)
        {
            var settings = _connectionStrings[name];

            if (settings == null)
            {
                _connectionStrings.Add(new ConnectionStringSettings
                {
                    Name = name,
                    ConnectionString = connectionString,
                    ProviderName = providerName
                });
            }
            else
            {
                typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic)
                                            .SetValue(settings, false);

                settings.ConnectionString = connectionString;
                settings.ProviderName = providerName;
            }
        }
    }
}
