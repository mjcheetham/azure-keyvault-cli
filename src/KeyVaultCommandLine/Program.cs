using CommandLine;
using Mjcheetham.KeyVaultCommandLine.Configuration;
using Mjcheetham.KeyVaultCommandLine.Options;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Mjcheetham.KeyVaultCommandLine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<ListOptions, GetOptions, VaultListOptions, VaultAddOptions, VaultRemoveOptions>(args)
                .WithParsed<ListOptions>(List)
                .WithParsed<GetOptions>(Get)
                .WithParsed<VaultListOptions>(VaultList)
                .WithParsed<VaultAddOptions>(VaultAdd)
                .WithParsed<VaultRemoveOptions>(VaultRemove);
        }

        #region Option handlers

        private static void List(ListOptions options)
        {
            var vaultConfig = GetKnownVaultConfig(options.VaultName);
            IKeyVaultService kvService = CreateVaultService(vaultConfig.Authentication);

            var secrets = kvService.GetSecrets(new Uri(vaultConfig.Url));

            foreach (var secret in secrets)
            {
                if (options.Verbose)
                {
                    WriteJson(secret);
                }
                else
                {
                    Console.WriteLine(secret.Identifier.Name);
                }
            }
        }

        private static void Get(GetOptions options)
        {
            var vaultConfig = GetKnownVaultConfig(options.VaultName);
            IKeyVaultService kvService = CreateVaultService(vaultConfig.Authentication);

            var uri = new Uri($"{vaultConfig.Url}/secrets/{options.SecretName}");

            var secret = kvService.GetSecret(uri);

            if (!options.Force)
            {
                secret.Value = "********";
                Console.WriteLine("INFO: Secret value is masked; '--force' option is not present.");
            }

            if (options.Verbose)
            {
                WriteJson(secret);
            }
            else
            {
                Console.WriteLine(secret.Value);
            }
        }

        private static void VaultList(VaultListOptions options)
        {
            var configManager = new ConfigurationManager(Directory.GetCurrentDirectory());

            WriteJson(configManager.Configuration.KnownVaults);
        }

        private static void VaultAdd(VaultAddOptions options)
        {
            var configManager = new ConfigurationManager(Directory.GetCurrentDirectory());

            var newVault = new VaultConfig
            {
                Url = options.Url,
                Authentication = new VaultAuthConfig
                {
                    ClientId = options.ClientId,
                    CertificateThumbprint = options.CertificateThumbprint
                }
            };

            configManager.Configuration.KnownVaults[options.Name] = newVault;

            configManager.SaveConfiguration();
        }

        private static void VaultRemove(VaultRemoveOptions options)
        {
            var configManager = new ConfigurationManager(Directory.GetCurrentDirectory());

            if (configManager.Configuration.KnownVaults.Remove(options.Name))
            {
                configManager.SaveConfiguration();
            }
        }

        #endregion

        #region Helpers

        private static IKeyVaultService CreateVaultService(VaultAuthConfig authConfig)
        {
            IKeyVaultService keyVaultService;

            if (string.IsNullOrWhiteSpace(authConfig.CertificateThumbprint))
            {
                keyVaultService = new KeyVaultService(authConfig.ClientId);
            }
            else
            {
                var certificate = CertificateHelper.FindCertificateByThumbprint(authConfig.CertificateThumbprint);
                if (certificate == null)
                {
                    throw new Exception($"No certificate with thumbprint {authConfig.CertificateThumbprint} could be found.");
                }

                keyVaultService = new KeyVaultService(authConfig.ClientId, certificate);
            }

            return keyVaultService;
        }

        private static void WriteJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static VaultConfig GetKnownVaultConfig(string vaultName)
        {
            var configManager = new ConfigurationManager(Directory.GetCurrentDirectory());

            VaultConfig vaultConfig;
            configManager.Configuration.KnownVaults.TryGetValue(vaultName, out vaultConfig);
            return vaultConfig;
        }

        #endregion
    }
}
