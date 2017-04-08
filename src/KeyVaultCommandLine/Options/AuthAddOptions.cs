using System;
using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("auth-add", HelpText = Strings.AuthAdd_Verb_Help)]
    internal class AuthAddOptions
    {
        [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
        public string Url { get; set; }

        [Option('c', "clientid", Required = true, HelpText = Strings.AuthAdd_Param_ClientId_Help)]
        public Guid ClientId { get; set; }

        [Option('t', "thumbprint", Required = false, HelpText = Strings.AuthAdd_Param_CertificateThumbprint_Help)]
        public string CertificateThumbprint { get; set; }
    }
}
