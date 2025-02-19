using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace CertificateManager.Src
{

	public class HttpClientFactory
	{
		private static HttpClient _httpClient;
		private static string url = "https://example.com/pgp-public-key.asc";

		static HttpClientFactory()
		{
			_httpClient = new HttpClient();
		}
		public static async Task<string> Get()
		{

			// Download PGP key from the URL
			var pgpKey = await DownloadPgpKeyFromUrl(url);

			if (pgpKey == null)
			{
				Console.WriteLine("Failed to download the PGP key.");
				return "null";
			}

			// Basic Verification
			if (VerifyPgpKey(pgpKey))
			{
				Console.WriteLine($"The PGP key is valid: {pgpKey}");
			}
			else
			{
				Console.WriteLine("The PGP key is not valid.");
			}
			return pgpKey;
		}
		// Method to download PGP key from URL
		public static async Task<string> DownloadPgpKeyFromUrl(string url)
		{
			try
			{

				var response = await _httpClient.GetStringAsync(url);
				return response; // Returns the PGP key as a string (PEM format)
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error downloading the PGP key: {ex.Message}");
				return null;
			}
		}
		public static bool VerifyPgpKey(string pgpKey)
		{
			try
			{
				using (var reader = new StringReader(pgpKey))
				{
					var pemReader = new PemReader(reader);
					var keyObject = pemReader.ReadObject();

					// Check if the key is a valid public key
					if (keyObject is RsaKeyParameters publicKey)
					{
						Console.WriteLine("Public Key Details:");
						Console.WriteLine($"Modulus: {publicKey.Modulus.ToString(16)}");
						Console.WriteLine($"Exponent: {publicKey.Exponent.ToString(16)}");
						return true;
					}
					else
					{
						Console.WriteLine("The key is not a valid public key.");
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error verifying the PGP key: {ex.Message}");
				return false;
			}
		}
	}
}
