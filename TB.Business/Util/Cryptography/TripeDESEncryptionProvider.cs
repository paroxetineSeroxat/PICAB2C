using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TB.Business.Util.Cryptography
{
    internal class TripeDESEncryptionProvider : IEncryptionFactory
    {

        //private LogContext logContext;

        private readonly TripleDESCryptoServiceProvider _des = new TripleDESCryptoServiceProvider();
        private readonly UTF8Encoding _utf8 = new UTF8Encoding();
        private byte[] _keyByte = { }; 
        //Default Key
        private static string _key = "Pass@123#"; 
        //Default initial vector
        private byte[] _ivByte = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 }; 

        public string Key { get; set; }

        public string IV { get; set; }

        public string Encrypt(string cadena)
        {

            string encryptValue = string.Empty;
            MemoryStream ms = null;
            CryptoStream cs = null;
            if (!string.IsNullOrEmpty(cadena))
            {
                try
                {
                    if (!string.IsNullOrEmpty(Key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (Key.Substring(0, 8));
                        if (!string.IsNullOrEmpty(IV))
                        {
                            _ivByte = Encoding.UTF8.GetBytes
                                (IV.Substring(0, 8));
                        }
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        byte[] inputByteArray =
                            Encoding.UTF8.GetBytes(cadena);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateEncryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        encryptValue = Convert.ToBase64String(ms.ToArray());
                    }
                }
                catch(Exception ex)
                {
                    //SaveExceptionLog(ex.Message, ex.StackTrace, "TripeDESEncryptionProvider", "Cifrar", LogTriggerTypeConst.EXCEPTION, "", "", cadena, LogUserConst.SYSTEM, Guid.NewGuid().ToString());
                    throw new Exception("La cadena no se puede descifrar bajo el algoritmo DES.");
                    //TODO: write log 
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return encryptValue; 
        }

        public string Decrypting(string cadena)
        {
            string decrptValue = string.Empty;
            if (!string.IsNullOrEmpty(cadena))
            {
                MemoryStream ms = null;
                CryptoStream cs = null;
                cadena = cadena.Replace(" ", "+");
                byte[] inputByteArray = new byte[cadena.Length];
                try
                {
                    if (!string.IsNullOrEmpty(Key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (Key.Substring(0, 8));
                        if (!string.IsNullOrEmpty(IV))
                        {
                            _ivByte = Encoding.UTF8.GetBytes
                                (IV.Substring(0, 8));
                        }
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        inputByteArray = Convert.FromBase64String(cadena);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateDecryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        Encoding encoding = Encoding.UTF8;
                        decrptValue = encoding.GetString(ms.ToArray());
                    }
                }
                 
                catch (Exception ex)
                {
                    //SaveExceptionLog(ex.Message, ex.StackTrace, "TripeDESEncryptionProvider", "Descifrar", LogTriggerTypeConst.EXCEPTION, "", "",cadena, LogUserConst.SYSTEM, Guid.NewGuid().ToString());
                    throw new  Exception("La cadena no se puede descifrar bajo el algoritmo DES.");
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return decrptValue; 
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }


        #region IBusnessObject

        public void SaveExceptionLog(string exceptionMessage, string stackTrace,string className, string method, string type, string responseMessage, string responseId, string parametersMessage, string user, string EventId)
        {

        }

        public void SaveEventlogs( string className, string method, string type, string responseMessage, string responseId, string parametersMessage, string user, out string eventId)
        {
            eventId = Guid.NewGuid().ToString();
        }

        public void SaveEventlog(string className, string method, string type, string responseMessage, string responseId, string parametersMessage, string user, string eventId)
        {

        }

        #endregion IBusnessObject

    }
}
