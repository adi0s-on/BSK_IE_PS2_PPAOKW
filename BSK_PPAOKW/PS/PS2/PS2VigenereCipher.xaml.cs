using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS2VigenereCipher.xaml
    /// </summary>
    public partial class PS2VigenereCipher : UserControl
    {
        public PS2VigenereCipher()
        {
            InitializeComponent();
        }
        private void Encrypt(object sender, RoutedEventArgs e)
        {
            string Text = Encrypt_Text.Text.ToString();
            try
            {
                if (Text != "")
                {
                    string key = Key_lengthen(Text, Encrypt_Key.Text.ToUpper());
                    Encrypted_Result.Text = Encrypt_word(Text, key);
                }
                else Encrypted_Result.Text = "Please enter text in adjacent window.";
            }

            catch
            {
                if (Text != "")
                {
                    Encrypted_Result.Text = "";
                }
                else
                {
                    Encrypted_Result.Text = "Please enter text in adjacent window\nN has to be a number!";
                }
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            string Text = Decrypt_Text.Text.ToString();
            try
            {
                if (Text != "")
                {
                    string key = Key_lengthen(Text, Decrypt_Key.Text.ToUpper());
                    Decrypted_Result.Text = Decrypt_word(Text, key);
                }
                else
                {
                    Decrypted_Result.Text = "Please enter text in adjacent window.";
                }
            }
            catch
            {
                if (Text != "")
                {
                    Decrypted_Result.Text = "N has to be a number higher than 1!";
                }
                else
                {
                    Decrypted_Result.Text = "Please enter text in adjacent window\nN has to be a number!";
                }
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
                if (Encrypt_file_name_textblock.Text != "")
                {

                    List<string> WordsFromFile = System.IO.File.ReadAllLines(Encrypt_file_name_textblock.Text).ToList();
                    string result = "";
                    foreach (var word in WordsFromFile)
                    {
                        string key = Key_lengthen(word, Encrypt_file_key.Text.ToUpper());
                        result += Encrypt_word(word, key)+"\n";
                    }
                    Encrypted_file_result.Text = result;
                }
                else if(Encrypt_file_key.Text == "")
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
                        string key = Key_lengthen(word, Decrypt_file_key.Text.ToUpper());
                        result += Decrypt_word(word, key) + "\n";
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

        private string Key_lengthen(string word, string key)
        {
            if(key.Length >= word.Length)
            {
                return key.Remove(word.Length, key.Length - word.Length);
            }

            int whichIKey = 0; string keyToReturn = "";
            for (int i = 0; i < word.Length; i++)
            {
                keyToReturn += key[whichIKey]; whichIKey++;
                if (whichIKey == key.Length) whichIKey = 0;
            }
            return keyToReturn;
        }

        private char Encrypter(char x, char key)
        {
            if (!char.IsLetter(x)) return x;

            char begin = 'a';
            if (char.IsUpper(x)) begin = 'A';
                else key = Char.ToLower(key);

            if ((char) (x+key-begin) >= begin+26) return (char)((key - begin + x) % (begin+26) + begin);
                else return (char)(((key - begin + x) % begin) + begin);
            
        }

        private char Decrypter(char x, char key)
        {
            if (!char.IsLetter(x)) return x;

            char begin = 'a';
            if (char.IsUpper(x)) begin = 'A';
                else key = Char.ToLower(key);

            char searchedLetter = (char)((x - begin) + (begin + 26) - (key - begin));
            if ((char)(searchedLetter) >= begin+26) return ((char)(searchedLetter - 26));
                else return (char)(searchedLetter);
        }

        private string Encrypt_word(string word, string key)
        {
            string encryptedWord = "";
            for(int i = 0; i < word.Length; i++)
            {
                encryptedWord += Encrypter(word[i], key[i]);
            }
            return encryptedWord;
        }

        private string Decrypt_word(string word, string key)
        {
            string decryptedWord = "";
            for(int i = 0; i < word.Length; i++)
            {
                decryptedWord += Decrypter(word[i], key[i]);
            }
            return decryptedWord;
        }
    }
}
