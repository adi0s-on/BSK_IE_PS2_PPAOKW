using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    public class Lfsr
    {
        public int RowLength { get; set; }
        public int[] Key { get; set; }
        public bool[] UpperArray { get; set; }
        public bool[] LowerArray { get; set; }
        public bool FirstTime { get; set; }
        public bool IsStopped { get; set; }
        public bool[] KeySeed { get; set; }
        public bool[] Seed { get; set; }
        public Lfsr(int rowLength, int[] key)
        {
            int counter = 0;
            for (int i = 0; i < key.Length; i++)
            {
                if(key[i] != 0)
                {
                    counter++;
                }
            }
            Key = new int[counter];
            counter = 0;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] != 0)
                {
                    Key[counter] = key[i];
                    counter++;
                }
            }
            Array.Sort(Key);
            RowLength = rowLength;
            FirstTime = true;
            UpperArray = new bool[rowLength];
            LowerArray = new bool[rowLength];
        }

        public bool AddRow()
        {
            if (FirstTime)
            {
                int howManyTimesFalse = 0, howManyTimesTrue = 0;
                //first line has to be random
                Random random = new Random();
                for (int i = 0; i < RowLength; i++)
                {
                    int number = random.Next(0, 2);
                    if (number == 0)
                    {
                        UpperArray[i] = false; howManyTimesFalse++;
                    }
                    else
                    {
                        UpperArray[i] = true; howManyTimesTrue++;
                    }
                }
                //not only one type of values in starting row
                if (howManyTimesFalse == RowLength)
                {
                    UpperArray[RowLength - 1] = true;
                }
                else if (howManyTimesTrue == RowLength)
                {
                    UpperArray[RowLength - 1] = false;
                }

                KeySeed = new bool[UpperArray.Length];
                for (int i = 0; i < UpperArray.Length; i++)
                {
                    KeySeed[i] = UpperArray[i];
                }

                //XOR first and last | Q1 and Qk
                bool XORResult = OperationXOR(UpperArray);
                LowerArray[0] = XORResult;
                for (int i = 1; i < LowerArray.Length; i++)
                {
                    LowerArray[i] = UpperArray[i - 1];
                }
                FirstTime = false;
                return UpperArray[UpperArray.Length - 1];
            }

            else
            {
                for (int i = 0; i < UpperArray.Length; i++)
                {
                    UpperArray[i] = LowerArray[i];
                }

                bool XORResult = OperationXOR(UpperArray);
                LowerArray[0] = XORResult;

                for (int i = 1; i < LowerArray.Length; i++)
                {
                    LowerArray[i] = UpperArray[i - 1];
                }

                return UpperArray[UpperArray.Length-1];
            }
        }

        public bool AddRowDecrypt()
        {
            if (FirstTime)
            {
                for (int i = 0; i < RowLength; i++)
                {
                    UpperArray[i] = Seed[i]; 
                }

                KeySeed = new bool[UpperArray.Length];
                for (int i = 0; i < UpperArray.Length; i++)
                {
                    KeySeed[i] = UpperArray[i];
                }

                //XOR first and last | Q1 and Qk
                bool XORResult = OperationXOR(UpperArray);
                LowerArray[0] = XORResult;
                for (int i = 1; i < LowerArray.Length; i++)
                {
                    LowerArray[i] = UpperArray[i - 1];
                }
                FirstTime = false;
                return UpperArray[UpperArray.Length - 1];
            }

            else
            {
                for (int i = 0; i < UpperArray.Length; i++)
                {
                    UpperArray[i] = LowerArray[i];
                }

                bool XORResult = OperationXOR(UpperArray);
                LowerArray[0] = XORResult;

                for (int i = 1; i < LowerArray.Length; i++)
                {
                    LowerArray[i] = UpperArray[i - 1];
                }

                return UpperArray[UpperArray.Length - 1];
            }
        }

        //public bool OperationXOR(bool[] row)
        //{
        //    if (row[0] != row[row.Length-1])
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool OperationXOR(bool[] row)        
        {
            int counter = 0;
            bool result = row[Key[counter]-1];
            counter++;
            while (counter != Key.Length - 1)
            {
                if (row[Key[counter]-1] != result)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                counter++;
            }
            return result;
        }
    }
}
