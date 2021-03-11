using System;
using System.Linq;
using System.Text;

namespace BSK_PPAOKW.PS
{
    class MatrixConversionPS2
    {
        private int[] Key { get; set; }
        private char[,] MatrixTable { get; set; }
        private int RowNumber { get; set; }
        private int ColumnNumber { get; set; }

        public MatrixConversionPS2(string key)
        {
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
            int[] Key2 = new int[Key.Length];
            for (int i = 0; i < Key.Length; i++)
            {
                Key2[Key[i] - 1] = i + 1;
            }
            Key = Key2;
        }

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");
            RowNumber = Key.Length * Key.Length;
            ColumnNumber = Key.Length;
            MatrixTable = new char[RowNumber, ColumnNumber];
            int letterCounter = 0;
            int rowCounter= 0;
            int columnCounter;
            try
            {
                while(letterCounter != message.Length)
                {
                    columnCounter = 0;
                    while (columnCounter < Key[rowCounter] && letterCounter != message.Length)
                    {
                        MatrixTable[rowCounter, columnCounter] = message[letterCounter];
                        columnCounter++;
                        letterCounter++;
                    }
                    rowCounter++;

                }
            }
            catch (Exception)
            {
                return "KEY IS INVALID! Perhaps it is too short for this method!";
            }
            string result = "";
            for (int i = 0; i < ColumnNumber; i++)
            {
                for (int j = 0; j < RowNumber; j++)
                {
                    if (MatrixTable[j, Key[i] - 1] != '\0')
                    {
                        result += MatrixTable[j, Key[i]-1];
                    }
                }
                result += " ";
            }
            return result;
        }

        public string Decrypt(string message)
        {
            bool[,] matrixTableHelper = CreateHelper(message);
            if(matrixTableHelper == null)
            {
                return "Wrong key!";
            }
            char[] word = new char[message.Length];
            string messageCopy = message.Replace(" ","");
            for (int i = 0; i < messageCopy.Length; i++)
            {
                word[i] = messageCopy[i];
            }
            string[] subs = message.Split(' ');
            string longest = subs.OrderByDescending(x=>x.Length).First();
            RowNumber = longest.Length;
            ColumnNumber = Key.Length;
            int letterCounter = 0;
            MatrixTable = new char[RowNumber,ColumnNumber];

            for (int i = 0; i < ColumnNumber; i++)
            {
                for (int j = 0; j < RowNumber; j++)
                {
                    if (matrixTableHelper[j, Key[i] - 1] != true)
                    { 
                        continue;
                    }
                    if (letterCounter < word.Length)
                    {
                        MatrixTable[j, Key[i]-1] = word[letterCounter];
                        letterCounter++;
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
        public bool[,] CreateHelper(string message)
        {
            message = message.Replace(" ", "");
            int rowNumber = Key.Length * Key.Length;
            int columnNumber = Key.Length;
            bool[,] matrixTableHelper = new bool[rowNumber, columnNumber];
            int letterCounter = 0;
            int rowCounter = 0;
            int columnCounter;
            try
            {
                while (letterCounter != message.Length)
                {
                    columnCounter = 0;
                    while (columnCounter < Key[rowCounter] && letterCounter != message.Length)
                    {
                        matrixTableHelper[rowCounter, columnCounter] = true;
                        columnCounter++;
                        letterCounter++;
                    }
                    rowCounter++;

                }
            }
            catch (Exception)
            {
                return null;
            }
            return matrixTableHelper;
        }
    }
}
