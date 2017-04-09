using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class ConfigurationManager : IConfigurationManager
    {
        private const string ConfigFileName = ".kvconfig";

        private readonly string _configurationFilePath;

        public ConfigurationManager()
        {
            _configurationFilePath = GetUserConfigPath();
            ReloadConfiguration();
        }

        #region IConfigurationManager

        public Configuration Configuration { get; private set; }

        public void ReloadConfiguration()
        {
            if (File.Exists(_configurationFilePath))
            {
                var json = File.ReadAllText(_configurationFilePath);
                Configuration = DeserializeConfiguration(json);
            }
            else
            {
                Configuration = new Configuration();
            }
        }

        public void SaveConfiguration()
        {
            var json = SerializeConfiguration(Configuration);
            File.WriteAllText(_configurationFilePath, json);
        }

        public VaultConfig GetVaultConfig(string vaultName)
        {
            if (string.IsNullOrWhiteSpace(vaultName))
            {
                throw new ArgumentNullException(nameof(vaultName));
            }

            return Configuration.KnownVaults
                .FirstOrDefault(x => StringComparer.OrdinalIgnoreCase.Equals(x.Key, vaultName))
                .Value;
        }

        public AuthConfig GetAuthConfig(VaultConfig vaultConfig)
        {
            if (vaultConfig == null)
            {
                throw new ArgumentNullException(nameof(vaultConfig));
            }

            return Configuration.Authentication
                .FirstOrDefault(x => StringComparer.OrdinalIgnoreCase.Equals(x.Key, vaultConfig.Url) || Regex.IsMatch(vaultConfig.Url, x.Key))
                .Value;
        }

        #endregion

        #region Helpers

        private static string GetUserConfigPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ConfigFileName);
        }

        private static Configuration DeserializeConfiguration(string json)
        {
            return JsonConvert.DeserializeObject<Configuration>(json);
        }

        private static string SerializeConfiguration(Configuration configuration)
        {
            return JsonConvert.SerializeObject(configuration, Formatting.Indented);
        }

        #endregion
    }
}
