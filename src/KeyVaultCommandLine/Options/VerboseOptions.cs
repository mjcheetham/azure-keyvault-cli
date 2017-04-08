using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    internal class VerboseOptions
    {
        [Option("verbose", HelpText = "Display verbose information")]
        public bool Verbose { get; set; }
    }
}
