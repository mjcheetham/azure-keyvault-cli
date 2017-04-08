using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("get", HelpText = "Get a secret from Key Vault")]
    public class GetOptions : CommonOptions
    {
        [Option('n', "name", Required = true, HelpText = "Name of secret to get from the vault")]
        public string SecretName { get; set; }

        [Option('f', "force", HelpText = "Print the plain-text secret value")]
        public bool Force { get; set; }
    }
}
