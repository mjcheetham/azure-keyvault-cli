namespace Mjcheetham.KeyVaultCommandLine
{
    internal static class Strings
    {
        public const string Common_Param_Verbose_Help = "Display verbose information";
        public const string Common_Param_Vault_Help = "Name of known vault";

        public const string Auth_Param_Url_Help = "Vault URL or regular expression";

        public const string Vault_Verb_Help = "Manage known vaults";

        public const string VaultList_Verb_Help = "List known vaults";

        public const string VaultAdd_Verb_Help = "Add a new vault to the known vaults";
        public const string VaultAdd_Param_Name_Help = "Name of the vault";
        public const string VaultAdd_Param_Url_Help = "URL for the vault";

        public const string VaultRemove_Verb_Help = "Remove a known vault";
        public const string VaultRemove_Param_Name_Help = "Name of the vault";

        public const string AuthList_Verb_Help = "Manage authentication configuration (List)";

        public const string AuthAdd_Verb_Help = "Manage authentication configuration (Add)";
        public const string AuthAdd_Param_ClientId_Help = "Client ID";
        public const string AuthAdd_Param_CertificateThumbprint_Help = "Thumbprint of a certificate to use to authenticate";

        public const string AuthRemove_Verb_Help = "Manage authentication configuration (Remove)";

        public const string List_Verb_Help = "List all secrets in a Key Vault";

        public const string Get_Verb_Help = "Get a secret from Key Vault";
        public const string Get_Param_Secret_Help = "Name of secret to get from the vault";
        public const string Get_Param_Force_Help = "Print the plain-text secret value";

    }
}
