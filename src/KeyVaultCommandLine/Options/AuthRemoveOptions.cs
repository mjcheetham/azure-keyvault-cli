using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("auth-remove", HelpText = Strings.AuthRemove_Verb_Help)]
    internal class AuthRemoveOptions
    {
        [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
        public string Url { get; internal set; }
    }
}
