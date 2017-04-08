using System;
using System.Collections.Generic;
using Microsoft.Azure.KeyVault.Models;

namespace Mjcheetham.KeyVaultCommandLine
{
    public interface IKeyVaultService
    {
        SecretBundle GetSecret(Uri secretUri);

        IEnumerable<SecretItem> GetSecrets(Uri vaultUri);
    }
}
