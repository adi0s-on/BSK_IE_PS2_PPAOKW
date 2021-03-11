using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS1MatrixConversions.xaml
    /// </summary>
    public partial class PS1MatrixConversions : UserControl
    {
        public PS1MatrixConversions()
        {
            InitializeComponent();
        }
        private void Encrypt(object sender, RoutedEventArgs e)
        {
            if (Encrypt_Text.Text != "")
            {
                MatrixConversion matrixConversion = new MatrixConversion(Encrypt_Text.Text.ToString(), Encrypt_Key.Text.ToString(), true);
                Encrypted_Result.Text = matrixConversion.Encrypt();
            }
            else if (Encrypt_Key.Text == "") Encrypted_Result.Text = "Please enter text and key in adjacent windows";
            else Encrypted_Result.Text = "Please enter text in adjacent window";
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            if (Decrypt_Text.Text != "")
            {
                MatrixConversion matrixConversion = new MatrixConversion(Decrypt_Text.Text.ToString(), Decrypt_Key.Text.ToString(), false);
                Decrypted_Result.Text = matrixConversion.Decrypt();
            }
            else if (Decrypt_Key.Text == "") Decrypted_Result.Text = "Please enter text and key in adjacent windows";
            else Decrypted_Result.Text = "Please enter text in adjacent window";

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
            try
            { 
                if (Encrypt_file_name_textblock.Text != "")
                {

                    List<string> WordsFromFile = System.IO.File.ReadAllLines(Encrypt_file_name_textblock.Text).ToList();
                    string result = "";
                    foreach (var word in WordsFromFile)
                    {
                        MatrixConversion matrixConversion = new MatrixConversion(word, Encrypt_file_key.Text.ToString(),true);
                        result += matrixConversion.Encrypt() + "\n";
                    }
                    Encrypted_file_result.Text = result;
                }
                else if (Encrypt_file_key.Text == "")
                {
                    Encrypted_file_result.Text = "No file was given!\nPlease enter key.";
                }
                else Encrypted_file_result.Text = "No file was given!";

            }
            catch
            {
                Encrypted_file_result.Text = Encrypt_file_key.Text;
            }
        }
        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Decrypt_file_name_textblock.Text != "")
                {
                    List<string> WordsFromFile = System.IO.File.ReadAllLines(Decrypt_file_name_textblock.Text).ToList();
                    string result = "";
                    foreach (var word in WordsFromFile)
                    {
                        MatrixConversion matrixConversion = new MatrixConversion(word, Decrypt_file_key.Text.ToString(), false);
                        result += matrixConversion.Decrypt() + "\n";
                    }
                    Decrypted_file_result.Text = result;
                }
                else if (Decrypt_file_key.Text == "")
                {
                    Decrypted_file_result.Text = "No file was given!\nPlease enter key.";
                }
                else Decrypted_file_result.Text = "No file was given!";
            }
            catch
            {
                Decrypted_file_result.Text = Decrypt_file_key.Text;
            }
        }
    }
}