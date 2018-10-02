using KellermanSoftware.NetEncryptionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEncryption.Interface;

namespace VideoEncryption.Services
{
    public class TrippleDesSvc : IEncrypDecrypt
    {
        Encryption oEncrypt = new Encryption();
        bool status = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptionKey"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public bool Decrypt(string decryptionKey, string inputFilePath, string outputFilePath)
        {
            try
            {
                status  = oEncrypt.DecryptFile(EncryptionProvider.TripleDES, decryptionKey, inputFilePath, outputFilePath);
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptionKey"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public bool Encrypt(string encryptionKey, string inputFilePath, string outputFilePath)
        {
            try
            {
               status =  oEncrypt.EncryptFile(EncryptionProvider.TripleDES, encryptionKey, inputFilePath, outputFilePath);


                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
