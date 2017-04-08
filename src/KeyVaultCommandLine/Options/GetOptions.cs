using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("get", HelpText = Strings.Get_Verb_Help)]
    internal class GetOptions : VerboseOptions
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }

        [Value(1, MetaName = "secret", Required = true, HelpText = Strings.Get_Param_Secret_Help)]
        public string Secret { get; set; }

        [Option('f', "force", HelpText = Strings.Get_Param_Force_Help)]
        public bool Force { get; set; }
    }
}
