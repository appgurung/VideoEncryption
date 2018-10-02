using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KellermanSoftware.NetEncryptionLibrary;
using VideoEncryption.Interface;

namespace VideoEncryption.Services
{
    public class BlowFish3DesSvc
    {
        Encryption oEncrypt = new Encryption();
        bool status = false;

        public bool Encrypt(string encryptionKey, string inputFilePath, string outputFilePath)
        {
            try
            {
                BlowFishSvc blowFishSvc = new BlowFishSvc();
                TrippleDesSvc trippleDesSvc = new TrippleDesSvc();

                if (blowFishSvc.Encrypt(encryptionKey, inputFilePath, outputFilePath) == true)
                    if (trippleDesSvc.Encrypt(encryptionKey, outputFilePath, Constants.encryptedBlowFishTrippleDESPath) == true)
                        return status = true;

                return false;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public bool Decrypt(string encryptionKey, string inputFilePath, string outputFilePath)
        {
            try
            {
                BlowFishSvc blowFishSvc = new BlowFishSvc();
                TrippleDesSvc trippleDesSvc = new TrippleDesSvc();
                if (trippleDesSvc.Decrypt(encryptionKey, inputFilePath, outputFilePath) == true)
                    if (blowFishSvc.Decrypt(encryptionKey, outputFilePath, Constants.decryptedBlowFishTrippleDESPath) == true)
                        return status = true;

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

    }
}
