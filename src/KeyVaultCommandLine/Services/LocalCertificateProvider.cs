using System;
using System.Security.Cryptography.X509Certificates;

namespace Mjcheetham.KeyVaultCommandLine.Services
{
    internal class LocalCertificateProvider : ICertificateProvider
    {
        public X509Certificate2 FindCertificateByThumbprint(string thumbprint)
        {
            foreach (StoreLocation storeLocation in Enum.GetValues(typeof(StoreLocation)))
            {
                var certificate = FindCertificateByThumbprint(thumbprint, storeLocation, StoreName.My);
                if (certificate != null)
                {
                    return certificate;
                }
            }

            return null;
        }

        public X509Certificate2 FindCertificateByThumbprint(string thumbprint, StoreLocation location, StoreName name)
        {
            // Don't validate certs, since the test root isn't installed.
            const bool validateCerts = false;

            var store = new X509Store(name, location);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validateCerts);

                return collection.Count == 0
                    ? null
                    : collection[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}