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
        public bool  FirstTime { get; set; }
        public bool IsStopped { get; set; }

        public Lfsr(int rowLength)
        {
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
                for (int i = 0; i < RowLength; i++)
                {
                    Random random = new Random();
                    if (random.Next(0, 2) == 0)
                    {
                        UpperArray[i] = false; howManyTimesFalse++;
                    }
                    else
                    {
                        UpperArray[i] = true; howManyTimesTrue++;
                    }
                }
                if(howManyTimesFalse == RowLength)
                {
                    UpperArray[RowLength - 1] = true;
                }
                else if (howManyTimesTrue == RowLength)
                {
                    UpperArray[RowLength - 1] = false;
                }

                //XOR first and last | Q1 and Qk
                bool XORResult = OperationXOR(UpperArray);

                LowerArray[0] = XORResult;  
                for (int i = 1; i < LowerArray.Length; i++)
                {
                    LowerArray[i] = UpperArray[i - 1];
                }
                FirstTime = false;

                return XORResult;

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

                return XORResult;
            }
        }

        private bool OperationXOR(bool[] row)
        {
            if (row[0] != row[row.Length-1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
