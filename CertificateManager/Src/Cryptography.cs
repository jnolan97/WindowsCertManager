using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace CertificateManager.Src
{

	public static class PgpKeyGenerator
	{
		public static (string PublicKey, string PrivateKey) GeneratePgpKeyPair()
		{
			// Generate PGP key pair (RSA in this example)
			var keyPairGenerator = new RsaKeyPairGenerator();
			keyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
			var keyPair = keyPairGenerator.GenerateKeyPair();

			using (var publicKeyStream = new StringWriter())
			using (var privateKeyStream = new StringWriter())
			{
				var publicKeyWriter = new PemWriter(publicKeyStream);
				publicKeyWriter.WriteObject(keyPair.Public);
				var privateKeyWriter = new PemWriter(privateKeyStream);
				privateKeyWriter.WriteObject(keyPair.Private);

				return (publicKeyStream.ToString(), privateKeyStream.ToString());
			}
		}
	}

	public static class PgpEncryptDecrypt
	{
		public static string Encrypt(string data, string publicKey)
		{
			var publicKeyReader = new StringReader(publicKey);
			var pemReader = new PemReader(publicKeyReader);
			var pubKey = (RsaKeyParameters)pemReader.ReadObject();

			using (var rsa = new RSACryptoServiceProvider())
			{
				rsa.ImportParameters(new RSAParameters
				{
					Modulus = pubKey.Modulus.ToByteArray(),
					Exponent = pubKey.Exponent.ToByteArray()
				});

				var dataBytes = Encoding.UTF8.GetBytes(data);
				var encryptedData = rsa.Encrypt(dataBytes, false);
				return Convert.ToBase64String(encryptedData);
			}
		}

		public static string Decrypt(string encryptedData, string privateKey)
		{
			var privateKeyReader = new StringReader(privateKey);
			var pemReader = new PemReader(privateKeyReader);
			return "Not Implemented Yet";
			//var privKey = (RsaPrivateKeyParameters)pemReader.ReadObject();

			//using (var rsa = new RSACryptoServiceProvider())
			//{
			//	rsa.ImportParameters(new RSAParameters
			//	{
			//		Modulus = privKey.Modulus.ToByteArray(),
			//		Exponent = privKey.PublicExponent.ToByteArray(),
			//		D = privKey.Exponent.ToByteArray(),
			//		DP = privKey.P.ToByteArray(),
			//		DQ = privKey.Q.ToByteArray(),
			//		InverseQ = privKey.QInv.ToByteArray(),
			//		P = privKey.P.ToByteArray(),
			//		Q = privKey.Q.ToByteArray()
			//	});

			//	var encryptedBytes = Convert.FromBase64String(encryptedData);
			//	var decryptedData = rsa.Decrypt(encryptedBytes, false);
			//	return Encoding.UTF8.GetString(decryptedData);
			//}
		}
	}
}
