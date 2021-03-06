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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Encrypt (object sender, RoutedEventArgs e)
        {
            MatrixConversion matrixConversion = new MatrixConversion(Encrypt_Text.Text.ToString(),Encrypt_Key.Text.ToString());
            matrixConversion.Encrypt();

        }
    }
}
