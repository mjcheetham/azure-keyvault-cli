using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault", HelpText = Strings.Vault_Verb_Help)]
    [ChildVerbs(typeof(ListOptions), typeof(AddOptions), typeof(RemoveOptions))]
    internal class VaultOptions : VerboseOptions
    {
        [Verb("list", HelpText = Strings.VaultList_Verb_Help)]
        public class ListOptions : VaultOptions { }

        [Verb("add", HelpText = Strings.VaultAdd_Verb_Help)]
        public class AddOptions : VaultOptions
        {
            [Value(0, MetaName = "name", Required = true, HelpText = Strings.Vault_Param_Name_Help)]
            public string Name { get; set; }

            [Value(1, MetaName = "url", Required = true, HelpText = Strings.VaultAdd_Param_Url_Help)]
            public string Url { get; set; }
        }

        [Verb("remove", HelpText = Strings.VaultRemove_Verb_Help)]
        public class RemoveOptions : VaultOptions
        {
            [Value(0, MetaName = "name", Required = true, HelpText = Strings.Vault_Param_Name_Help)]
            public string Name { get; set; }
        }
    }
}
