using System;
using CommandLine;
using Mjcheetham.KeyVaultCommandLine.Configuration;
using Ninject;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    internal abstract class Command
    {
        [Option('v', "verbose", HelpText = Strings.Common_Param_Verbose_Help)]
        public bool Verbose { get; set; }

        [Inject]
        public IConsole Console { get; set; }

        [Inject]
        public IConfigurationManager ConfigurationManager { get; set; }

        protected void WriteInfo(string message)
        {
            Console.Out.WriteLine($"INFO: {message}");
        }

        protected void WriteError(string message)
        {
            Console.Error.WriteLine($"ERROR: {message}");
        }

        protected void WriteError(string message, Exception exception)
        {
            var exMessage = exception.InnerException == null
                ? exception.Message
                : $"{exception.Message} ({exception.InnerException.Message})";

            WriteError($"{message}. {exMessage}");

            if (Verbose)
            {
                Console.Error.WriteLine($"EXCEPTION: {exception}");
            }
        }

        protected void WriteVerbose(string message)
        {
            if (Verbose)
            {
                Console.Out.WriteLine($"VERBOSE: {message}");
            }
        }

        protected IKeyVaultService CreateVaultService(AuthConfig authConfig)
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

        public abstract void Execute();
    }
}
