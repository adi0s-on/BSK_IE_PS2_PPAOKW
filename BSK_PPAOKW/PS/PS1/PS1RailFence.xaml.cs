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
    /// Logika interakcji dla klasy PS1RailFence.xaml
    /// </summary>
    public partial class PS1RailFence : UserControl
    {
        public PS1RailFence()
        {
            InitializeComponent();
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            string Text = Encrypt_Text.Text.ToString();
            try
            {
                int N = Int32.Parse(Encrypt_N.Text.ToString()); 
                if (Text != "")
                {
                    RailFence railFence = new RailFence(Text, N);
                    Encrypted_Result.Text = railFence.Encrypt();
                }
                else Encrypt_Text.Text = "Please enter text in adjacent window.";
            }
            catch
            {
                if(Text != "") Encrypted_Result.Text = "N has to be a number!";
                else Encrypted_Result.Text = "Please enter text in adjacent window\nN has to be a number!";
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            string Text = Decrypt_Text.Text.ToString();
            try 
            {
                int N = Int32.Parse(Decrypt_N.Text.ToString()); 
                if(Text != "")
                {
                    RailFence railFence = new RailFence(Text, N);
                    Decrypted_Result.Text = railFence.Decrypt();
                }
                else Decrypted_Result.Text = "Please enter text in adjacent window.";
            }
            catch
            {
                if (Text != "") Decrypted_Result.Text = "N has to be a number!";
                    else Decrypted_Result.Text = "Please enter text in adjacent window\nN has to be a number!";
            }
            
        }
    }
}
