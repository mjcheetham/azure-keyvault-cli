using CommandLine;
using System.Linq;
using Mjcheetham.KeyVaultCommandLine.Configuration.Model;

namespace Mjcheetham.KeyVaultCommandLine.Commands
{
    [Verb("vault", HelpText = Strings.Vault_Verb_Help)]
    [ChildVerbs(typeof(ListCommand), typeof(AddCommand), typeof(RemoveCommand), typeof(RenameCommand))]
    internal abstract class VaultCommand : Command
    {
        [Verb("list", HelpText = Strings.VaultList_Verb_Help)]
        public class ListCommand : VaultCommand
        {
            public override void Execute()
            {
                if (Verbose)
                {
                    Console.Out.WriteJson(ConfigurationManager.Configuration.KnownVaults);
                }
                else
                {
                    foreach (var vault in ConfigurationManager.Configuration.KnownVaults.Select(x => new { Name = x.Key, Configuration = x.Value }))
                    {
                        System.Console.WriteLine($"{vault.Name}: {vault.Configuration.Url}");
                    }
                }
            }
        }

        [Verb("add", HelpText = Strings.VaultAdd_Verb_Help)]
        public class AddCommand : VaultCommand
        {
            [Value(0, MetaName = "name", Required = true, HelpText = Strings.Vault_Param_Name_Help)]
            public string Name { get; set; }

            [Value(1, MetaName = "url", Required = true, HelpText = Strings.VaultAdd_Param_Url_Help)]
            public string Url { get; set; }

            public override void Execute()
            {
                ConfigurationManager.Configuration.KnownVaults[Name] = new VaultConfig(Url);
                ConfigurationManager.SaveConfiguration();
            }
        }

        [Verb("remove", HelpText = Strings.VaultRemove_Verb_Help)]
        public class RemoveCommand : VaultCommand
        {
            [Value(0, MetaName = "name", Required = true, HelpText = Strings.Vault_Param_Name_Help)]
            public string Name { get; set; }

            public override void Execute()
            {
                if (ConfigurationManager.Configuration.KnownVaults.Remove(Name))
                {
                    ConfigurationManager.SaveConfiguration();
                }
            }
        }

        [Verb("rename", HelpText = Strings.VaultRename_Verb_Help)]
        public class RenameCommand : VaultCommand
        {
            [Value(0, MetaName = "old-name", Required = true, HelpText = Strings.VaultRename_Param_OldName_Help)]
            public string OldName { get; set; }

            [Value(1, MetaName = "new-name", Required = true, HelpText = Strings.VaultRename_Param_NewName_Help)]
            public string NewName { get; set; }

            public override void Execute()
            {
                var existingConfig = ConfigurationManager.GetVaultConfig(OldName);
                if (existingConfig == null)
                {
                    WriteError($"Unknown vault '{OldName}'");
                    return;
                }

                if (ConfigurationManager.Configuration.KnownVaults.ContainsKey(NewName))
                {
                    WriteError($"A vault with the name '{NewName}' already exists");
                    return;
                }

                ConfigurationManager.Configuration.KnownVaults.Remove(OldName);
                ConfigurationManager.Configuration.KnownVaults[NewName] = existingConfig;
                ConfigurationManager.SaveConfiguration();
            }
        }
    }
}
