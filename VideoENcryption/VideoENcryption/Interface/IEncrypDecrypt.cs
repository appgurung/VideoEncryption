using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEncryption.Interface
{
    public interface IEncrypDecrypt
    {

         bool Encrypt(string encryptionKey, string inputFilePath, string outputFilePath);
         bool Decrypt(string decryptionKey, string inputFilePath, string outputFilePath);
    }
}
