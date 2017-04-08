using System;
using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-add", HelpText = Strings.VaultAdd_Verb_Help)]
    internal class VaultAddOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = Strings.VaultAdd_Param_Name_Help)]
        public string Name { get; set; }

        [Option('u', "url", Required = true, HelpText = Strings.VaultAdd_Param_Url_Help)]
        public string Url { get; set; }

        [Option('c', "clientid", Required = true, HelpText = Strings.VaultAdd_Param_ClientId_Help)]
        public Guid ClientId { get; set; }

        [Option('t', "thumbprint", Required = false, HelpText = Strings.VaultAdd_Param_CertificateThumbprint_Help)]
        public string CertificateThumbprint { get; set; }
    }
}
