using System;
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
            FileOpen.Open_file(Encrypt_file_name_textblock);
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
            FileOpen.Open_file(Decrypt_file_name_textblock);
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
            int counter = 0;

            if(LfsrMethod_Encrypt == null)
            {
                try
                {
                    for (int i = 0; i < polynominal_encrypt.Length; i++)
                    {
                        if (polynominal_encrypt[i] == '^')
                        {
                            int counter2 = 0;
                            while (Int32.TryParse(polynominal_encrypt[i + 1 + counter2].ToString(), out int nothing))
                            {
                                Powers[counter] = Int32.Parse(Powers[counter].ToString() + polynominal_encrypt[i + 1 + counter2].ToString());
                                counter2++;
                                if (i + 1 + counter2 == polynominal_encrypt.Length)
                                {
                                    break;
                                }
                            }
                            counter++;
                        }
                    }
                    ErrorTextBlock_Encrypt.Text = "";
                    LfsrMethod_Encrypt = new Lfsr(Powers.Max());
                    LfsrMethod_Encrypt.IsStopped = false;
                    ProgressBar_Encrypt.Visibility = Visibility.Visible;
                    Result_Encrypt.Visibility = Visibility.Visible;
                    V_Encrypt.Visibility = Visibility.Visible;

                }
                catch
                {
                    ErrorTextBlock_Encrypt.Text = "WRONG polynominal!";
                }
            }
            if(LfsrMethod_Encrypt != null)
            {
                LfsrMethod_Encrypt.IsStopped = false;
                new Thread(GenerateKeyForEncrypter).Start();
                Encrypt_button.IsEnabled = false;
                Encrypt_button.Background = Brushes.Gray;
            }    
        }

        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            string polynominal_decrypt = Polynomial_Decrypt.Text.ToString();
            int[] Powers = new int[polynominal_decrypt.Length];
            int counter = 0;

            if (LfsrMethod_Decrypt == null)
            {
                try
                {
                    for (int i = 0; i < polynominal_decrypt.Length; i++)
                    {
                        if (polynominal_decrypt[i] == '^')
                        {
                            int counter2 = 0;
                            while (Int32.TryParse(polynominal_decrypt[i + 1 + counter2].ToString(), out int nothing))
                            {
                                Powers[counter] = Int32.Parse(Powers[counter].ToString() + polynominal_decrypt[i + 1 + counter2].ToString());
                                counter2++;
                                if (i + 1 + counter2 == polynominal_decrypt.Length)
                                {
                                    break;
                                }
                            }
                            counter++;
                        }
                    }
                    ErrorTextBlock_Decrypt.Text = "";
                    LfsrMethod_Decrypt = new Lfsr(Powers.Max());
                    LfsrMethod_Decrypt.IsStopped = false;
                    ProgressBar_Decrypt.Visibility = Visibility.Visible;
                    Result_Decrypt.Visibility = Visibility.Visible;
                    V_Decrypt.Visibility = Visibility.Visible;

                }
                catch
                {
                    ErrorTextBlock_Decrypt.Text = "WRONG polynominal!";
                }
            }
            if (LfsrMethod_Decrypt != null)
            {
                LfsrMethod_Decrypt.IsStopped = false;
                new Thread(GenerateKeyForDecrypter).Start();
                Decrypt_button.IsEnabled = false;
                Decrypt_button.Background = Brushes.Gray;
            }
        }

        public void GenerateKeyForEncrypter()
        {

            try
            {
                while (!LfsrMethod_Encrypt.IsStopped)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        bool row = LfsrMethod_Encrypt.AddRow();
                        string toAdd = "";

                        if (row)
                        {
                            toAdd = "1";
                        }
                        else
                        {
                            toAdd = "0";
                        }

                        if (keyEncryption.Length >= 11)
                        {
                            keyEncryption = keyEncryption.Substring(1);
                            keyEncryption += toAdd;
                        }
                        else keyEncryption += toAdd;

                        Key_Encrypt.Text = keyEncryption;
                    });

                    Thread.Sleep(120);
                }
            }
            catch
            {
                ErrorTextBlock_Encrypt.Text = "Unknown Error has occured!";
            }
        }

        public void GenerateKeyForDecrypter()
        {

            try
            {
                while (!LfsrMethod_Decrypt.IsStopped)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        bool row = LfsrMethod_Decrypt.AddRow();
                        string toAdd = "";

                        if (row)
                        {
                            toAdd = "1";
                        }
                        else
                        {
                            toAdd = "0";
                        }

                        if (keyDecryption.Length >= 11)
                        {
                            keyDecryption = keyEncryption.Substring(1);
                            keyDecryption += toAdd;
                        }
                        else keyDecryption += toAdd;

                        Key_Decrypt.Text = keyDecryption;
                    });

                    Thread.Sleep(120);
                }
            }
            catch
            {
                ErrorTextBlock_Decrypt.Text = "Unknown Error has occured!";
            }
        }

    }
}
