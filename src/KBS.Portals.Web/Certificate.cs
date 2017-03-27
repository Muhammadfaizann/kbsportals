using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web.Configuration;

namespace KBS.Portals.Web
{
    public class Certificate
    {
        private static readonly string CertificateSubject = WebConfigurationManager.AppSettings["EmbeddedIdentityServerCertificateName"];

        public static X509Certificate2 Get()
        {
            var store = new X509Store("MY", StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            var foundCertificates = store.Certificates.Find(X509FindType.FindBySubjectName, CertificateSubject, false);
            store.Close();

            if (foundCertificates.Count != 1)
            {
                throw new ConfigurationErrorsException(
                    $"There are {foundCertificates.Count} certificates with the subject name {CertificateSubject} " +
                    "Use scripts/certificates/IdentityServer.ps1 to generate this");
            }

            var cert = foundCertificates[0];


            bool noKey;

            try
            {
                noKey = cert.PrivateKey == null;
            }
            catch (CryptographicException cre)
            {
                var account = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                throw new ConfigurationErrorsException(
                    $"'{account}' does not have access to the private key for the certificate.'{CertificateSubject}' " +
                     "This user should be given 'READ' permissions ONLY through mmc > all tasks > manage private keys");
            }

            if (noKey)
            {
                throw new ConfigurationErrorsException(
                    $"There is no private key in the certificate {CertificateSubject}");
            }

            return cert;
        }
    }
}