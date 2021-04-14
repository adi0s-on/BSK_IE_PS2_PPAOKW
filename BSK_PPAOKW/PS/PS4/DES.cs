using System.Collections.Generic;

namespace BSK_PPAOKW.PS
{
    public class DES
    {
        public byte[] Content { get; set; }
        public byte[] Helper { get; set; }
        public List<byte[]> Bytes { get; set; }
        public DES(byte[] content)
        {
            Content = content;
            Bytes = new List<byte[]>();
        }

        public void StartAlgorytm()
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
                Bytes.Add(block);
                
                
            }
            foreach (var block in Bytes)
            {
                EncryptBlock(block);
            }
        }

        public void EncryptBlock(byte[] block)
        {

        }
    }
}
