namespace Mjcheetham.KeyVaultCommandLine
{
    internal static class Strings
    {
        public const string Common_Param_Verbose_Help = "Display verbose information";
        public const string Common_Param_Vault_Help = "Name of known Key Vault";

        public const string VaultList_Verb_Help = "Manage Key Vault CLI configuration (List)";

        public const string VaultAdd_Verb_Help = "Manage Key Vault CLI configuration (Add)";
        public const string VaultAdd_Param_Name_Help = "Name of the Key Vault to add";
        public const string VaultAdd_Param_Url_Help = "Vault URL";
        public const string VaultAdd_Param_ClientId_Help = "Client ID";
        public const string VaultAdd_Param_CertificateThumbprint_Help = "Thumbprint of a certificate to use to authenticate to the vault";

        public const string VaultRemove_Verb_Help = "Manage Key Vault CLI configuration (Remove)";
        public const string VaultRemove_Param_Name_Help = "Name of the Key Vault to remove";

        public const string List_Verb_Help = "List all secrets in a Key Vault";

        public const string Get_Verb_Help = "Get a secret from Key Vault";
        public const string Get_Param_Secret_Help = "Name of secret to get from the vault";
        public const string Get_Param_Force_Help = "Print the plain-text secret value";

    }
}
