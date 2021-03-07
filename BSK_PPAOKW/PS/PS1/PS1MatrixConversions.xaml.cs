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
        public static string filepathRailFenceEncrypt = "", filepathRailFenceDecrypt = "";
        public PS1MatrixConversions()
        {
            InitializeComponent();
        }
        private void Encrypt (object sender, RoutedEventArgs e)
        {
            MatrixConversion matrixConversion = new MatrixConversion(Encrypt_Text.Text.ToString(),Encrypt_Key.Text.ToString());
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
                filepathRailFenceEncrypt = openFileDialog.FileName;
                Encrypt_file_name_textblock.Text = filepathRailFenceEncrypt;
            }
        }
        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                filepathRailFenceDecrypt = openFileDialog.FileName;
                Decrypt_file_name_textblock.Text = filepathRailFenceDecrypt;
            }
        }
    }
}
