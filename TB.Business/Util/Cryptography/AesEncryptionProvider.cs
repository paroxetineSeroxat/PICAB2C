using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TB.Business.Util.Cryptography
{
    internal class AesEncryptionProvider : IEncryptionFactory
    {
        public string Key { get; set; }

        public string IV { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AesEncryptionProvider()
        {

        }

        public string Encrypt(string str)
        {
            string result = string.Empty;
            using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
            {
                myAes.Mode = CipherMode.CBC;
                myAes.Key = Convert.FromBase64String(Key);
                myAes.IV = Convert.FromBase64String(IV);

                ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);
                byte[] encrypted;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(str);
                        }
                        encrypted = msEncrypt.ToArray();

                        result = Convert.ToBase64String(encrypted);
                    }
                }

            }
            return result;
        }

        public string Decrypting(string str)
        {
            ////////////////////////////////////////////
            //string dummyData = cadena.Trim().Replace(" ", "+");
            //if (dummyData.Length % 4 > 0)
            //    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
            ///////////////////////////////////////////


            byte[] encryption = Convert.FromBase64String(str);
            //byte[] cifrado = Convert.FromBase64String(dummyData);
            string result = string.Empty;
            using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
            {
                myAes.Mode = CipherMode.CBC;
                myAes.Key = Convert.FromBase64String(Key);
                myAes.IV = Convert.FromBase64String(IV);

                ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);

                using (MemoryStream msEncrypt = new MemoryStream(encryption))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return result;
        }
    }
}
