using System.Security.Cryptography.X509Certificates;

namespace Mjcheetham.KeyVaultCommandLine.Services
{
    internal interface ICertificateProvider
    {
        /// <summary>
        /// Try and locate a certificate matching the given <paramref name="thumbprint"/> by searching in 
        /// the in the <see cref="StoreName.My"/> store name for all all available <see cref="StoreLocation"/>s.
        /// </summary>
        /// <param name="thumbprint">Thumbprint of certificate to locate</param>
        /// <returns><see cref="X509Certificate2"/> with <paramref name="thumbprint"/>, or null if no matching certificate was found</returns>
        X509Certificate2 FindCertificateByThumbprint(string thumbprint);

        /// <summary>
        /// Try and locate a certificate matching the given <paramref name="thumbprint"/> by searching in 
        /// the in the given <see cref="StoreName"/> and <see cref="StoreLocation"/>.
        /// </summary>
        /// <param name="thumbprint">Thumbprint of certificate to locate</param>
        /// <param name="location"><see cref="StoreLocation"/> in which to search for a matching certificate</param>
        /// <param name="name"><see cref="StoreName"/> in which to search for a matching certificate</param>
        /// <returns><see cref="X509Certificate2"/> with <paramref name="thumbprint"/>, or null if no matching certificate was found</returns>
        X509Certificate2 FindCertificateByThumbprint(string thumbprint, StoreLocation location, StoreName name);
    }
}
