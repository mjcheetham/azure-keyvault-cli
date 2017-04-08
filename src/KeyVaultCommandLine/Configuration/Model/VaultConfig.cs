using Newtonsoft.Json;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class VaultConfig
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("auth")]
        public VaultAuthConfig Authentication { get; set; } = new VaultAuthConfig();
    }
}
