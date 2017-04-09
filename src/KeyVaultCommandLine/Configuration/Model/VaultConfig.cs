using System;
using Newtonsoft.Json;

namespace Mjcheetham.KeyVaultCommandLine.Configuration.Model
{
    internal class VaultConfig
    {
        internal VaultConfig() { }

        public VaultConfig(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            Url = url;
        }

        public VaultConfig(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            Url = uri.ToString();
        }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
