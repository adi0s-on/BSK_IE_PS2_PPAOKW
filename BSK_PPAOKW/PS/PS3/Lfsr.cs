using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_PPAOKW.PS
{
    public class Lfsr
    {
        public List<bool[]> TableXOR { get; set; }
        public int RowLength { get; set; }
        public List<bool> Result { get; set; }
        public int[] Key { get; set; }

        public bool IsStopped { get; set; }

        public Lfsr(int rowLength)
        {
            RowLength = rowLength;
            TableXOR = new List<bool[]>();
            Result = new List<bool>();
        }

        public bool AddRow()
        {
            if (TableXOR.Count == 0)
            {
                bool[] row = new bool[RowLength];
                for (int i = 0; i < RowLength; i++)
                {
                    Random random = new Random();
                    int q = random.Next(0, 2);
                    if (q == 0)
                    {
                        row[i] = false;
                    }
                    else
                    {
                        row[i] = true;
                    }
                }
                TableXOR.Add(row);
                bool xor = OperationXOR(row);
                Result.Add(xor);
                return xor;
                
            }
            else
            {
                bool[] row = new bool[RowLength];
                row[0] = Result.LastOrDefault();
                for (int i = 1; i < RowLength; i++)
                {
                    row[i] = TableXOR.LastOrDefault()[i];
                }
                TableXOR.Add(row);
                bool xor = OperationXOR(row);
                Result.Add(xor);
                return xor;
            }
        }

        private bool OperationXOR(bool[] row)
        {
            if (row[0] != row[RowLength-1])
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
