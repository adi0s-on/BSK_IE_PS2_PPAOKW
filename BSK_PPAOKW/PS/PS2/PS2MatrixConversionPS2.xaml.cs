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
            MatrixConversionPS2 matrixConversionPS2 = new MatrixConversionPS2(Encrypt_Text.Text.ToString(),Encrypt_Key.Text.ToString());
            Encrypted_Result.Text = matrixConversionPS2.Encrypt();
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {

        }

        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {

        }

        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {

        }

        private void Encrypt_from_file(object sender, RoutedEventArgs e)
        {

        }

        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {

        }
    }
}
