using CommandLine;
using Mjcheetham.KeyVaultCommandLine.Commands;
using Mjcheetham.KeyVaultCommandLine.Configuration;
using Ninject;
using System;

namespace Mjcheetham.KeyVaultCommandLine
{
    public class Program
    {
        private static IKernel _kernel;

        public static void Main(string[] args)
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IConfigurationManager>().ToMethod(_ => new ConfigurationManager());
            _kernel.Bind<IConsole>().ToMethod(_ => new StandardConsole());

            Parser.Default.ParseVerbs<ListCommand, GetCommand, VaultCommand, AuthCommand>(args)
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
