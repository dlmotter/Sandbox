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

            var width = convertIntTo4ByteList(1920);
            var height = convertIntTo4ByteList(1080);
            var dimensions = width.Concat(height).ToList();

            var encodedList = encodeByteList(new List<byte> {
                255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255
            }, dimensions);

            var decodedList = decodeByteList(encodedList);

            var decodedWidth = convert4ByteArrayToInt(decodedList.Take(4).ToList());
            var decodedHeight = convert4ByteArrayToInt(decodedList.Skip(4).Take(4).ToList());
        }

        static public List<byte> convertIntTo4ByteList(int integer)
        {
            List<byte> retVal = new List<byte>();
            string bits = Convert.ToString(integer, 2);
            if (bits.Length <= 32)
            {
                string paddedBits = bits.PadLeft(32, '0');
                retVal.Add((byte)Convert.ToInt32(paddedBits.Substring(0, 8), 2));
                retVal.Add((byte)Convert.ToInt32(paddedBits.Substring(8, 8), 2));
                retVal.Add((byte)Convert.ToInt32(paddedBits.Substring(16, 8), 2));
                retVal.Add((byte)Convert.ToInt32(paddedBits.Substring(24, 8), 2));
            }
            else
            {
                return null;
            }
            return retVal;
        }

        static public int? convert4ByteArrayToInt(List<byte> array)
        {
            if (array.Count == 4)
            {
                string bits = string.Empty;
                foreach (byte b in array)
                {
                    bits = string.Concat(bits, Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                return Convert.ToInt32(bits, 2);
            }
            else
            {
                return null;
            }
        }

        static public List<byte> encodeByteList(List<byte> superList, List<byte> subList) {
            List<byte> resultList = new List<byte>();

            if (subList.Count * 4 <= superList.Count)
            {
                for (int superByteIndex = 0; superByteIndex < superList.Count; superByteIndex++)
                {
                    string superByteBinary = Convert.ToString(superList[superByteIndex], 2);
                    string containerBits = superByteBinary.Substring(0, 6);

                    int subByteIndex = superByteIndex / 4;
                    int subByteSection = superByteIndex % 4;
                    if (subByteIndex < subList.Count)
                    {                      
                        string subByteBinary = Convert.ToString(subList[subByteIndex], 2).PadLeft(8, '0');
                        string secretBits = subByteBinary.Substring(subByteSection * 2, 2);

                        byte resultantByte = (byte)Convert.ToInt32(string.Concat(containerBits, secretBits), 2);
                        resultList.Add(resultantByte);
                    }
                }
                return resultList;
            }
            else
            {
                return null;
            }
        }
        
        static public List<byte> decodeByteList(List<byte> encodedByteList)
        {
            List<byte> decodedByteList = new List<byte>();

            for (int encodedByteIndex = 0; encodedByteIndex < encodedByteList.Count; encodedByteIndex += 4)
            {
                int decodedByteIndex = encodedByteIndex / 4;
                string resultantByteBinary = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    string secretBits = Convert.ToString(encodedByteList[encodedByteIndex + i], 2).Substring(6, 2);
                    resultantByteBinary = string.Concat(resultantByteBinary, secretBits);
                }
                decodedByteList.Add((byte)Convert.ToInt32(resultantByteBinary, 2));
            }

            return decodedByteList;
        }  
    }
}
