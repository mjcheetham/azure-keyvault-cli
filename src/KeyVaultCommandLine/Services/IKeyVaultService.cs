using System;
using System.Collections.Generic;
using Microsoft.Azure.KeyVault.Models;

namespace Mjcheetham.KeyVaultCommandLine.Services
{
    public interface IKeyVaultService
    {
        SecretBundle GetSecret(Uri secretUri);

        IEnumerable<SecretItem> GetSecrets(Uri vaultUri);

        SecretBundle SetSecret(Uri vaultUri, string secretName, string secretValue);

        SecretBundle DeleteSecret(Uri vaultUri, string secretName);
    }
}
