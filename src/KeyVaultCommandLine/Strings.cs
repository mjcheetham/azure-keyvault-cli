namespace Mjcheetham.KeyVaultCommandLine
{
    internal static class Strings
    {
        public const string Common_Param_Verbose_Help = "Display verbose information";
        public const string Common_Param_Vault_Help = "Name of known vault";
        public const string Common_Param_Secret_Help = "Name of secret";

        #region Vault

        public const string Vault_Verb_Help = "Manage known vaults";
        public const string Vault_Param_Name_Help = "Name of the vault";

        public const string VaultList_Verb_Help = "List known vaults";

        public const string VaultAdd_Verb_Help = "Add a new vault to the known vaults";
        public const string VaultAdd_Param_Url_Help = "URL for the vault";

        public const string VaultRemove_Verb_Help = "Remove a known vault";

        public const string VaultRename_Verb_Help = "Rename a known vault";
        public const string VaultRename_Param_OldName_Help = "Existing name of the vault";
        public const string VaultRename_Param_NewName_Help = "New name for the vault";

        #endregion

        #region Auth

        public const string Auth_Verb_Help = "Manage authentication configuration";
        public const string Auth_Param_Url_Help = "Vault URL or regular expression";

        public const string AuthList_Verb_Help = "List authentication configurations";

        public const string AuthAdd_Verb_Help = "Add a new authentication configuration";
        public const string AuthAdd_Param_ClientId_Help = "Client ID";
        public const string AuthAdd_Param_CertificateThumbprint_Help = "Thumbprint of a certificate to use to authenticate";

        public const string AuthRemove_Verb_Help = "Remove an authentication configuration";

        #endregion

        #region List

        public const string List_Verb_Help = "List all secrets in a Key Vault";

        #endregion

        #region Get

        public const string Get_Verb_Help = "Get a secret from Key Vault";
        public const string Get_Param_Force_Help = "Print the plain-text secret value";

        #endregion

        #region Set

        public const string Set_Verb_Help = "Create/set a secret in a Key Vault";
        public const string Set_Param_Value_Help = "Secret value to store in the vault";

        #endregion

        #region Delete

        public const string Delete_Verb_Help = "Delete a secret from a Key Vault";
        public const string Delete_Param_Force_Help = "Delete the secret";

        #endregion
    }
}
