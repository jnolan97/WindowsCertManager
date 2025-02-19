using System.Security.Cryptography.X509Certificates;

namespace CertificateManager.Src
{

	public class CertificateReader
	{
		// Read certificate from PFX file
		public static X509Certificate2 ReadCertificateFromPfx(string pfxPath, string password)
		{
			return new X509Certificate2(pfxPath, password);
		}

		// Read certificate from PEM file (for this, you need to install BouncyCastle or use OpenSSL)
		public static X509Certificate2 ReadCertificateFromPem(string pemPath)
		{
			// PEM format certificates would require parsing with an external library like BouncyCastle
			throw new NotImplementedException("PEM parsing is not implemented here. Use an external library.");
		}

		// Read certificate from CRT file
		public static X509Certificate2 ReadCertificateFromCrt(string crtPath)
		{
			return new X509Certificate2(crtPath);
		}
	}
}
