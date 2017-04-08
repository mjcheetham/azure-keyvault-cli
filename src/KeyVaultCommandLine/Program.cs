using CommandLine;
using Microsoft.Azure.KeyVault.Models;
using Mjcheetham.KeyVaultCommandLine.Configuration;
using Mjcheetham.KeyVaultCommandLine.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var vaultConfig = GetKnownVaultConfig(options.Vault);
            IKeyVaultService kvService = CreateVaultService(vaultConfig.Authentication);

            IEnumerable<SecretItem> secrets = null;
            try
            {
                secrets = kvService.GetSecrets(new Uri(vaultConfig.Url));
            }
            catch (Exception ex)
            {
                PrintException($"Failed to list secrets in vault '{options.Vault}'", ex, options.Verbose);
                return;
            }

            foreach (var secret in secrets)
            {
                if (options.Verbose)
                {
                    PrintJson(secret);
                }
                else
                {
                    Console.WriteLine(secret.Identifier.Name);
                }
            }
        }

        private static void Get(GetOptions options)
        {
            var vaultConfig = GetKnownVaultConfig(options.Vault);
            IKeyVaultService kvService = CreateVaultService(vaultConfig.Authentication);

            var uri = new Uri($"{vaultConfig.Url}/secrets/{options.Secret}");

            SecretBundle secret = null;
            try
            {
                secret = kvService.GetSecret(uri);
            }
            catch (Exception ex)
            {
                PrintException($"Failed to get secret '{options.Secret}' from vault '{options.Vault}'", ex, options.Verbose);
                return;
            }

            if (!options.Force)
            {
                secret.Value = "********";
                PrintInfo("Secret value is masked; '--force' option is not present");
            }

            if (options.Verbose)
            {
                PrintJson(secret);
            }
            else
            {
                Console.WriteLine(secret.Value);
            }
        }

        private static void VaultList(VaultListOptions options)
        {
            var configManager = CreateConfigurationManger();

            if (options.Verbose)
            {
                PrintJson(configManager.Configuration.KnownVaults);
            }
            else
            {
                foreach (var vault in configManager.Configuration.KnownVaults.Select(x => new { Name = x.Key, Configuration = x.Value }))
                {
                    Console.WriteLine($"{vault.Name}: {vault.Configuration.Url}");
                }
            }
        }

        private static void VaultAdd(VaultAddOptions options)
        {
            var configManager = CreateConfigurationManger();

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
            var configManager = new ConfigurationManager();

            if (configManager.Configuration.KnownVaults.Remove(options.Name))
            {
                configManager.SaveConfiguration();
            }
        }

        #endregion

        #region Helpers

        private static IConfigurationManager CreateConfigurationManger()
        {
            return new ConfigurationManager();
        }

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

        private static void PrintJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static VaultConfig GetKnownVaultConfig(string vaultName)
        {
            var configManager = CreateConfigurationManger();

            VaultConfig vaultConfig;
            configManager.Configuration.KnownVaults.TryGetValue(vaultName, out vaultConfig);
            return vaultConfig;
        }

        private static void PrintException(string errorMessage, Exception ex, bool verbose = false)
        {
            var exMessage = ex.InnerException == null
                ? ex.Message
                : $"{ex.Message} ({ex.InnerException.Message})";

            Console.Error.WriteLine($"ERROR: {errorMessage}. {exMessage}");

            if (verbose)
            {
                Console.Error.WriteLine($"ERROR: {ex.ToString()}");
            }
        }

        private static void PrintInfo(string message)
        {
            Console.WriteLine($"INFO: {message}.");
        }

        #endregion
    }
}
