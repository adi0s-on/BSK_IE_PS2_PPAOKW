using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    class MatrixConversionPS2
    {
        private int[] Key { get; set; }
        private char[,] MatrixTable { get; set; }
        private char[] Word { get; set; }
        private int RowNumber { get; set; }
        private int ColumnNumber { get; set; }

        public MatrixConversionPS2(string message, string key)
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
        }

        public string Encrypt()
        {
            return "";
        }
    }
}
