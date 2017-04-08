using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("list", HelpText = Strings.List_Verb_Help)]
    internal class ListOptions : VerboseOptions
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }
    }
}
