using System;
using CommandLine;
using Microsoft.Azure.KeyVault.Models;
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;
using Mjcheetham.KeyVaultCommandLine.Services;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("delete", HelpText = Strings.Delete_Verb_Help)]
    internal class DeleteCommand : Command
    {
        [Value(0, MetaName = "vault", Required = true, HelpText = Strings.Common_Param_Vault_Help)]
        public string Vault { get; set; }

        [Value(1, MetaName = "secret", Required = true, HelpText = Strings.Common_Param_Secret_Help)]
        public string Secret { get; set; }

        [Option('f', "force", Required = true, HelpText = Strings.Delete_Param_Force_Help)]
        public bool Force { get; set; }

        public override void Execute()
        {
            var vaultConfig = ConfigurationManager.GetVaultConfig(Vault);
            if (vaultConfig == null)
            {
                WriteError($"Unknown vault '{Vault}'");
                return;
            }

            if (!Force)
            {
                WriteError("'--force' option is required");
                return;
            }

            var authConfig = ConfigurationManager.GetAuthConfig(vaultConfig);
            if (authConfig == null)
            {
                WriteError($"No authentication methods have been configured for vault '{Vault}'; see `kv auth`");
                return;
            }

            IKeyVaultService kvService = CreateVaultService(authConfig);

            SecretBundle deletedSecret;
            try
            {
                deletedSecret = kvService.DeleteSecret(vaultConfig.GetVaultUri(), Secret);
            }
            catch (Exception ex)
            {
                WriteError($"Failed to delete secret '{Secret}' in vault '{Vault}'", ex);
                return;
            }

            WriteInfo($"Secret '{Secret}' was deleted successfully");

            if (Verbose)
            {
                WriteVerbose("Deleted secret:");
                Console.Out.WriteJson(deletedSecret);
            }
        }
    }
}
