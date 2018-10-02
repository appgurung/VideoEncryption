using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEncryption.Interface;
using KellermanSoftware.NetEncryptionLibrary;

namespace VideoEncryption.Services
{
    public class BlowFishSvc : IEncrypDecrypt
    {
        Encryption oEncrypt = new Encryption();
        bool status = false;
        /// <summary>
        /// Use this method to decrypt an enceypted file output
        /// </summary>
        /// <param name="decryptionKey">This is the key used to enrypt file in first phase</param>
        /// <param name="inputFilePath">The location of the file to be decrypted</param>
        /// <param name="outputFilePath">The location where file will be decryoted too</param>
        /// <returns></returns>
        public bool Decrypt(string decryptionKey, string inputFilePath, string outputFilePath)
        {
            try
            {
               status = oEncrypt.DecryptFile(EncryptionProvider.Blowfish, decryptionKey, inputFilePath, outputFilePath);
                return status;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        /// <summary>
        /// Use this method to encrypt a file.
        /// </summary>
        /// <param name="encryptionKey">This is the key to be used to encrypt the file</param>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public bool Encrypt(string encryptionKey,string inputFilePath, string outputFilePath)
        {
            try
            {
               status = oEncrypt.EncryptFile(EncryptionProvider.Blowfish, encryptionKey, inputFilePath, outputFilePath);

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
