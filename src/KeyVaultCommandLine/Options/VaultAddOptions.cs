using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-add", HelpText = Strings.VaultAdd_Verb_Help)]
    internal class VaultAddOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = Strings.VaultAdd_Param_Name_Help)]
        public string Name { get; set; }

        [Value(1, MetaName = "url", Required = true, HelpText = Strings.VaultAdd_Param_Url_Help)]
        public string Url { get; set; }
    }
}
