using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace sparkim_passview
{
  internal static class Encryption
  {
    private const string KEY = "ugfpV1dMC5jyJtqwVAfTpHkxqJ0+E0ae";
    
    public static string Encrypt(string data)
    {
      var tdes = new TripleDESCryptoServiceProvider();
      tdes.Mode = CipherMode.ECB; //this the default used by java
      tdes.Padding = PaddingMode.PKCS7; //this should be 5, but 7 *should* work
      tdes.Key = Convert.FromBase64String(KEY);
      var encryptor = tdes.CreateEncryptor();

      var rawData = Encoding.UTF8.GetBytes(data);
      var result = encryptor.TransformFinalBlock(rawData, 0, rawData.Length);

      return Convert.ToBase64String(result);
    }

    public static string Decrypt(string data)
    {
      var tdes = new TripleDESCryptoServiceProvider();
      tdes.Mode = CipherMode.ECB; //this the default used by java
      tdes.Padding = PaddingMode.PKCS7; //this should be 5, but 7 *should* work
      tdes.Key = Convert.FromBase64String(KEY);
      var decryptor = tdes.CreateDecryptor();

      var rawData = Convert.FromBase64String(data);
      var result = decryptor.TransformFinalBlock(rawData, 0, rawData.Length);

      return Encoding.UTF8.GetString(result);
    }
  }
}
