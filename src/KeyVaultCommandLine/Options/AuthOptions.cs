using System;
using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("auth", HelpText = Strings.Auth_Verb_Help)]
    [ChildVerbs(typeof(ListOptions), typeof(AddOptions), typeof(RemoveOptions))]
    internal class AuthOptions
    {
        [Verb("list", HelpText = Strings.AuthList_Verb_Help)]
        public class ListOptions : AuthOptions { }

        [Verb("add", HelpText = Strings.AuthAdd_Verb_Help)]
        public class AddOptions : AuthOptions
        {
            [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
            public string Url { get; set; }

            [Option('c', "clientid", Required = true, HelpText = Strings.AuthAdd_Param_ClientId_Help)]
            public Guid ClientId { get; set; }

            [Option('t', "thumbprint", Required = false, HelpText = Strings.AuthAdd_Param_CertificateThumbprint_Help)]
            public string CertificateThumbprint { get; set; }
        }

        [Verb("remove", HelpText = Strings.AuthRemove_Verb_Help)]
        public class RemoveOptions : AuthOptions
        {
            [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
            public string Url { get; internal set; }
        }
    }
}
