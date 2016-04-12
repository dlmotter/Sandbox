using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

            saveEncodedImage(
                @"C:\GitHubRepos\Sandbox\Steganography Utility\hider.bmp", 
                @"C:\GitHubRepos\Sandbox\Steganography Utility\hidden.bmp", 
                @"C:\GitHubRepos\Sandbox\Steganography Utility\encoded.bmp"
            );

            saveDecodedFile(
                @"C:\GitHubRepos\Sandbox\Steganography Utility\encoded.bmp",
                @"C:\GitHubRepos\Sandbox\Steganography Utility\decoded"
            );
        }

        static private List<byte> intToBytes(int integer, int numBytes)
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

        static private int bytesToInt(List<byte> array)
        {
            string bits = string.Empty;
            foreach (byte b in array)
            {
                bits = string.Concat(bits, Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return Convert.ToInt32(bits, 2);
        }

        static private List<byte> encodeByteList(List<byte> superList, List<byte> subList) {
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
        
        static private List<byte> decodeByteList(List<byte> encodedByteList)
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

        static private List<byte> bitmapToBytes(Bitmap image)
        {
            List<byte> retVal = new List<byte>();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    retVal.Add(pixelColor.R);
                    retVal.Add(pixelColor.G);
                    retVal.Add(pixelColor.B);
                }
            }

            return retVal;
        }

        static private Bitmap bytesToBitmap(List<byte> bytes, int width, int height)
        {
            Bitmap retVal = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            int byteIndex = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int red = bytes[byteIndex];
                    int green = bytes[byteIndex + 1];
                    int blue = bytes[byteIndex + 2];

                    byteIndex += 3;

                    retVal.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }

            return retVal;
        }

        static public void saveEncodedImage(string superImagePath, string subImagePath, string resultImagePath)
        {
            try {
                Bitmap superImage = new Bitmap(superImagePath);
                Bitmap subImage = new Bitmap(subImagePath);

                var superBytes = bitmapToBytes(superImage);

                var subBytes = new List<byte>();
                subBytes.Add(Convert.ToByte(0));                   // 0 as first secret byte indicates a hidden image (as opposed to text, TBI)
                subBytes.AddRange(intToBytes(subImage.Width, 4));  // Next 4 secret bytes tell hidden image width
                subBytes.AddRange(intToBytes(subImage.Height, 4)); // Next 4 secret bytes tell hidden image height
                subBytes.AddRange(bitmapToBytes(subImage));        // The rest of the secret bytes are the hidden image RGB data

                var encodedBytes = encodeByteList(superBytes, subBytes);

                if (encodedBytes == null)
                {
                    throw new Exception("Container image not large enough to hide hidden image");
                }

                Bitmap resultImage = bytesToBitmap(encodedBytes, superImage.Width, superImage.Height);

                resultImage.Save(resultImagePath);
            }
            catch (Exception ex)  
            {

            }
        }

        static public void saveDecodedFile(string encodedImagePath, string resultImagePath)
        {
            Bitmap encodedImage = new Bitmap(encodedImagePath);
            var encodedBytes = bitmapToBytes(encodedImage);
            var decodedBytes = decodeByteList(encodedBytes);
            string fileFormat = string.Empty;

            if (decodedBytes[0] == 0)
            {
                // Look for encoded image
                fileFormat = ".bmp";

                // Get width and height from header
                var width = bytesToInt(decodedBytes.Skip(1).Take(4).ToList());
                var height = bytesToInt(decodedBytes.Skip(5).Take(4).ToList());

                // Remove header
                decodedBytes.RemoveRange(0, 9);

                // Save bitmap
                Bitmap decodedImage = bytesToBitmap(decodedBytes, width, height);
                decodedImage.Save(resultImagePath + fileFormat);
            }
            else
            {
                // Look for encoded text, TBI
                fileFormat = ".txt";
            }
        }
    }
}
