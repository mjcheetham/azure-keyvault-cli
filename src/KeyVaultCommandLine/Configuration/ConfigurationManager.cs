using System;
using System.IO;
using Newtonsoft.Json;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class ConfigurationManager : IConfigurationManager
    {
        private readonly string _configurationFilePath;

        public ConfigurationManager(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException("Path must be an absolute path", nameof(path));
            }

            if (File.Exists(path))
            {
                _configurationFilePath = path;
            }
            else if (Directory.Exists(path))
            {
                _configurationFilePath = SearchForConfiguration(path);
            }
            else
            {
                throw new ArgumentException("Value should be either a valid file path to a configuration file or a directory", nameof(path));
            }

            if (_configurationFilePath == null)
            {
                Configuration = new Configuration();
                _configurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".kvconfig");
            }
            else
            {
                ReloadConfiguration();
            }
        }

        #region IConfigurationManager

        public Configuration Configuration { get; private set; }

        public void ReloadConfiguration()
        {
            var json = File.ReadAllText(_configurationFilePath);
            Configuration = DeserializeConfiguration(json);
        }

        public void SaveConfiguration()
        {
            var json = SerializeConfiguration(Configuration);
            File.WriteAllText(_configurationFilePath, json);
        }

        #endregion

        #region Helpers

        private string SearchForConfiguration(string rootPath)
        {
            foreach(var child in Directory.GetFileSystemEntries(rootPath))
            {
                if (StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(child), ".kvconfig"))
                {
                    return child;
                }
            }

            return null;
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
