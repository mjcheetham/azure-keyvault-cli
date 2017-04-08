using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Mjcheetham.KeyVaultCommandLine
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly KeyVaultClient _keyVaultClient;
        private readonly KeyVaultAuthenticationType _authenticationType;
        private readonly ClientAssertionCertificate _assertionCert;
        private readonly Guid _clientId;

        #region Constructors

        public KeyVaultService(Guid clientId)
        {
            if (clientId == Guid.Empty)
            {
                throw new ArgumentException(nameof(clientId));
            }

            _authenticationType = KeyVaultAuthenticationType.UserCredential;
            _clientId = clientId;

            _keyVaultClient = new KeyVaultClient(AuthenticationCallbackAsync);
        }

        public KeyVaultService(Guid clientId, X509Certificate2 certificate)
        {
            if (clientId == Guid.Empty)
            {
                throw new ArgumentException(nameof(clientId));
            }

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            _authenticationType = KeyVaultAuthenticationType.ClientCertificate;
            _clientId = clientId;
            _assertionCert = new ClientAssertionCertificate(_clientId.ToString("D"), certificate);
            _keyVaultClient = new KeyVaultClient(AuthenticationCallbackAsync);
        }

        #endregion

        #region IKeyVaultService

        public SecretBundle GetSecret(Uri secretUri)
        {
            return _keyVaultClient.GetSecretAsync(secretUri.ToString()).GetAwaiter().GetResult();
        }

        public IEnumerable<SecretItem> GetSecrets(Uri vaultUri)
        {
            return _keyVaultClient.GetSecretsAsync(vaultUri.ToString()).GetAwaiter().GetResult();
        }

        #endregion

        #region Private

        private async Task<string> AuthenticationCallbackAsync(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority, TokenCache.DefaultShared);

            AuthenticationResult authResult = null;
            switch (_authenticationType)
            {
                case KeyVaultAuthenticationType.ClientCertificate:
                    authResult = await authContext.AcquireTokenAsync(resource, _assertionCert);
                    break;
                case KeyVaultAuthenticationType.UserCredential:
                    authResult = await authContext.AcquireTokenAsync(resource, _clientId.ToString("D"), new UserCredential());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return authResult?.AccessToken;
        }

        #endregion
    }
}
