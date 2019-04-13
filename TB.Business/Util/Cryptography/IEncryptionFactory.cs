using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB.Business.Util.Cryptography
{
    public interface IEncryptionFactory
    {
        string Key { get; set; }

        string IV { get; set; }

        string Encrypt(string str);

        string Decrypting(string str);
    }
}
