using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BSK_PPAOKW.PS
{
    public class DES
    {
        private byte[] Content { get; set; }
        private byte[] Helper { get; set; }
        private BitArray Key { get; set; }
        private BitArray[] KeyLeft { get; set; }
        private BitArray[] KeyRight { get; set; }
        private BitArray[] Kn { get; set; }
        private List<BitArray> Bits { get; set; }
        private List<BitArray> ResultsIn64BitArrays { get; set; }
        private int KCounter { get; set; }
        private bool Encrypt { get; set; }
        public byte[] Result { get; set; }
        public DES(byte[] content, bool encrypt)
        {
            Content = content;
            Bits = new List<BitArray>();
            Key = new BitArray(64);
            KeyLeft = new BitArray[17];
            KeyRight = new BitArray[17];
            Kn = new BitArray[16];
            ResultsIn64BitArrays = new List<BitArray>();
            if (encrypt)
            {
                Encrypt = true;
            }
            else
            {
                Encrypt = false;
            }
        }
        public void Algorythm()
        {
            if(Encrypt)
            {
                InputForEncrypt();
            }
            else 
            {
                InputForDecrypt();
            }
            KeyTransformFor56Bits(Key);
            DivideKeyFor28Bits(Key, out BitArray leftKey, out BitArray rightKey);
            KeyMovingIterations(leftKey, rightKey);
            for (int i = 1; i < 17; i++)
            {
                Kn[i-1] = CnDnFusionToKnAndPermutedChoice2(KeyLeft[i], KeyRight[i]);
            }
            foreach (var block in Bits)
            {
                if (Encrypt)
                {
                    KCounter = 0;
                }
                else
                {
                    KCounter = 15;
                }
                InitialPermutation(block);
                DivideForTwoBlocks(block, out BitArray leftBlock, out BitArray rightBlock);
                //RANDOM PROGRAMMING
                BitArray RnNext = new BitArray(64);
                for (int n = 0; n < 16; n++)
                {
                    RnNext = new BitArray(32);
                    BitArray blockToXorWithLnFromRn = PermutateWithP(STables(Expanding32BitsRBlockTo48Bits(rightBlock)));
                    if(Encrypt)
                    {
                        KCounter++;
                    }
                    else
                    {
                        KCounter--;
                    }
                    for (int i = 0; i < 32; i++)
                    {
                        if (leftBlock[i] != blockToXorWithLnFromRn[i])
                        {
                            RnNext[i] = true;
                        }
                    }
                    leftBlock = blockToXorWithLnFromRn;
                }
                BitArray MergeRightAndLeft = new BitArray(64);
                for (int i = 0; i < 32; i++)
                {
                    MergeRightAndLeft[i] = RnNext[i];
                }
                for (int i = 32; i < 64; i++)
                {
                    MergeRightAndLeft[i] = leftBlock[i-32];
                }
                BitArray result = InvertedInitialPermutation(MergeRightAndLeft);
                ResultsIn64BitArrays.Add(result);
            }
            BitArray AllBits = BitArraysToByteArray(ResultsIn64BitArrays);
            Result = BitArrayToByteArray(AllBits);

            //byte helper;
            //helper = Result[Result.Length - 1];
            //string lastByte = Convert.ToString(helper, 2).PadLeft(8, '0');
            //byte helper2 = Convert.ToByte(Convert.ToInt32(lastByte.Substring(0, 3),2));
            //Result[Result.Length - 1] = helper2;
            //Console.WriteLine("");

            //byte[] helperResult = new byte[Result.Length - 5];
            //for (int i = 0; i < helperResult.Length; i++)
            //{
            //    helperResult[i] = Result[i];
            //}

            //Result = helperResult;
            
        }
        private void InputForEncrypt()
        {
            int bytesAfterGrouping = 8 - (Content.Length % 8);
            Helper = new byte[Content.Length + bytesAfterGrouping];
            int i;
            for (i = 0; i < Content.Length; i++)
            {
                Helper[i] = Content[i];
            }
            for (int j = i; j < i + bytesAfterGrouping; j++)
            {
                Helper[j] = 0;
            }
            Helper[Helper.Length - 1] = (byte)bytesAfterGrouping;

            int counter = 0;
            for (int k = 0; k < Helper.Length/8; k++)
            {
                byte[] block = new byte[8]; 
                for (int l = 0; l < 8; l++)
                {
                    block[l] = Helper[counter];
                    counter++;
                }
                Bits.Add(new BitArray(block));
            }
        }
        private void InputForDecrypt()
        {
            int bytesAfterGrouping = 8 - (Content.Length % 8);
            bytesAfterGrouping = 0;
            Helper = new byte[Content.Length + bytesAfterGrouping];
            int i;
            for (i = 0; i < Content.Length; i++)
            {
                Helper[i] = Content[i];
            }
            for (int j = i; j < i + bytesAfterGrouping; j++)
            {
                Helper[j] = 0;
            }
            Helper[Helper.Length - 1] = (byte)bytesAfterGrouping;

            int counter = 0;
            for (int k = 0; k < Helper.Length / 8; k++)
            {
                byte[] block = new byte[8];
                for (int l = 0; l < 8; l++)
                {
                    block[l] = Helper[counter];
                    counter++;
                }
                Bits.Add(new BitArray(block));
            }
        }
        private void InitialPermutation(BitArray bitArray)
        {
            BitArray helper = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                helper[i] = bitArray[i];
            }
            for (int i = 0; i < 64; i++)
            {
                bitArray[i] = helper[ConstantValues.InitialPermutation[i]-1];
            }
        }
        private void DivideForTwoBlocks(BitArray bitArray, out BitArray left, out BitArray right)
        {
            left = new BitArray(32);
            right = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                left[i] = bitArray[i];
            }
            for (int i = 32; i < 64; i++)
            {
                right[i-32] = bitArray[i];
            }
        }
        private void KeyTransformFor56Bits(BitArray key)
        {
            BitArray helper = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                helper[i] = key[i];
            }

            key = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                key[i] = helper[ConstantValues.PermutedChoice1[i] - 1];
            }
        }
        private void DivideKeyFor28Bits(BitArray key,out BitArray left, out BitArray right)
        {
            left = new BitArray(28);
            right = new BitArray(28);
            for (int i = 0; i < 28; i++)
            {
                left[i] = key[i];
            }
            for (int i = 28; i < 56; i++)
            {
                right[i-28] = key[i];
            }
        }
        private void KeyMovingIterations(BitArray leftKey, BitArray rightKey)
        {
            KeyLeft[0] = leftKey; KeyRight[0] = rightKey;
            BitsShifting(KeyLeft);
            BitsShifting(KeyRight);
        }
        private void BitsShifting(BitArray[] bitArrays)
        {
            for (int i = 1; i < 17; i++)
            {
                bitArrays[i] = bitArrays[i - 1];
                if (ConstantValues.CnDnBitMove[i - 1] == 1)
                {
                    bool bit = bitArrays[i][0];
                    for (int j = 1; j < 27; j++)
                    {
                        bitArrays[i][j - 1] = bitArrays[i][j];
                    }
                    bitArrays[i][27] = bit;
                }
                else
                {
                    bool bit = bitArrays[i][0];
                    bool bit2 = bitArrays[i][1];
                    for (int j = 1; j < 27; j++)
                    {
                        bitArrays[i][j - 1] = bitArrays[i][j];
                    }
                    bitArrays[i][26] = bit;
                    bitArrays[i][27] = bit2;
                }
            }
        }
        private BitArray CnDnFusionToKnAndPermutedChoice2(BitArray Cn, BitArray Dn)
        {
            BitArray Kn = new BitArray(48);
            for (int i = 0; i < 24; i++)
            {
                Kn[i] = Cn[i];
            }
            for (int i = 24; i < 48; i++)
            {
                Kn[i] = Dn[i-24];
            }
            BitArray helper = new BitArray(48);
            for (int i = 0; i < 48; i++)
            {
                helper[i] = Kn[i];
            }
            for (int i = 0; i < 48; i++)
            {
                Kn[i] = helper[ConstantValues.ExpandingPermutation[i] - 1];
            }
            return Kn;
        }
        private BitArray Expanding32BitsRBlockTo48Bits(BitArray bitArray)
        {
            BitArray Bit48 = new BitArray(48);
            for (int i = 0; i < 48; i++)
            {
                Bit48[i] = bitArray[ConstantValues.ExpandingPermutation[i]-1];
            }
            return Bit48;
        }
        private BitArray STables(BitArray Rn)
        {
            BitArray SeriesOfBits = new BitArray(48);
            for (int i = 0; i < 48; i++)
            {
                if (Kn[KCounter][0] != Rn[0])
                {
                    SeriesOfBits[0] = true;
                }
                else
                {
                    SeriesOfBits[0] = false;
                }
            }
            BitArray[] SeriesArray = new BitArray[8];
            int CurrentBit = 0;
            for (int i = 0; i < 8; i++)
            {
                SeriesArray[i] = new BitArray(6);
                for (int j = 0; j < 6; j++)
                {
                    SeriesArray[i][j] = SeriesOfBits[CurrentBit];
                    CurrentBit++;
                }
            }
            List<int[,]> TabularData = ConstantValues.GetTabular_S();
            for (int s = 0; s < 8; s++)
            {
                int row = Convert.ToInt32(Convert.ToInt32(SeriesArray[s][0]).ToString() + Convert.ToInt32(SeriesArray[s][5]).ToString(), 2);
                int column = Convert.ToInt32(Convert.ToInt32(SeriesArray[s][1]).ToString() + Convert.ToInt32(SeriesArray[s][2]).ToString() + Convert.ToInt32(SeriesArray[s][3]).ToString() + Convert.ToInt32(SeriesArray[s][4]).ToString(), 2);
                SeriesArray[s] = new BitArray(new int[] { TabularData[s][row, column] });
            }

            BitArray Bit32ArrayToReturn = new BitArray(32);
            int bitCounter = 0;
            for (int s = 0; s < 8; s++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Bit32ArrayToReturn[bitCounter] = SeriesArray[s][i];
                    bitCounter++;
                }
            }
            return Bit32ArrayToReturn;
        }
        private BitArray PermutateWithP(BitArray arrayToPermutate)
        {
            BitArray bits = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                bits[i] = arrayToPermutate[ConstantValues.PBlockPermutation[i] - 1];
            }
            return bits;
        }
        private BitArray InvertedInitialPermutation(BitArray bitArray)
        {
            BitArray bits = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                bits[i] = bitArray[ConstantValues.EndingPermutation[i] - 1];
            }
            return bits;
        }
        private BitArray BitArraysToByteArray(List<BitArray> bitArrays)
        {
            int amountOfBits = bitArrays.Count * 64;
            BitArray bits = new BitArray(amountOfBits);
            int i = 0;
            int bitArrayCounter = 0;
            while (i < amountOfBits)
            {
                for (int k = 0; k < 64; k++)
                {
                    bits[i] = bitArrays[bitArrayCounter][k];
                    i++;
                }
                bitArrayCounter++;
            }
            return bits;
        }
        public byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }
    } 
}
