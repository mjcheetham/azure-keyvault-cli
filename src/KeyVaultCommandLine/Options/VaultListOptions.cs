using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-list", HelpText = Strings.VaultList_Verb_Help)]
    internal class VaultListOptions : VerboseOptions
    {
    }
}
