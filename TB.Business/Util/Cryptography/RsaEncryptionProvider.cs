using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections;

namespace TB.Business.Util.Cryptography
{
    /// <summary>
    /// Representa una empresa
    /// Autor: Nelson Javier Novoa Tellez
    /// Fecha: 12/11/2013
    /// Version: 1.0
    /// Modificado por:
    /// Fecha Modificación:
    /// </summary>
    internal class RsaEncryptionProvider : IEncryptionFactory
    {
        public string Key { get; set; }

        public string IV { get; set; }
        /*
        public string Key
        {
            get
            {
                return Key;//throw new NotImplementedException();
            }
            set
            {
                Key = value;//throw new NotImplementedException();
            }
        }

        public string IV
        {
            get
            {
                return IV;// throw new NotImplementedException();
            }
            set
            {
                IV = value;// throw new NotImplementedException();
            }
        }*/

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RsaEncryptionProvider()
        {

        }

        /// <summary>
        /// Cifra el texto indicado, utiliza algoritmo RSA
        /// </summary>
        /// <param name="cadena">Texto a encriptar</param>
        /// <returns>Texto cifrado</returns>
        public string Encrypt(string cadena)
        {
            string fileString = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            //MemoryStream memStream = new MemoryStream(Properties.Resources.PublicKey);
            MemoryStream memStream = new MemoryStream();
            StreamReader streamReader = new StreamReader(memStream);
            fileString = streamReader.ReadToEnd();
            streamReader.Close();

            if (!string.IsNullOrEmpty(fileString))
            {
                string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                fileString = fileString.Replace(bitStrengthString, "");
                int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
                RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(bitStrength);
                rsaCryptoServiceProvider.FromXmlString(fileString);
                int keySize = bitStrength / 8;
                byte[] bytes = Encoding.UTF32.GetBytes(cadena);
                int maxLength = keySize - 42;
                int dataLength = bytes.Length;
                int iterations = dataLength / maxLength;
                for (int i = 0; i <= iterations; i++)
                {
                    byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                    byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                    // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the DecryptString function.
                    Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Descifra el texto indicado, utiliza algoritmo RSA
        /// </summary>
        /// <param name="texto">Texto encriptado</param>
        /// <returns>Texto plano<returns>
        public string Decrypting(string cadena)
        {
            string fileString = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            //MemoryStream memStream = new MemoryStream(Properties.Resources.PrivateKey);
            MemoryStream memStream = new MemoryStream();
            StreamReader streamReader = new StreamReader(memStream);
            fileString = streamReader.ReadToEnd();
            streamReader.Close();
            string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
            fileString = fileString.Replace(bitStrengthString, "");
            int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(bitStrength);
            rsaCryptoServiceProvider.FromXmlString(fileString);
            int base64BlockSize = ((bitStrength / 8) % 3 != 0) ? (((bitStrength / 8) / 3) * 4) + 4 : ((bitStrength / 8) / 3) * 4;
            int iterations = cadena.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(cadena.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }


    }
}
