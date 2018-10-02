using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoEncryption.Services;
using VideoEncryption;
namespace VideoENcryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
       
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog()
            {
               InitialDirectory = @"C:\",
               Title = "Browse Video Files",
               CheckFileExists = true,
               CheckPathExists = true,
               DefaultExt = "txt",
               Filter = "Video files (*.mp4)|*.mp4|All files (*.*)|*.*",
               FilterIndex = 2,
               RestoreDirectory = true,
               ReadOnlyChecked = true,
               ShowReadOnly = true,
               AddExtension = true
        };
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text= openFileDialog1.FileName;

                if (!String.IsNullOrEmpty(textBox4.Text)
                    && (textBox4.Text.Contains("BlowFish Encrypted")
                    || textBox4.Text.Contains("TrippleDES Encrypted")
                    || textBox4.Text.Contains("Blowfish & 3DES Encrypted")))
                {
                    button3.Enabled = true;
                    button2.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                radioButton2.Checked = false ? radioButton3.Checked : false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                radioButton1.Checked = false ? radioButton3.Checked : false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                radioButton1.Checked = false ? radioButton2.Checked : false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                MessageBox.Show("Please select an encryption type from radio button");

            if (radioButton1.Checked)
            {
                BlowFishSvc svc = new BlowFishSvc();
                var watch = System.Diagnostics.Stopwatch.StartNew();

                if (svc.Encrypt(Constants.encryptionKey, textBox4.Text, Constants.encryptedBlowfishPath) == true)
                {
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    textBox1.Text = elapsedMs.ToString();
                    textBox8.Text = new FileInfo(Constants.encryptedBlowfishPath).Length.ToString();
                    textBox5.Text = Constants.encryptedBlowfishPath;
                    MessageBox.Show("File Encrypted Successfully");        
                    textBox4.Text = "";
                    button2.Enabled = false;
                }
                else
                    MessageBox.Show("File Encrypted Failed");

                return;
            }

            if (radioButton2.Checked)
            {
                TrippleDesSvc svc = new TrippleDesSvc();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                if (svc.Encrypt(Constants.encryptionKey, textBox4.Text, Constants.encryptedTrippleDESPath) == true)
                {
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    textBox2.Text = elapsedMs.ToString();
                    textBox5.Text = Constants.encryptedTrippleDESPath;
                    textBox7.Text = new FileInfo(Constants.encryptedTrippleDESPath).Length.ToString();
                    MessageBox.Show("File Encrypted Successfully");               
                    textBox4.Text = "";
                    button2.Enabled = false;
                }
                else
                    MessageBox.Show("File Encryption Failed");

                return;
            }

            if(radioButton3.Checked)
            {
                BlowFish3DesSvc blow3desSvc = new BlowFish3DesSvc();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                if (blow3desSvc.Encrypt(Constants.encryptionKey, textBox4.Text, Constants.phase1_encryptedBlowFishTrippleDESPath) == true) {
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    textBox3.Text = elapsedMs.ToString();
                    File.Delete(Constants.phase1_encryptedBlowFishTrippleDESPath);
                    textBox5.Text = Constants.encryptedTrippleDESPath;
                    textBox6.Text = new FileInfo(Constants.encryptedBlowFishTrippleDESPath).Length.ToString();
                    MessageBox.Show("File Encrypted Successfully");
                    textBox4.Text = "";
                    button2.Enabled = false;
                }
                else
                    MessageBox.Show("File Encryption Failed");
                return;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("WELCOME TO VIDEO ENCRYPTION PROGRAM", "ENCRYPTION SOFTWARE", MessageBoxButtons.OK);
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox4.Text.Contains("BlowFish Encrypted"))
            {
                BlowFishSvc svc = new BlowFishSvc();

                if (svc.Decrypt(Constants.encryptionKey, textBox4.Text, Constants.decryptedBlowfishPath) == true) {
                    textBox5.Text = Constants.decryptedBlowfishPath;
                    MessageBox.Show("File Decrypted");
                    textBox4.Text = "";
                    button3.Enabled = false;            
                }
                return;
            }

            if(textBox4.Text.Contains("TrippleDES Encrypted"))
            {
                TrippleDesSvc svc = new TrippleDesSvc();

                if (svc.Decrypt(Constants.encryptionKey, textBox4.Text, Constants.decryptedTrippleDESPath) == true)
                {
                    textBox5.Text = Constants.decryptedTrippleDESPath;
                    MessageBox.Show("File Decrypted");
                    textBox4.Text = "";
                    button3.Enabled = false;            
                }
                return;
            }

            if(textBox4.Text.Contains("Blowfish & 3DES Encrypted"))
            {
                BlowFish3DesSvc blow3DesSvc = new BlowFish3DesSvc();
                if(blow3DesSvc.Decrypt(Constants.encryptionKey, textBox4.Text, Constants.phase1_decryptedBlowFishTrippleDESPath) == true)
                {
                    File.Delete(Constants.phase1_decryptedBlowFishTrippleDESPath);
                    textBox5.Text = Constants.decryptedTrippleDESPath;
                    MessageBox.Show("File Decrypted");
                    textBox4.Text = "";
                    button3.Enabled = false;
                }
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo AccountDirectoryForUsers = Directory.CreateDirectory(@"C:\\Project Encryption");

            //Decrypted Directories
            AccountDirectoryForUsers.CreateSubdirectory("Decrypted").CreateSubdirectory("Blowfish & TrippleDES Decrypted");
            AccountDirectoryForUsers.CreateSubdirectory("Decrypted").CreateSubdirectory("BlowFish Decrypted");
            AccountDirectoryForUsers.CreateSubdirectory("Decrypted").CreateSubdirectory("TrippleDES Decrypted");


            //Encrypted Directories
            AccountDirectoryForUsers.CreateSubdirectory("Encrypted").CreateSubdirectory("Blowfish & 3DES Encrypted");
            AccountDirectoryForUsers.CreateSubdirectory("Encrypted").CreateSubdirectory("BlowFish Encrypted");
            AccountDirectoryForUsers.CreateSubdirectory("Encrypted").CreateSubdirectory("TrippleDES Encrypted");

            MessageBox.Show("Application folders created, software ready for use...Good luck!!!", "INFORMATION", MessageBoxButtons.OK);
        }
    }
}

