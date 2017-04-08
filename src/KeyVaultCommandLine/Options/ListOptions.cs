using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("list", HelpText = "List all secrets in a Key Vault")]
    public class ListOptions : CommonOptions
    {
    }
}