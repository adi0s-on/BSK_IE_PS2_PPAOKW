using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    public class MatrixConversion
    {
        private int[] Key { get; set; }
        private char[,] MatrixTable { get; set; }
        private char[] Word { get; set; }
        private int RowNumber { get; set; }
        private int ColumnNumber { get; set; }
        private bool IsKeyCipher { get; set; }
        private bool IsDecryptingWithWordKey { get; set; }
        public MatrixConversion(string word, string key, bool encrypt)
        {
            IsDecryptingWithWordKey = false;
            if(key.Contains('-'))
            {
                IsKeyCipher = true;
                string[] subs = key.Split('-');
                Key = new int[subs.Length];
                for (int i = 0; i < subs.Length; i++)
                {
                    Key[i] = Int32.Parse(subs[i]);
                }
            }
            else
            {
                if (!encrypt)
                {
                    IsDecryptingWithWordKey = true;
                }
                IsKeyCipher = false;
                Key = new int[key.Length];
                int counter = 1;
                for (int i = 0; i < Key.Length; i++)
                {
                    Key[key.IndexOf(key.Min())] = counter;
                    StringBuilder sb = new StringBuilder(key);
                    sb[key.IndexOf(key.Min())] = '|';
                    key = sb.ToString();
                    counter++;
                }
            }
            
            
            if (!IsDecryptingWithWordKey)
            {
                word = word.Replace(" ", "");
                Word = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Word[i] = word[i];
                }

                RowNumber = Word.Length / Key.Length;
                if (Word.Length/Key.Length * Key.Length < Word.Length)
                {
                    RowNumber++;
                }
                ColumnNumber = Key.Length;
                MatrixTable = new char[RowNumber,ColumnNumber];
            }
            else
            {
                Word = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Word[i] = word[i];
                }

            }
        }

        public string Encrypt()
        {
            int counter = 0;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber ; j++)
                {
                    if (counter < Word.Length)
                    {
                        MatrixTable[i, j] = Word[counter];
                        counter++;
                    }
                }
            }

            string result = "";
            if(IsKeyCipher)
            {
                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < Key.Length; j++)
                    {
                        result += MatrixTable[i, Key[j] - 1];
                    }
                }

            }
            else
            {
                int[] Key2 = new int[Key.Length];
                for (int i = 0; i < Key.Length; i++)
                {
                    Key2[Key[i]-1] = i;
                }
                for (int i = 0; i < Key.Length; i++)
                {
                    for (int j = 0; j < RowNumber; j++)
                    {
                        result += MatrixTable[j, Key2[i]];
                    }
                    result += " ";
                }
            }
            return result;
        }

        public string Decrypt()
        {
            string result = "";
            if(IsKeyCipher)
            {
                int[] KeySecond;
                int lastLettersCount = Word.Length - (RowNumber * ColumnNumber - ColumnNumber);
                KeySecond = new int[lastLettersCount];
                bool isKeySecondNeeded = false;
                if (Word.Length < RowNumber * ColumnNumber)
                {
                    isKeySecondNeeded = true;
                    int counterForSecondKey = 0;
                    for (int i = 0; i < Key.Length; i++)
                    {
                        if (Key[i] <= lastLettersCount)
                        {
                            KeySecond[counterForSecondKey] = Key[i];
                            counterForSecondKey++;
                        }
                    }

                }
                int counter = 0;
                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        if (counter < Word.Length)
                        {
                            if (i == RowNumber - 1 && isKeySecondNeeded)
                            {
                                MatrixTable[i, KeySecond[j] - 1] = Word[counter]; 
                            }
                            else
                            {
                                MatrixTable[i, Key[j]-1] = Word[counter];
                            }
                            counter++;
                        }
                    }
                }
                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        result += MatrixTable[i, j];
                    }
                }
            }
            else
            {
                if(Word.Length < Key.Length)
                {
                    return "The key is invalid for this word!";
                }
                string s = "";
                for (int i = 0; i < Word.Length; i++)
                {
                    s += Word[i];
                }
                string[] subs = s.ToString().Split(' ');
                int max = 0;
                for (int i = 0; i < subs.Length; i++)
                {
                    if(subs[i].Length > max)
                    {
                        max = subs[i].Length;
                    }
                }
                ColumnNumber = subs.Length;
                RowNumber = max;
                MatrixTable = new char[max, subs.Length];

                int[] Key2 = new int[Key.Length];
                for (int i = 0; i < Key.Length; i++)
                {
                    Key2[Key[i] - 1] = i;
                }
                for (int i = 0; i < ColumnNumber; i++)
                {
                    for (int j = 0; j < RowNumber; j++)
                    {
                        if(j < subs[i].Length && subs.Length >= Key.Length)
                        {
                            MatrixTable[j, Key2[i]] = subs[i][j];
                        }
                    }
                }

                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        result += MatrixTable[i, j];
                    }
                }


            }
            return result;
        }
    }
}
