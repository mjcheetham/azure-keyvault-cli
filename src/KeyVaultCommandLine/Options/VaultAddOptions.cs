using System;
using CommandLine;

namespace Mjcheetham.KeyVaultCommandLine.Options
{
    [Verb("vault-add", HelpText = "Manage Key Vault CLI configuration (Add)")]
    internal class VaultAddOptions
    {
        [Option('n', "name", Required = true, HelpText = "Name of the Key Vault")]
        public string Name { get; set; }

        [Option('u', "url", Required = true, HelpText = "URL of the Key Vault")]
        public string Url { get; set; }

        [Option('c', "clientid", Required = true, HelpText = "Client ID")]
        public Guid ClientId { get; set; }

        [Option('t', "thumbprint", Required = false, HelpText = "Thumbprint of a certificate to use to authenticate to the vault")]
        public string CertificateThumbprint { get; set; }
    }
}