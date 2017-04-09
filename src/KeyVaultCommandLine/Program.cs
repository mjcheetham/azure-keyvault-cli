using CommandLine;
using Mjcheetham.KeyVaultCommandLine.Commands;
using Mjcheetham.KeyVaultCommandLine.Configuration;
using Ninject;
using System;
using Mjcheetham.KeyVaultCommandLine.Services;

namespace Mjcheetham.KeyVaultCommandLine
{
    public class Program
    {
        private static IKernel _kernel;

        public static void Main(string[] args)
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IConfigurationManager>().To<ConfigurationManager>();
            _kernel.Bind<ICertificateProvider>().To<LocalCertificateProvider>();
            _kernel.Bind<IConsole>().To<StandardConsole>();

            Parser.Default.ParseVerbs<ListCommand, GetCommand, SetCommand, DeleteCommand, VaultCommand, AuthCommand>(args)
                          .WithParsed<Command>(ExecuteCommand);
        }

        private static void ExecuteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            try
            {
                _kernel.Inject(command);
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ERROR: Command execution failed. {ex.Message}");
            }
        }
    }
}
