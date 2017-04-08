using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class Configuration
    {
        [JsonProperty("vaults")]
        public Dictionary<string, VaultConfig> KnownVaults { get; set; } = new Dictionary<string, VaultConfig>();
    }
}
