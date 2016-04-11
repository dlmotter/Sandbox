using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography_Utility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            // Test encoding with super array non-divisible by four, and sub array not filling space
            var encodedList = encodeByteList(
                new List<byte> { 255, 255, 255, 255, 85, 85, 85, 85, 170 },
                new List<byte> { 228 } 
            );

            // The decoded list will have extraneous bytes at the end if it doesn't fill up the super list.
            // This is OK, because length data will be stored in the header, so we know how many decoded bits to take.
            var decodedList = decodeByteList(encodedList);
        }

        static public List<byte> intToBytes(int integer, int numBytes)
        {
            List<byte> retVal = new List<byte>();
            string bits = Convert.ToString(integer, 2);
            if (bits.Length <= (8 * numBytes))
            {
                string paddedBits = bits.PadLeft((8 * numBytes), '0');
                for (int i = 0; i < (8 * numBytes); i += 8)
                {
                    retVal.Add((byte)Convert.ToInt32(paddedBits.Substring(i, 8), 2));
                }
            }
            else
            {
                return null;
            }
            return retVal;
        }

        static public int bytesToInt(List<byte> array)
        {
            string bits = string.Empty;
            foreach (byte b in array)
            {
                bits = string.Concat(bits, Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return Convert.ToInt32(bits, 2);
        }

        static public List<byte> encodeByteList(List<byte> superList, List<byte> subList) {
            List<byte> resultList = new List<byte>();

            // Make sure that the sublist is at most one fourth the length of the super list
            if (subList.Count * 4 <= superList.Count)
            {
                // Save off extraneous bytes from the super list that we will add back in at the end.
                // These are one, two, or three bytes after the last multiple of four.
                // They can't be used for encoding because we need four super bytes to encode one subbyte
                List<byte> extraneousBytes = new List<byte>();
                while (superList.Count % 4 != 0)
                {
                    extraneousBytes.Add(superList[superList.Count - 1]);
                    superList.RemoveAt(superList.Count - 1);
                }

                // Iterate through super bytes
                // Take the first six bits of super byte
                // Then append two bits at a time from sub bytes and append them to make the resultant byte
                for (int superIndex = 0; superIndex < superList.Count; superIndex++)
                {
                    if (superIndex / 4 < subList.Count)
                    {
                        // If we have bytes to encode

                        string superBits = Convert.ToString(superList[superIndex], 2).PadLeft(8, '0');
                        string containerBits = superBits.Substring(0, 6);

                        string subBits = Convert.ToString(subList[superIndex / 4], 2).PadLeft(8, '0');
                        string secretBits = subBits.Substring((superIndex % 4) * 2, 2);

                        string resultBits = string.Concat(containerBits, secretBits);
                        int resultInt = Convert.ToInt32(resultBits, 2);
                        resultList.Add(Convert.ToByte(resultInt));
                    }
                    else
                    {
                        // Otherwise, just use the super byte

                        resultList.Add(superList[superIndex]);
                    }
                }

                return resultList.Concat(extraneousBytes).ToList();
            }
            return null;
        }
        
        static public List<byte> decodeByteList(List<byte> encodedByteList)
        {
            List<byte> decodedByteList = new List<byte>();

            // Cut off extraneous bytes. We know no encoded information is in them.
            while (encodedByteList.Count % 4 != 0)
            {
                encodedByteList.RemoveAt(encodedByteList.Count - 1);
            }

            // We can now safely assume the length is divisible by four
            for (int encodedIndex = 0; encodedIndex < encodedByteList.Count; encodedIndex += 4)
            {
                string resultantByteBinary = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    string secretBits = Convert.ToString(encodedByteList[encodedIndex + i], 2).PadLeft(8, '0').Substring(6, 2);
                    resultantByteBinary = string.Concat(resultantByteBinary, secretBits);
                }
                decodedByteList.Add((byte)Convert.ToInt32(resultantByteBinary, 2));
            }

            return decodedByteList;
        }  
    }
}
