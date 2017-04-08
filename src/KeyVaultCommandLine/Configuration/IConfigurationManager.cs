
namespace Mjcheetham.KeyVaultCommandLine.Configuration
{
    internal interface IConfigurationManager
    {
        Configuration Configuration { get; }

        void ReloadConfiguration();

        void SaveConfiguration();
    }
}
