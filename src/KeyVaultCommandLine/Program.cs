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
        private static IConfigurationManager _configManager;

        private static IConfigurationManager ConfigManager => _configManager ?? (_configManager = CreateConfigurationManger());

        public static void Main(string[] args)
        {
            Parser.Default
                .ParseVerbs<ListOptions, GetOptions, VaultOptions, AuthOptions>(args)
                .WithParsed<ListOptions>(List)
                .WithParsed<GetOptions>(Get)
                .WithParsed<VaultOptions>(Vault)
                .WithParsed<AuthOptions>(Auth);
        }

        #region Secret handlers

        private static void List(ListOptions options)
        {
            var vaultConfig = ConfigManager.GetVaultConfig(options.Vault);
            if (vaultConfig == null)
            {
                PrintError($"Unknown vault '{options.Vault}'");
                return;
            }

            var authConfig = ConfigManager.GetAuthConfig(vaultConfig);

            IKeyVaultService kvService = CreateVaultService(authConfig);

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
            var vaultConfig = ConfigManager.GetVaultConfig(options.Vault);
            if (vaultConfig == null)
            {
                PrintError($"Unknown vault '{options.Vault}'");
                return;
            }

            var authConfig = ConfigManager.GetAuthConfig(vaultConfig);

            IKeyVaultService kvService = CreateVaultService(authConfig);

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

        private static void Vault(VaultOptions options)
        {
            if (options is VaultOptions.ListOptions)
            {
                VaultList((VaultOptions.ListOptions) options);
            }
            else if (options is VaultOptions.AddOptions)
            {
                VaultAdd((VaultOptions.AddOptions)options);
            }
            else if (options is VaultOptions.RemoveOptions)
            {
                VaultRemove((VaultOptions.RemoveOptions) options);
            }
        }

        private static void VaultList(VaultOptions.ListOptions options)
        {
            if (options.Verbose)
            {
                PrintJson(ConfigManager.Configuration.KnownVaults);
            }
            else
            {
                foreach (var vault in ConfigManager.Configuration.KnownVaults.Select(x => new { Name = x.Key, Configuration = x.Value }))
                {
                    Console.WriteLine($"{vault.Name}: {vault.Configuration.Url}");
                }
            }
        }

        private static void VaultAdd(VaultOptions.AddOptions options)
        {
            var configManager = CreateConfigurationManger();

            configManager.Configuration.KnownVaults[options.Name] = new VaultConfig(options.Url);

            configManager.SaveConfiguration();
        }

        private static void VaultRemove(VaultOptions.RemoveOptions options)
        {
            var configManager = new ConfigurationManager();

            if (configManager.Configuration.KnownVaults.Remove(options.Name))
            {
                configManager.SaveConfiguration();
            }
        }

        private static void Auth(AuthOptions options)
        {
            if (options is AuthOptions.ListOptions)
            {
                AuthList((AuthOptions.ListOptions)options);
            }
            else if (options is AuthOptions.AddOptions)
            {
                AuthAdd((AuthOptions.AddOptions)options);
            }
            else if (options is AuthOptions.RemoveOptions)
            {
                AuthRemove((AuthOptions.RemoveOptions) options);
            }
        }

        private static void AuthList(AuthOptions.ListOptions options)
        {
            PrintJson(ConfigManager.Configuration.Authentication);
        }

        private static void AuthAdd(AuthOptions.AddOptions options)
        {
            var configManager = CreateConfigurationManger();

            configManager.Configuration.Authentication[options.Url] = new AuthConfig(options.ClientId, options.CertificateThumbprint);

            configManager.SaveConfiguration();
        }

        private static void AuthRemove(AuthOptions.RemoveOptions options)
        {
            var configManager = new ConfigurationManager();

            if (configManager.Configuration.Authentication.Remove(options.Url))
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

        private static IKeyVaultService CreateVaultService(AuthConfig authConfig)
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

        private static void PrintError(string message)
        {
            Console.Error.WriteLine($"ERROR: {message}");
        }

        private static void PrintInfo(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        private static void PrintException(string message, Exception ex, bool verbose = false)
        {
            if (verbose)
            {
                PrintError(ex.ToString());
            }
            else
            {
                var exMessage = ex.InnerException == null
                              ? ex.Message
                              : $"{ex.Message} ({ex.InnerException.Message})";

                PrintError($"{message}. {exMessage}");
            }
        }

        #endregion
    }
}
