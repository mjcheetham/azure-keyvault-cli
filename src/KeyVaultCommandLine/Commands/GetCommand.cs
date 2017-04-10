using System;
using CommandLine;
using Microsoft.Azure.KeyVault.Models;
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;
using Mjcheetham.KeyVaultCommandLine.Services;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("get", HelpText = Strings.Get_Verb_Help)]
    internal class GetCommand : VaultCommand
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }

        [Value(1, MetaName = "secret", Required = true, HelpText = Strings.Common_Param_Secret_Help)]
        public string Secret { get; set; }

        [Option('f', "force", HelpText = Strings.Get_Param_Force_Help)]
        public bool Force { get; set; }

        public override void Execute()
        {
            var vaultConfig = ConfigurationManager.GetVaultConfig(Vault); 
            if (vaultConfig == null)
            {
                WriteError($"Unknown vault '{Vault}'");
                return;
            }

            var authConfig = ConfigurationManager.GetAuthConfig(vaultConfig);
            if (authConfig == null)
            {
                WriteError($"No authentication methods have been configured for vault '{Vault}'; see `kv auth`");
                return;
            }

            IKeyVaultService kvService = CreateVaultService(authConfig);

            SecretBundle secret;
            try
            {
                secret = kvService.GetSecret(vaultConfig.GetVaultUri($"secrets/{Secret}"));
            }
            catch (Exception ex)
            {
                WriteError($"Failed to get secret '{Secret}' from vault '{Vault}'", ex);
                return;
            }

            if (!Force)
            {
                secret.Value = "********";
                WriteInfo("Secret value is masked; '--force' option is not present");
            }

            if (Verbose)
            {
                Console.Out.WriteJson(secret);
            }
            else
            {
                Console.Out.WriteLine(secret.Value);
            }
        }
    }
}
