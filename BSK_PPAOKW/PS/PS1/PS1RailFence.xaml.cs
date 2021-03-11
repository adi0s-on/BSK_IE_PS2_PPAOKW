using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                if (Int32.Parse(Encrypt_file_N.Text) <= 1) Encrypted_file_result.Text = "N has to be a number greater than 1!";
                else
                {
                    int N = Int32.Parse(Encrypt_file_N.Text.ToString());
                    if (Encrypt_file_name_textblock.Text != "")
                    {
                        List<string> WordsFromFile = System.IO.File.ReadAllLines(Encrypt_file_name_textblock.Text).ToList();
                        string result = "";
                        Encrypted_file_result.Text = "";

                        foreach (String word in WordsFromFile)
                        {
                            RailFence railFence = new RailFence(word, Int32.Parse(Encrypt_file_N.Text));
                            result += railFence.Encrypt() + "\n";
                        }
                        Encrypted_file_result.Text = result;
                    }
                    else Encrypted_file_result.Text = "No file was given!";   
                }
            }
            catch
            {
                if (Encrypt_file_name_textblock.Text != "") Encrypted_file_result.Text = "N has to be a number!";
                    else Encrypted_file_result.Text = "No file was given!\nN has to be a number!";
            } 
        }
        private void Decrypt_from_file(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(Decrypt_file_N.Text) <= 1) Decrypted_file_result.Text = "N has to be a number greater than 1!";
                else
                {
                    int N = Int32.Parse(Decrypt_file_N.Text.ToString());
                    if (Decrypt_file_name_textblock.Text != "")
                    {
                        List<string> WordsFromFile = System.IO.File.ReadAllLines(Decrypt_file_name_textblock.Text).ToList();
                        string result = "";
                        Decrypted_file_result.Text = "";

                        foreach(String word in WordsFromFile)
                        {
                            RailFence railFence = new RailFence(word, Int32.Parse(Decrypt_file_N.Text));
                            result += railFence.Decrypt() + "\n";
                        }
                        Decrypted_file_result.Text = result;
                    }
                    else Decrypted_file_result.Text = "No file was given!";
                }
            }
            catch
            {
                if (Decrypt_file_name_textblock.Text != "") Decrypted_file_result.Text = "N has to be a number!";
                else Decrypted_file_result.Text = "No file was given!\nN has to be a number!";
            }
        }
    }
}
