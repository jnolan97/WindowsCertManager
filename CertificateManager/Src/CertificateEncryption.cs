using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace CertificateManager.Src
{
	public class CertificateEncryption
	{
		// Encrypt data with public key
		public static byte[] EncryptData(string data, X509Certificate2 cert)
		{
			using (RSA rsa = cert.GetRSAPublicKey())
			{
				return rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.OaepSHA256);
			}
		}

		// Decrypt data with private key
		public static string DecryptData(byte[] encryptedData, X509Certificate2 cert)
		{
			using (RSA rsa = cert.GetRSAPrivateKey())
			{
				byte[] decryptedBytes = rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
				return System.Text.Encoding.UTF8.GetString(decryptedBytes);
			}
		}
	}
}
