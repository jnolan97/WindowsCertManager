using CertificateManager.Src;

namespace CertificateManager
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var cert = CertificateCreator.CreateSelfSignedCertificate("TestCert");
				Console.WriteLine($"Certificate Created: {cert.Subject}");

				// Encrypt and Decrypt with self-signed certificate
				string originalData = "Command line test...";
				var encryptedData = CertificateEncryption.EncryptData(originalData, cert);
				string decryptedData = CertificateEncryption.DecryptData(encryptedData, cert);

				Console.WriteLine($"Decrypted Data: {decryptedData}");
				Console.WriteLine($"Beginnging PGP Test {DateTime.UtcNow}");
				Console.WriteLine("Starting PGP Key Generation...");


				var pgpUrl = Task.Run(GetHttpReturn).GetAwaiter().GetResult();
				Console.WriteLine(pgpUrl);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Caught Error in Main {ex.Message}.....{ex.StackTrace}");
			}
		}
		public static async Task<string> GetHttpReturn()
		{
			// URL of the PGP key (e.g., a public key file hosted online)
			// url = "https://example.com/pgp-public-key.asc";
			var result = await HttpClientFactory.Get();
			var pgpKey = HttpClientFactory.VerifyPgpKey(result);
			Console.WriteLine($"PGP Key From URL: {result}");
			Console.WriteLine($"Cont.: {pgpKey}");
			return result;
		}
	}
}
