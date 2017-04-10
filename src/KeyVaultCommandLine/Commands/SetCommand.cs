using System;
using CommandLine;
using Microsoft.Azure.KeyVault.Models;
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;
using Mjcheetham.KeyVaultCommandLine.Services;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("set", HelpText = Strings.Set_Verb_Help)]
    internal class SetCommand : Command
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }

        [Value(1, MetaName = "secret", Required = true, HelpText = Strings.Common_Param_Secret_Help)]
        public string Secret { get; set; }

        [Value(2, MetaName = "value", Required = true, HelpText = Strings.Set_Param_Value_Help)]
        public string Value { get; set; }

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

            SecretBundle newSecret;
            try
            {
                newSecret = kvService.SetSecret(vaultConfig.GetVaultUri(), Secret, Value);
            }
            catch (Exception ex)
            {
                WriteError($"Failed to set secret '{Secret}' in vault '{Vault}'", ex);
                return;
            }

            if (Verbose)
            {
                Console.Out.WriteJson(newSecret);
            }
            else
            {
                WriteInfo($"Secret '{Secret}' was set successfully");
            }
        }
    }
}
