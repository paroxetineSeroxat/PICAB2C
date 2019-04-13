using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB.Business.Util.Cryptography
{
    public class EncryptionFactory
    {
        #region Singleton

        private static volatile EncryptionFactory instancia;
        private static object syncRoot = new Object();

        public static EncryptionFactory Instancia
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new EncryptionFactory();
                    }
                }
                return instancia;
            }
        }

        #endregion Singleton

        /// <summary>
        /// Obtiene el proveedor de cifrado solicitado
        /// </summary>
        /// <param name="proveedor">Proveedor de cifrado</param>
        /// <returns></returns>
        public IEncryptionFactory GetProvider(EncryptionProvider proveedor)
        {
            IEncryptionFactory resultado = null;
            switch (proveedor)
            {
                case EncryptionProvider.Aes:
                    resultado = new AesEncryptionProvider();
                    break;
                case EncryptionProvider.Rsa:
                    resultado = new RsaEncryptionProvider();
                    break;
                case EncryptionProvider.TripeDes: 
                     resultado = new TripeDESEncryptionProvider();
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (resultado == null)
                throw new NotImplementedException();
            return resultado;
        }
    }
}
