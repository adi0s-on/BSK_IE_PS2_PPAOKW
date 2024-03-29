﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public DES(byte[] content, bool encrypt, string key)
        {
            Content = content;
            Bits = new List<BitArray>();
            Key = new BitArray(key.Select(x => x == '1').ToArray());
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
            BitArray key = KeyTransformFor56Bits(Key);
            DivideKeyFor28Bits(key, out BitArray leftKey, out BitArray rightKey);
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
                BitArray afterIP = InitialPermutation(block);
                DivideForTwoBlocks(afterIP, out BitArray leftBlock, out BitArray rightBlock);

                BitArray leftBlockWorking = leftBlock;
                BitArray rightBlockWorking = rightBlock;

                BitArray RnNext = null;
                //after that we should get R16 and L16
                for (int n = 0; n < 16; n++)
                {
                    RnNext = new BitArray(32);
                    BitArray blockToXorWithLnFromRn = PermutateWithP(STables(Expanding32BitsRBlockTo48Bits(rightBlockWorking)));
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
                        if (leftBlockWorking[i] != blockToXorWithLnFromRn[i])
                        {
                            RnNext[i] = true;
                        }
                    }

                    leftBlockWorking = rightBlock;
                    rightBlockWorking = RnNext;
                }

                BitArray MergeRightAndLeft = new BitArray(64);
                for (int i = 0; i < 32; i++)
                {
                    MergeRightAndLeft[i] = rightBlockWorking[i];
                }
                for (int i = 32; i < 64; i++)
                {
                    MergeRightAndLeft[i] = leftBlockWorking[i-32];
                }
                BitArray result = InvertedInitialPermutation(MergeRightAndLeft);
                ResultsIn64BitArrays.Add(result);
            }
            BitArray AllBits = BitArraysToByteArray(ResultsIn64BitArrays);
            Result = BitArrayToByteArray(AllBits);

            //Unfinished attempt to decrypt, unfinished because of encrypting not working correctly 
            if(!Encrypt)
            {
                byte[] helperResult = new byte[Result.Length - 5];
                for (int i = 0; i < helperResult.Length; i++)
                {
                    helperResult[i] = Result[i];
                }
                Result = helperResult;
            }

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
        private BitArray InitialPermutation(BitArray bitArray)
        {
            BitArray helper = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                helper[i] = bitArray[ConstantValues.InitialPermutation[i]-1];
            }
            return helper;
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
        private BitArray KeyTransformFor56Bits(BitArray key)
        {
            BitArray keyToReturn = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                keyToReturn[i] = key[ConstantValues.PermutedChoice1[i] - 1];
            }
            return keyToReturn;
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
            KeyLeft = BitsShifting(KeyLeft);
            KeyRight = BitsShifting(KeyRight);
        }
        private BitArray[] BitsShifting(BitArray[] bitArrays)
        {
            for (int i = 1; i < 17; i++)
            {
                bitArrays[i] = bitArrays[i - 1];
                if (ConstantValues.CnDnBitMove[i - 1] == 1)
                {
                    bool bit = bitArrays[i][0];
                    for (int j = 1; j <= 27; j++)
                    {
                        bitArrays[i][j - 1] = bitArrays[i][j];
                    }
                    bitArrays[i][27] = bit;
                }
                else
                {
                    bool bit = bitArrays[i][0];
                    bool bit2 = bitArrays[i][1];
                    for (int j = 2; j <= 27; j++)
                    {
                        bitArrays[i][j - 2] = bitArrays[i][j];
                    }
                    bitArrays[i][26] = bit;
                    bitArrays[i][27] = bit2;
                }
            }
            return bitArrays;
        }
        private BitArray CnDnFusionToKnAndPermutedChoice2(BitArray Cn, BitArray Dn)
        {
            BitArray Kn = new BitArray(56);
            for (int i = 0; i < 28; i++)
            {
                Kn[i] = Cn[i];
            }
            for (int i = 28; i < 56; i++)
            {
                Kn[i] = Dn[i-28];
            }
            BitArray helper = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                helper[i] = Kn[i];
            }
            Kn = new BitArray(48);
            for (int i = 0; i < 48; i++)
            {
                Kn[i] = helper[ConstantValues.PermutedChoice2[i] - 1];
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
                if (Kn[KCounter][i] != Rn[i])
                {
                    SeriesOfBits[i] = true;
                }
                else
                {
                    SeriesOfBits[i] = false;
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

            BitArray[] Series4Bits = new BitArray[8];

            for (int s = 0; s < 8; s++)
            {
                int row = Convert.ToInt32(Convert.ToInt32(SeriesArray[s][0]).ToString() + Convert.ToInt32(SeriesArray[s][5]).ToString(), 2);
                int column = Convert.ToInt32(Convert.ToInt32(SeriesArray[s][1]).ToString() + Convert.ToInt32(SeriesArray[s][2]).ToString() + Convert.ToInt32(SeriesArray[s][3]).ToString() + Convert.ToInt32(SeriesArray[s][4]).ToString(), 2);
                Series4Bits[s] = new BitArray(4);
                string number = Convert.ToString(TabularData[s][row, column],2);
                for (int i = 0; i < number.Length; i++)
                {
                    Series4Bits[s][i] = Convert.ToBoolean(Convert.ToInt32(number[i]));
                }
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
