using System;

namespace Mjcheetham.KeyVaultCommandLine.Configuration.Model
{
    internal static class VaultConfigExtensions
    {
        public static Uri GetVaultUri(this VaultConfig vaultConfig)
        {
            if (vaultConfig == null)
            {
                throw new ArgumentNullException(nameof(vaultConfig));
            }

            return new Uri(vaultConfig.Url);
        }

        public static Uri GetVaultUri(this VaultConfig vaultConfig, string relativeUri)
        {
            if (vaultConfig == null)
            {
                throw new ArgumentNullException(nameof(vaultConfig));
            }

            if (string.IsNullOrWhiteSpace(relativeUri))
            {
                throw new ArgumentNullException(nameof(relativeUri));
            }

            return new Uri(vaultConfig.GetVaultUri(), relativeUri);
        }
    }
}
