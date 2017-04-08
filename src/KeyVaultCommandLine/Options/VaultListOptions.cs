using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-list", HelpText = "Manage Key Vault CLI configuration (List)")]
    internal class VaultListOptions : VerboseOptions
    {
    }
}