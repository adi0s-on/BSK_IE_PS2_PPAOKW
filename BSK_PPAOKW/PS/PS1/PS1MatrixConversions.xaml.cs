using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            MatrixConversion matrixConversion = new MatrixConversion(Encrypt_Text.Text.ToString(), Encrypt_Key.Text.ToString());
            Encrypted_Result.Text = matrixConversion.Encrypt();
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            MatrixConversion matrixConversion = new MatrixConversion(Decrypt_Text.Text.ToString(), Decrypt_Key.Text.ToString());
            Decrypted_Result.Text = matrixConversion.Decrypt();
        }
        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                Encrypt_file_name_textblock.Text = openFileDialog.FileName;
            }
        }
        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                Decrypt_file_name_textblock.Text = openFileDialog.FileName;
            }
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
                        MatrixConversion matrixConversion = new MatrixConversion(word, Encrypt_file_key.Text.ToString());
                        result += matrixConversion.Encrypt() + "\n";
                    }
                    Encrypted_file_result.Text = result;
                }
                else Encrypt_file_name_textblock.Text = "No file was given!";
                
            }
            catch
            {
                Encrypted_file_result.Text = Encrypt_file_key.Text;
            }
        }
        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            //List<string> WordsFromFile = System.IO.File.ReadAllLines(Decrypt_file_name_textblock.Text).ToList();
            //string result = "";
            //foreach (var word in WordsFromFile)
            //{
            //    MatrixConversion matrixConversion = new MatrixConversion(word, Decrypt_file_key.Text.ToString());
            //    result += matrixConversion.Decrypt() + "\n";
            //}
            //Decrypted_file_result.Text = result;
        }
    }
}