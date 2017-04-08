using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    internal class CommonOptions : VerboseOptions
    {
        [Option('v', "vault", Required = true, HelpText = "Name of known Key Vault")]
        public string VaultName { get; set; }
    }
}