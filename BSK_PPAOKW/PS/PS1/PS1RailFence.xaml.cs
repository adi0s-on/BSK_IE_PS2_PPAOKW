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
using System.Threading;
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
        public static string filepathRailFenceEncrypt = "", filepathRailFenceDecrypt = "";
        public PS1RailFence()
        {
            InitializeComponent();
            //Progress_bar.Visibility = Visibility.Hidden;
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
                else Encrypted_Result.Text = "Please enter text in adjacent window.";
            }
            catch
            {
                if(Text != "") Encrypted_Result.Text = "N has to be a number higher than 1!";
                else Encrypted_Result.Text = "Please enter text in adjacent window\nN has to be a number higher than 1!";
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
        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if(response == true)
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
        private void Encrypt_from_file(object sender, RoutedEventArgs e)
        {
            try
            {
                int N = Int32.Parse(Encrypt_file_N.Text.ToString());
                if (filepathRailFenceEncrypt != "")
                {
                    //Background.Source = new BitmapImage(new Uri("BSK_PPAOKW/Assets/Grayout.png", UriKind.Relative)); 

                    RailFenceFromFile railFenceFromFile = new RailFenceFromFile(filepathRailFenceEncrypt, N);
                    Encrypted_file_result.Text = "";

                    List<string> words = railFenceFromFile.EncryptFromFile();
                    foreach(String word in words)
                    {
                        Encrypted_file_result.Text += word; Encrypted_file_result.Text += "\n";
                    }

                   // Background.Source = new BitmapImage(new Uri("BSK_PPAOKW/Assets/Whiteout.png", UriKind.Relative));
                   //Progress_bar.Visibility = Visibility.Hidden;
                }
                else Encrypted_file_result.Text = "No file was given!";   
            }
            catch
            {
               // Progress_bar.Visibility = Visibility.Hidden;
                if (filepathRailFenceEncrypt != "") Encrypted_file_result.Text = "N has to be a number!";
                    else Encrypted_file_result.Text = "No file was given!\nN has to be a number!";
            } 
        }
        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            try
            {
                int N = Int32.Parse(Decrypt_file_N.Text.ToString());
                if (filepathRailFenceDecrypt != "")
                {
                    RailFenceFromFile railFenceFromFile = new RailFenceFromFile(filepathRailFenceDecrypt, N);
                    Decrypted_file_result.Text = "";
                    List<string> words = railFenceFromFile.DecryptFromFile();
                    foreach(String word in words)
                    {
                        Decrypted_file_result.Text += word; Decrypted_file_result.Text += "\n";
                    }
                }
                else Decrypted_file_result.Text = "No file was given!";
            }
            catch
            {
                if (filepathRailFenceDecrypt != "") Decrypted_file_result.Text = "N has to be a number!";
                else Decrypted_file_result.Text = "No file was given!\nN has to be a number!";
            }
        }
    }
}
