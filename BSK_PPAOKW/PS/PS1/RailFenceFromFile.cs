using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK_PPAOKW.PS;

namespace BSK_PPAOKW.PS
{
    public class RailFenceFromFile
    {
        private char[] Word { get; set; }
        private char[,] CryptingTable { get; set; }
        private List<String> WordsFromFile { get; set; }
        private int N { get; set; }

        public RailFenceFromFile(string filepath, int n)
        {
            WordsFromFile = System.IO.File.ReadAllLines(@filepath).ToList();
            N = n;
        }

        public List<string> EncryptFromFile()
        {
            List<String> EncryptedWords = new List<string>();

            foreach(String word in WordsFromFile)
            {
                RailFenceCounter counter = new RailFenceCounter()
                {
                    MaxValue = N - 1,
                    Value = 0,
                    Direction = true,
                };

                Word = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Word[i] = word[i];
                }
                CryptingTable = new char[N, Word.Length];

                for(int i = 0; i < Word.Length; i++)
                {
                    CryptingTable[counter.Value, i] = Word[i];
                    counter.Tick();
                }

                string result = "";
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < Word.Length; j++)
                    {
                        if (CryptingTable[i, j] != '\0')
                        {
                            result += CryptingTable[i, j].ToString();
                        }
                    }
                }
                EncryptedWords.Add(result);
            }
            return EncryptedWords;
        }

        public List<string> DecryptFromFile()
        {
            List<String> DecryptedWords = new List<string>();

            foreach(String word in WordsFromFile)
            {
                RailFenceCounter counter = new RailFenceCounter()
                {
                    MaxValue = N - 1,
                    Value = 0,
                    Direction = true,
                };

                Word = new char[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Word[i] = word[i];
                }
                CryptingTable = new char[N, Word.Length];

                int letterNumber = 0;
                for (int i = 0; i < N; i++)
                {
                    if (i == 0 || i == N - 1)
                    {
                        for (int j = i; j < Word.Length; j += 2 * (N - 1))
                        {
                            CryptingTable[i, j] = Word[letterNumber]; letterNumber++;
                        }

                    }
                    else
                    {
                        bool even = false;
                        for (int j = i; j < Word.Length;)
                        {
                            even = !even;
                            CryptingTable[i, j] = Word[letterNumber]; letterNumber++;
                            if (even)
                            {
                                j += 2 * (N - 1 - i);
                            }
                            else
                            {
                                j += 2 * i;
                            }
                        }
                    }
                }

                string decipheredWord = "";
                for (int j = 0; j < Word.Length; j++)
                {
                    decipheredWord += CryptingTable[counter.Value, j];
                    counter.Tick();
                }
                DecryptedWords.Add(decipheredWord);
            }
            return DecryptedWords;
        }
        public class RailFenceCounter
        {
            public int Value { get; set; }
            public int MaxValue { get; set; }
            public bool Direction { get; set; }

            public void Tick()
            {
                if (Direction)
                {
                    if (Value < MaxValue)
                    {
                        Value++;
                    }
                    else if (Value == MaxValue)
                    {
                        Value--;
                        Direction = !Direction;
                    }
                }
                else
                {
                    if (Value > 0)
                    {
                        Value--;
                    }
                    else if (Value == 0)
                    {
                        Value++;
                        Direction = !Direction;
                    }
                }
            }
        }
    }
}
