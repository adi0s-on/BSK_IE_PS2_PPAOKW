using System.Collections;
using System.Collections.Generic;

namespace BSK_PPAOKW.PS
{
    public class DES
    {
        private byte[] Content { get; set; }
        private byte[] Helper { get; set; }
        private BitArray Key { get; set; }
        private BitArray KeyLeft { get; set; }
        private BitArray KeyRight { get; set; }
        private List<BitArray> Bits { get; set; }
        public DES(byte[] content)
        {
            Content = content;
            Bits = new List<BitArray>();
            Key = new BitArray(64);
        }
        public void Algorythm()
        {
            Input();
            KeyTransformFor56Bits(Key);
            DivideKeyFor28Bits(Key, out BitArray leftKey, out BitArray rightKey);
            KeyLeft = leftKey; KeyRight = rightKey;
            foreach (var block in Bits)
            {
                InitialPermutation(block);
                DivideForTwoBlocks(block, out BitArray leftBlock, out BitArray rightBlock);
            }
        }
        private void Input()
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
        private void KeyMovingIterations()
        {

        }
    }
}
