using Newtonsoft.Json;
using System;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal class AuthConfig
    {
        internal AuthConfig() { }

        public AuthConfig(Guid clientId, string certificateThumbprint)
        {
            ClientId = clientId;
            CertificateThumbprint = certificateThumbprint;
        }

        [JsonProperty("client")]
        public Guid ClientId { get; set; }

        [JsonProperty("certThumbprint")]
        public string CertificateThumbprint { get; set; }
    }
}
