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

        public MatrixConversion(string word, string key)
        {
            string[] subs = key.Split('-');
            Key = new int[subs.Length];
            for (int i = 0; i < subs.Length; i++)
            {
                Key[i] = Int32.Parse(subs[i]);
            }

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
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < Key.Length; j++)
                {
                    result += MatrixTable[i,Key[j]-1];
                }
            }
            return result;
        }

        public string Decrypt()
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
            string result = "";
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    result += MatrixTable[i, j];
                }
            }
            return result;
        }
    }
}
