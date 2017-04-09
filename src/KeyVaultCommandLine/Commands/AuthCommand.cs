using System;
using CommandLine;
using Mjcheetham.KeyVaultCommandLine.Configuration;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("auth", HelpText = Strings.Auth_Verb_Help)]
    [ChildVerbs(typeof(ListCommand), typeof(AddCommand), typeof(RemoveCommand))]
    internal abstract class AuthCommand : Command
    {
        [Verb("list", HelpText = Strings.AuthList_Verb_Help)]
        public class ListCommand : AuthCommand
        {
            public override void Execute()
            {
                Console.Out.WriteJson(ConfigurationManager.Configuration.Authentication);
            }
        }

        [Verb("add", HelpText = Strings.AuthAdd_Verb_Help)]
        public class AddCommand : AuthCommand
        {
            [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
            public string Url { get; set; }

            [Option('c', "clientid", Required = true, HelpText = Strings.AuthAdd_Param_ClientId_Help)]
            public Guid ClientId { get; set; }

            [Option('t', "thumbprint", Required = false, HelpText = Strings.AuthAdd_Param_CertificateThumbprint_Help)]
            public string CertificateThumbprint { get; set; }

            public override void Execute()
            {
                ConfigurationManager.Configuration.Authentication[Url] = new AuthConfig(ClientId, CertificateThumbprint);
                ConfigurationManager.SaveConfiguration();
            }
        }

        [Verb("remove", HelpText = Strings.AuthRemove_Verb_Help)]
        public class RemoveCommand : AuthCommand
        {
            [Option('u', "url", Required = true, HelpText = Strings.Auth_Param_Url_Help)]
            public string Url { get; internal set; }

            public override void Execute()
            {
                if (ConfigurationManager.Configuration.Authentication.Remove(Url))
                {
                    ConfigurationManager.SaveConfiguration();
                }
            }
        }
    }
}
