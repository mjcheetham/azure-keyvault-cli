using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    public class CommonOptions
    {
        [Option('v', "vault", Required = true, HelpText = "Name of known Key Vault")]
        public string VaultName { get; set; }

        [Option("verbose", HelpText = "Display verbose information")]
        public bool Verbose { get; set; }
    }
}