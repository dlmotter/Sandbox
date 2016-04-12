﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            Application.Run(new MainWindow());
        }

        #region Conversion functions

        /// <summary>
        /// Convert an integer to n number of bytes using the big endian convention
        /// <para>  For example, using 2 bytes, 1920 in binary is: 0000 0111 1000 0000</para>
        /// <para>  Converting each byte back to decimal, we get 7 and 128</para>
        /// <para>  So, this function would return { 7, 128 }</para>
        /// </summary>
        /// <param name="integer">The integer to convert</param>
        /// <param name="numBytes">The number of bytes to turn it into</param>
        /// <returns>The list of bytes representing the integer</returns>
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

        /// <summary>
        /// Convert an array of bytes into an integer using the big endian convention
        /// <para>  For example, pass in { 7, 128 }</para>
        /// <para>  Converting these bytes to binary gives you: 0000 0111 1000 0000</para>
        /// <para>  Parse that binary as one integer and you get 1920</para>
        /// </summary>
        /// <param name="list">The list of bytes to convert to an integer</param>
        /// <returns>The integer obtained from the byte list</returns>
        static private int bytesToInt(List<byte> list)
        {
            string bits = string.Empty;
            foreach (byte b in list)
            {
                bits = string.Concat(bits, Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return Convert.ToInt32(bits, 2);
        }

        /// <summary>
        /// Converts the RGB values of a Bitmap object into a list of bytes
        /// </summary>
        /// <param name="image">The Bitmap object</param>
        /// <returns>A list of format { R for pixel 0, G for pixel 0, B for pixel 0, R for pixel 1,  ... }</returns>
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

        /// <summary>
        /// Given a width and height, converts a list of bytes like one returned from decodeByteList() into a Bitmap object
        /// </summary>
        /// <param name="bytes">The list of bytes containg the RGB values</param>
        /// <param name="width">The width of the resultant Bitmap</param>
        /// <param name="height">The height of the resultant Bitmap</param>
        /// <returns>The Bitmap object created from the RGB values in the list</returns>
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

        #endregion

        #region Encoding/decoding functions

        /// <summary>
        /// Hide one list of bytes inside another list of bytes
        /// <para>To do this, split a secret byte into four, 2-bit chunks</para>
        /// <para>Then, take four of the container bytes, keep the six most significant bits, and change the last two to a chunk from the secret byte</para>
        /// <para>So, you need four "super" bytes to hide one "sub" byte</para>
        /// <para>All relevant operations are done using the big endian convention</para>
        /// </summary>
        /// <param name="superList">The list of bytes that will hide the secret bytes. Must be at least 4x as long as the sub list</param>
        /// <param name="subList">The list of bytes to hide. Must be at most one fourth the length of the super list</param>
        /// <returns>
        /// <para>The encoded list of bytes. The length of the result will be the same as the length of the original super list.</para>
        /// <para>Any bytes in the super list that were not needed for encoding remain unchanged.</para>
        /// </returns>
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

        /// <summary>
        /// Decode a list bytes encoded using the process described in the encodeByteList function
        /// </summary>
        /// <param name="encodedByteList">The list of encoded bytes</param>
        /// <returns>
        /// <para>The list of decoded bytes</para>
        /// <para>If the original sublist of bytes did not completely fill the super list, the decoded byte list will have extraneous bytes on the end</para>
        /// <para>This must be handled by the calling function</para>
        /// </returns>
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

        /// <summary>
        /// Saves an encoded version of an image. All major image types supported (bmp, png, jpg, ...)
        /// </summary>
        /// <param name="superImagePath">The filepath of the container image</param>
        /// <param name="subImagePath">The filepath of the secret image</param>
        /// <param name="resultImagePath">The filepath to save the resultant, encoded image</param>
        static public void saveEncodedImage(string superImagePath, string subImagePath, string resultImagePath)
        {
            try {
                // The super and sub images don't necessarily have to be .bmp file format.
                // You can pass a file of any format supported by the Image class to the Bitmap constructor
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

        /// <summary>
        /// Saves a decoded file. For now only supports images. Eventually will support text as well.
        /// </summary>
        /// <param name="encodedImagePath">The filepath of the encoded image</param>
        /// <param name="resultImagePath">The filepath to save the resultant, decoded image</param>
        static public void saveDecodedFile(string encodedImagePath, string resultImagePath)
        {
            Bitmap encodedImage = new Bitmap(encodedImagePath);
            var encodedBytes = bitmapToBytes(encodedImage);
            var decodedBytes = decodeByteList(encodedBytes);

            if (decodedBytes[0] == 0)
            {
                // Get width and height from header
                var width = bytesToInt(decodedBytes.Skip(1).Take(4).ToList());
                var height = bytesToInt(decodedBytes.Skip(5).Take(4).ToList());

                // Remove header
                decodedBytes.RemoveRange(0, 9);

                // Save image
                Bitmap decodedImage = bytesToBitmap(decodedBytes, width, height);
                decodedImage.Save(resultImagePath);
            }
            else
            {
                // Look for encoded text, TBI

            }
        }

        #endregion
    }
}