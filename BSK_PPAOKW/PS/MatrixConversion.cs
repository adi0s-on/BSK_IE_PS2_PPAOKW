using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    public class MatrixConversion
    {
        private int RowNumber { get; set; }
        private int ColumnNumber { get; set; }
        private int[] Key { get; set; }
        private char[,] MatrixTable { get; set; }

        public MatrixConversion(string word, string key)
        {
            string[] subs = key.Split('-');

            Key = new int[subs.Length];
            for (int i = 0; i < subs.Length; i++)
            {
                Key[i] = Int32.Parse(subs[i]);
            }

            MatrixTable = new char[Key.Length, 4];

        }
        public string Encrypt()
        {
            return "xd";
        }
    }
}
