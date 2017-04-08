using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    internal class VerboseOptions
    {
        [Option('v', "verbose", HelpText = Strings.Common_Param_Verbose_Help)]
        public bool Verbose { get; set; }
    }
}
