using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Azure.KeyVault.Models;
using Mjcheetham.KeyVaultCommandLine.Services;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("list", HelpText = Strings.List_Verb_Help)]
    internal class ListCommand : Command
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }

        public override void Execute()
        {
            var vaultConfig = ConfigurationManager.GetVaultConfig(Vault);
            if (vaultConfig == null)
            {
                WriteError($"Unknown vault '{Vault}'");
                return;
            }

            var authConfig = ConfigurationManager.GetAuthConfig(vaultConfig);

            IKeyVaultService kvService = CreateVaultService(authConfig);

            IEnumerable<SecretItem> secrets;
            try
            {
                secrets = kvService.GetSecrets(new Uri(vaultConfig.Url));
            }
            catch (Exception ex)
            {
                WriteError($"Failed to list secrets in vault '{Vault}'", ex);
                return;
            }

            foreach (var secret in secrets)
            {
                if (Verbose)
                {
                    Console.Out.WriteJson(secret);
                }
                else
                {
                    Console.Out.WriteLine(secret.Identifier.Name);
                }
            }
        }
    }
}
