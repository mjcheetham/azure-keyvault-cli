using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-remove", HelpText = Strings.VaultRemove_Verb_Help)]
    internal class VaultRemoveOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = Strings.VaultRemove_Param_Name_Help)]
        public string Name { get; internal set; }
    }
}
