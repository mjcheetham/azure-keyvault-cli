using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-remove", HelpText = "Manage Key Vault CLI configuration (Remove)")]
    internal class VaultRemoveOptions
    {
        [Option('n', "name", Required = true, HelpText = "Name of the Key Vault to remove")]
        public string Name { get; internal set; }
    }
}