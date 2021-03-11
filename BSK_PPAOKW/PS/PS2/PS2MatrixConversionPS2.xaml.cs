using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS2MatrixConversionPS2.xaml
    /// </summary>
    public partial class PS2MatrixConversionPS2 : UserControl
    {
        public PS2MatrixConversionPS2()
        {
            InitializeComponent();
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            MatrixConversionPS2 matrixConversionPS2 = new MatrixConversionPS2(Encrypt_Key.Text.ToString());
            Encrypted_Result.Text = matrixConversionPS2.Encrypt(Encrypt_Text.Text.ToString());
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            MatrixConversionPS2 matrixConversionPS2 = new MatrixConversionPS2(Decrypt_Key.Text.ToString());
            Decrypted_Result.Text = matrixConversionPS2.Decrypt(Decrypt_Text.Text.ToString());
        }

        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {
            FileOpen.Open_file(Encrypt_file_name_textblock);
        }
        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {
            FileOpen.Open_file(Decrypt_file_name_textblock);
        }

        private void Encrypt_from_file(object sender, RoutedEventArgs e)
        {
            //List<string> ResultToFile = new List<string>();
            try
            {
                if (Encrypt_file_name_textblock.Text != "")
                {
                    List<string> WordsFromFile = System.IO.File.ReadAllLines(Encrypt_file_name_textblock.Text).ToList();
                    string result = "";
                    Encrypted_file_result.Text = "";
                    

                    foreach (string word in WordsFromFile)
                    {
                        MatrixConversionPS2 matrixConversionPS2 = new MatrixConversionPS2(Encrypt_file_key.Text.ToString());
                        string helper = matrixConversionPS2.Encrypt(word);
                        result += helper + "\n";
                        //ResultToFile.Add(helper);
                    }
                    Encrypted_file_result.Text = result;
                }
                else
                {
                    Encrypted_file_result.Text = "No file was given!";
                }

            }
            catch
            {
                if (Encrypt_file_name_textblock.Text != "")
                {
                    Encrypted_file_result.Text = "Wrong key!";
                }
                else
                {
                    Encrypted_file_result.Text = "No file was given!\nWrong key!";
                }
            }
            //using (StreamWriter file = new StreamWriter(Decrypt_file_name_textblock.Text.ToString()))
            //{
            //    foreach (var line in ResultToFile)
            //    {
            //        file.WriteLine(line);
            //    }
            //}
        }

        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Decrypt_file_name_textblock.Text != "")
                {
                    List<string> WordsFromFile = System.IO.File.ReadAllLines(Decrypt_file_name_textblock.Text).ToList();
                    string result = "";
                    Decrypted_file_result.Text = "";

                    foreach (string word in WordsFromFile)
                    {
                        MatrixConversionPS2 matrixConversionPS2 = new MatrixConversionPS2(Decrypt_file_key.Text.ToString());
                        result += matrixConversionPS2.Decrypt(word);
                        result += "\n";
                    }
                    Decrypted_file_result.Text = result;
                }
                else
                {
                    Decrypted_file_result.Text = "No file was given!";
                }

            }
            catch
            {
                if (Decrypt_file_name_textblock.Text != "")
                {
                    Decrypted_file_result.Text = "Wrong key";
                }
                else
                {
                    Decrypted_file_result.Text = "No file was given!\nWrong key!";
                }
            }
        }
    }
}
