using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace CertificateManager.Src
{
	public class CertificateCreator
	{
		// Create self-signed certificate
		public static X509Certificate2 CreateSelfSignedCertificate(string subjectName, int keySize = 2048)
		{
			using (var rsa = new RSACryptoServiceProvider(keySize))
			{
				var request = new CertificateRequest(
					new X500DistinguishedName($"CN={subjectName}"),
					rsa,
					HashAlgorithmName.SHA256,
					RSASignaturePadding.Pkcs1);

				// Set certificate validity
				DateTimeOffset notBefore = DateTimeOffset.UtcNow;
				DateTimeOffset notAfter = notBefore.AddYears(1);
				var cert = request.CreateSelfSigned(notBefore, notAfter);

				return cert;
			}
		}
	}
}


