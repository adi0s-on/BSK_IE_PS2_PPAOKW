using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    public class MatrixConversionFromFile
    {
        private char[] Word { get; set; }
        private char[,] CryptingTable { get; set; }
        private List<String> WordsFromFile { get; set; }
        private int N { get; set; }

        public MatrixConversionFromFile(string filepath, int n)
        {
            WordsFromFile = System.IO.File.ReadAllLines(@filepath).ToList();
            N = n;
        }

        public List<string> EncryptFromFile()
        {
            List<String> EncryptedWords = new List<string>();
            return EncryptedWords;
        }

        public List<string> DecryptFromFile()
        {
            List<String> DecryptedWords = new List<string>();
            return DecryptedWords;
        }
    }
}
