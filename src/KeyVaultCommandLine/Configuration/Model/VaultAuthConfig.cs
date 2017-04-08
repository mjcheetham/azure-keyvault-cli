using Newtonsoft.Json;
using System;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class VaultAuthConfig
    {
        [JsonProperty("client")]
        public Guid ClientId { get; set; }

        [JsonProperty("certThumbprint")]
        public string CertificateThumbprint { get; set; }
    }
}
