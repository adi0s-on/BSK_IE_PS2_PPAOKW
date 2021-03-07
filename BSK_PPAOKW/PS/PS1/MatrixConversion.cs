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
            int counter = 0;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    if (counter < Word.Length)
                    {
                        MatrixTable[i, j] = Word[counter];
                        counter++;
                    }
                }
            }

            Array.Reverse(Key);

            string result = "";
            int counterToRemove = 0;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < Key.Length; j++)
                {
                    if(MatrixTable[i,Key[j]-1] != '\0')
                    {
                        result += MatrixTable[i, Key[j] - 1];
                    }
                    else
                    {
                        result += MatrixTable[i, Key[Key.Length - j] - 1];
                        counterToRemove++;
                    }
                }
            }
            string result2 = "";
            for (int i = 0; i < result.Length-counterToRemove; i++)
            {
                result2 += result[i];
            }
            return result2;
        }
    }
}
