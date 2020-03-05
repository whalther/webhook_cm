using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Utilities
{
   public class Cifrador
    {
		byte[] key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["llaveCifrado"]);
		public string Cifrar(string inputText, string ivE)

		{
			byte[] iv = Encoding.ASCII.GetBytes(ivE);
			byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
			byte[] encripted;
			RijndaelManaged cripto = new RijndaelManaged();
			cripto.GenerateIV();
			using (MemoryStream ms =

				new MemoryStream(inputBytes.Length))
			{
				using (CryptoStream objCryptoStream =
					new CryptoStream(ms,
						   cripto.CreateEncryptor(key, iv),
						   CryptoStreamMode.Write))
				{
					objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
					objCryptoStream.FlushFinalBlock();
					objCryptoStream.Close();
				}
				encripted = ms.ToArray();
			}
			return Convert.ToBase64String(encripted);
		}

		public string Descifrar(string inputText, string ivE)

		{
			byte[] iv = Encoding.ASCII.GetBytes(ivE);
			byte[] inputBytes = Convert.FromBase64String(inputText);
			byte[] resultBytes = new byte[inputBytes.Length];
			string textoLimpio = String.Empty;
			RijndaelManaged cripto = new RijndaelManaged();
			using (MemoryStream ms = new MemoryStream(inputBytes))

			{
				using (CryptoStream objCryptoStream =
				new CryptoStream(ms, cripto.CreateDecryptor(key, iv),
						CryptoStreamMode.Read))
				{
					using (StreamReader sr =
						new StreamReader(objCryptoStream, true))
					{
						textoLimpio = sr.ReadToEnd();
					}
				}
			}
			return textoLimpio;
		}

		public string generarIv()
		{
			var random = new Random();
			string result = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/", 16).Select(s => s[random.Next(s.Length)]).ToArray());
			return result;
		}
	}
}
