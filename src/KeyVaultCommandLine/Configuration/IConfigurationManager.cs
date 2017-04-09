
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;

namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal interface IConfigurationManager
    {
        Configuration Configuration { get; }

        void ReloadConfiguration();

        void SaveConfiguration();

        VaultConfig GetVaultConfig(string vaultName);

        AuthConfig GetAuthConfig(VaultConfig vaultConfig);
    }
}
