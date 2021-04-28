using Microsoft.Win32;
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
    /// Logika interakcji dla klasy PS4DES.xaml
    /// </summary>
    public partial class PS4DES : UserControl
    {
        public string fileExtension = "";
        public PS4DES()
        {
            InitializeComponent();
            Key_Encrypt.Text = "1010101011100010111100100111101010010111110010101010101110000101";
            Key_Decrypt.Text = "1010101011100010111100100111101010010111110010101010101110000101";
        }

        private void Open_file_encrypt(object sender, RoutedEventArgs e)
        {

            if (!FileOpen.Open_file(Encrypt_file_name_textblock))
            {
                return;
            }

            string str = Encrypt_file_name_textblock.Text.Substring(Encrypt_file_name_textblock.Text.Length - 4);
            if (str != ".txt" && str != ".mp3" && str != ".jpg")
            {
                Encrypt_file_name_textblock.Text = "Wrong file format!";
            }
            fileExtension = str;
        }

        private void Open_file_decrypt(object sender, RoutedEventArgs e)
        {

            if (!FileOpen.Open_file(Decrypt_file_name_textblock))
            {
                return;
            }

            string str = Decrypt_file_name_textblock.Text.Substring(Decrypt_file_name_textblock.Text.Length - 4);
            if (str != ".txt" && str != ".mp3" && str != ".jpg")
            {
                Decrypt_file_name_textblock.Text = "Wrong file format!";
            }
            else
            {
                SolidColorBrush brushButtonActive = new SolidColorBrush(Color.FromRgb(0, 173, 181));
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if(Encrypt_file_name_textblock.Text != "Wrong file format!")
            {
               DES des = new DES(System.IO.File.ReadAllBytes(Encrypt_file_name_textblock.Text),true,Key_Encrypt.Text.ToString());
               des.Algorythm();
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
                   System.IO.File.WriteAllBytes(filePathToSave, des.Result);
               }
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (Decrypt_file_name_textblock.Text != "Wrong file format!")
            {
                DES des = new DES(System.IO.File.ReadAllBytes(Decrypt_file_name_textblock.Text), false, Key_Decrypt.Text.ToString());
                des.Algorythm();
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
                    System.IO.File.WriteAllBytes(filePathToSave, des.Result);
                }
            }
        }
    }
}
