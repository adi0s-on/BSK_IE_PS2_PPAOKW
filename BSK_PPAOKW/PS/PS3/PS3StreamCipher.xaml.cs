using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS3StreamCipher.xaml
    /// </summary>
    public partial class PS3StreamCipher : UserControl
    {
        public Lfsr LfsrMethod_Encrypt { get; set; }
        public Lfsr LfsrMethod_Decrypt { get; set; }
        public string keyEncryption = "";
        public string keyDecryption = "";
        public string filePathEncrypt = "";
        public string filePathDecrypt = "";
        public string fileExtension = "";
        bool[] KeySeed { get; set; }
        public string Seed{ get; set; }
        public PS3StreamCipher()
        {
            InitializeComponent();
            ProgressBar_Encrypt.Visibility = Visibility.Hidden;
            ProgressBar_Decrypt.Visibility = Visibility.Hidden;
            Result_Encrypt.Visibility = Visibility.Hidden;
            Result_Decrypt.Visibility = Visibility.Hidden;
            Encrypt_button.IsEnabled = false;
            Decrypt_button.IsEnabled = false;
            Encrypt_button.Background = Brushes.Gray;
            Decrypt_button.Background = Brushes.Gray;
            V_Encrypt.Visibility = Visibility.Hidden;
            V_Decrypt.Visibility = Visibility.Hidden;
        }

        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {

            if (!FileOpen.Open_file(Encrypt_file_name_textblock))
            {
                return;
            }
            
            string str = Encrypt_file_name_textblock.Text.Substring(Encrypt_file_name_textblock.Text.Length - 4);
            if(str != ".txt" && str != ".mp3" && str != ".jpg")
            {
                Encrypt_file_name_textblock.Text = "Wrong file format!";
                Encrypt_button.IsEnabled = false;
                Encrypt_button.Background = Brushes.Gray;
            }
            else
            {
                SolidColorBrush brushButtonActive = new SolidColorBrush(Color.FromRgb(0, 173, 181));
                Encrypt_button.IsEnabled = true;
                Encrypt_button.Background = brushButtonActive;
            }
        }

        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {

            if(!FileOpen.Open_file(Decrypt_file_name_textblock))
            {
                return;
            }

            string str = Decrypt_file_name_textblock.Text.Substring(Decrypt_file_name_textblock.Text.Length - 4);
            if (str != ".txt" && str != ".mp3" && str != ".jpg")
            {
                Decrypt_file_name_textblock.Text = "Wrong file format!";
                Decrypt_button.IsEnabled = false;
                Decrypt_button.Background = Brushes.Gray;
            }
            else
            {
                SolidColorBrush brushButtonActive = new SolidColorBrush(Color.FromRgb(0, 173, 181));
                Decrypt_button.IsEnabled = true;
                Decrypt_button.Background = brushButtonActive;
            }
        }

        private void Encrypt_from_file(object sender, RoutedEventArgs e)
        {
            string polynominal_encrypt = Polynomial_Encrypt.Text.ToString();
            int[] Powers = new int[polynominal_encrypt.Length];

            if(LfsrMethod_Encrypt == null)
            {
                int rowLenght = ReadPolynomial(polynominal_encrypt, Powers);
                ErrorTextBlock_Encrypt.Text = "";
                LfsrMethod_Encrypt = new Lfsr(rowLenght);
                LfsrMethod_Encrypt.IsStopped = false;
                ProgressBar_Encrypt.Visibility = Visibility.Visible;
                Result_Encrypt.Visibility = Visibility.Visible;
                V_Encrypt.Visibility = Visibility.Visible;

            }
            if(LfsrMethod_Encrypt != null)
            {
                LfsrMethod_Encrypt.IsStopped = false;
                filePathEncrypt = Encrypt_file_name_textblock.Text;
                fileExtension = filePathEncrypt.Substring(filePathEncrypt.Length - 4);
                new Thread(GenerateKeyForEncrypter).Start();
                Encrypt_button.IsEnabled = false;
                Encrypt_button.Background = Brushes.Gray;
            }    
        }

        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            string polynominal_decrypt = Polynomial_Decrypt.Text.ToString();
            int[] Powers = new int[polynominal_decrypt.Length];
            Seed = "";
            for (int i = 0; i < SeedTextBox.Text.Length; i++)
            {
                Seed += SeedTextBox.Text[i];
            }
            if (LfsrMethod_Decrypt == null)
            {
                int rowLength = ReadPolynomial(polynominal_decrypt, Powers);
                ErrorTextBlock_Decrypt.Text = "";
                LfsrMethod_Decrypt = new Lfsr(rowLength);
                LfsrMethod_Decrypt.Seed = new bool[LfsrMethod_Decrypt.RowLength];
                LfsrMethod_Decrypt.IsStopped = false;
                ProgressBar_Decrypt.Visibility = Visibility.Visible;
                Result_Decrypt.Visibility = Visibility.Visible;
                V_Decrypt.Visibility = Visibility.Visible;

            }
            if (LfsrMethod_Decrypt != null)
            {
                LfsrMethod_Decrypt.IsStopped = false;
                filePathDecrypt = Decrypt_file_name_textblock.Text;
                fileExtension = filePathDecrypt.Substring(filePathDecrypt.Length - 4);
                new Thread(GenerateKeyForDecrypter).Start();
                Decrypt_button.IsEnabled = false;
                Decrypt_button.Background = Brushes.Gray;
                for (int i = 0; i < Seed.Length; i++)
                {
                    if(Seed[i] == '1')
                    {
                        LfsrMethod_Decrypt.Seed[i] = true;
                    }
                    else
                    {
                        LfsrMethod_Decrypt.Seed[i] = false;
                    }
                }
            }
        }

        public void GenerateKeyForEncrypter()
        {
            byte[] bytesFromFileEncrypt = System.IO.File.ReadAllBytes(filePathEncrypt);
            BitArray bits = new BitArray(bytesFromFileEncrypt);
            bool[] emptyByteArray = new bool[bits.Length];
            try
            {
                int count = 0;
                while (count < bits.Length)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        bool row = LfsrMethod_Encrypt.AddRow();
                        bool toAdd = false;
                        if (row != bits[count])
                        {
                            toAdd = true;
                        }
                        emptyByteArray[count] = toAdd;
                        count++;
                    });
                }
                byte[] xd = new byte[bytesFromFileEncrypt.Length];
                BitArray bitsToReturn = new BitArray(emptyByteArray);

                bitsToReturn.CopyTo(xd, 0);
                SaveFileDialog sfd = new SaveFileDialog();
                switch(fileExtension)
                {
                    case ".mp3":
                    {
                            sfd.Filter = "MP3 file|*.mp3";
                            sfd.Title = "Save an encrypted MP3 file";
                            break;
                    }
                    case ".jpg":
                    {
                            sfd.Filter = "JPeg Image|*.jpg";
                            sfd.Title = "Save an encrypted image file";
                            break;
                    }
                    case ".txt":
                    {
                            sfd.Filter = "Txt file|*.txt";
                            sfd.Title = "Save an encrypted text file";
                            break;
                    }
                    default:
                    {
                        break;
                    }
                }
                KeySeed = new bool[LfsrMethod_Encrypt.KeySeed.Length];
                for (int i = 0; i < LfsrMethod_Encrypt.KeySeed.Length; i++)
                {
                    KeySeed[i] = LfsrMethod_Encrypt.KeySeed[i];
                }
                bool? response = sfd.ShowDialog();
                if (response == true)
                {
                    string filePathToSave = sfd.FileName;
                    System.IO.File.WriteAllBytes(filePathToSave, xd);
                    LfsrMethod_Encrypt = null;
                }
            }
            catch
            {
                ErrorTextBlock_Encrypt.Text = "Unknown Error has occured!";
            }

        }

        public void GenerateKeyForDecrypter()
        {
            byte[] bytesFromFileDecrypt = System.IO.File.ReadAllBytes(filePathDecrypt);
            BitArray bits = new BitArray(bytesFromFileDecrypt);
            bool[] emptyByteArray = new bool[bits.Length];
           
            try
            { 
                int count = 0;
                while (count < bits.Length)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        bool row = LfsrMethod_Decrypt.AddRowDecrypt();
                        bool toAdd = false;
                        if (row != bits[count])
                        {
                            toAdd = true;
                        }
                        emptyByteArray[count] = toAdd;
                        count++;
                    });
                }
                byte[] xd = new byte[bytesFromFileDecrypt.Length];
                BitArray bitsToReturn = new BitArray(emptyByteArray);

                bitsToReturn.CopyTo(xd, 0);
                SaveFileDialog sfd = new SaveFileDialog();
                switch (fileExtension)
                {
                    case ".mp3":
                        {
                            sfd.Filter = "MP3 file|*.mp3";
                            sfd.Title = "Save an encrypted MP3 file";
                            break;
                        }
                    case ".jpg":
                        {
                            sfd.Filter = "JPeg Image|*.jpg";
                            sfd.Title = "Save an encrypted image file";
                            break;
                        }
                    case ".txt":
                        {
                            sfd.Filter = "Txt file|*.txt";
                            sfd.Title = "Save an encrypted text file";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                bool? response = sfd.ShowDialog();
                if (response == true)
                {
                    string filePathToSave = sfd.FileName;
                    System.IO.File.WriteAllBytes(filePathToSave, xd);
                    LfsrMethod_Decrypt = null;
                }
            }
            catch
            {
                ErrorTextBlock_Encrypt.Text = "Unknown Error has occured!";
            }
        }

        private int ReadPolynomial(string polynomial, int[] Powers)
        {
            try
            {
                int counter = 0;
                for (int i = 0; i < polynomial.Length; i++)
                {
                    if (polynomial[i] == '^')
                    {
                        int counter2 = 0;
                        while (Int32.TryParse(polynomial[i + 1 + counter2].ToString(), out int nothing))
                        {
                            Powers[counter] = Int32.Parse(Powers[counter].ToString() + polynomial[i + 1 + counter2].ToString());
                            counter2++;
                            if (i + 1 + counter2 == polynomial.Length)
                            {
                                break;
                            }
                        }
                        counter++;
                    }
                }
            }
            catch
            {
                ErrorTextBlock_Decrypt.Text = "WRONG polynominal!";
            }
            return Powers.Max();
        }
        private void ShowSeed(object sender, RoutedEventArgs e)
        {
            if (KeySeed != null)
            {
                V_Encrypt.Text = "";
                for (int i = 0; i < KeySeed.Length; i++)
                {
                    string number = "";
                    if(KeySeed[i])
                    {
                        number = "1";
                    }
                    else
                    {
                        number = "0";
                    }
                    V_Encrypt.Text += number + " ";
                }
            }
            else
            {
                V_Encrypt.Text = "Seed is not ready yet!";
            }
        }
    }
}
