using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Steganography_Utility
{
    static class Program
    {
        #region Global Variables
        public static List<string> _containerImageTypes = new List<string> { ".bmp", ".png", ".jpg", ".jpeg" };
        public static string _containerImageFilter = "Bitmap|*.bmp|PNG|*.png|JPEG|*.jpg;*.jpeg";
        public static List<string> _resultImageTypes = new List<string> { ".bmp", ".png" };
        public static string _resultImageFilter = "Bitmap|*.bmp|PNG|*.png";

        // Try to figure out a better way to do this. Need to be able to save filetype information in one byte.
        // For now, use a 1-1 mapping with most common filetypes according to http://fileinfo.com/filetypes/common
        public static Dictionary<byte, string> _fileTypeMapping = new Dictionary<byte, string>()
        {
            #region Text files
            { 000, ".doc" },
            { 001, ".docx" },
            { 002, ".log" },
            { 003, ".msg" },
            { 004, ".odt" },
            { 005, ".pages" },
            { 006, ".rtf" },
            { 007, ".tex" },
            { 008, ".txt" },
            { 009, ".wpd" },
            { 010, ".wps" },
            #endregion
            #region Data files
            { 011, ".csv" },
            { 012, ".dat" },
            { 013, ".ged" },
            { 014, ".key" },
            { 015, ".keychain" },
            { 016, ".pps" },
            { 017, ".ppt" },
            { 018, ".pptx" },
            { 019, ".sdf" },
            { 020, ".tar" },
            { 021, ".tax2014" },
            { 022, ".tax2015" },
            { 023, ".vcf" },
            { 024, ".xml" },
            #endregion
            #region Audio Files
            { 025, ".aif" },
            { 026, ".iff" },
            { 027, ".m3u" },
            { 028, ".m4a" },
            { 029, ".mid" },
            { 030, ".mp3" },
            { 031, ".mpa" },
            { 032, ".wav" },
            { 033, ".wma" },
            #endregion
            #region Video Files
            { 034, ".3g2" },
            { 035, ".3gp" },
            { 036, ".asf" },
            { 037, ".avi" },
            { 038, ".flv" },
            { 039, ".m4v" },
            { 040, ".mov" },
            { 041, ".mp4" },
            { 042, ".mpg" },
            { 043, ".rm" },
            { 044, ".srt" },
            { 045, ".swf" },
            { 046, ".vob" },
            { 047, ".wmv" },
            #endregion
            #region 3D Image Files
            { 048, ".3dm" },
            { 049, ".3ds" },
            { 050, ".max" },
            { 051, ".obj" },
            #endregion
            #region Raster Image Files
            { 052, ".bmp" },
            { 053, ".dds" },
            { 054, ".gif" },
            { 055, ".jpg" },
            { 056, ".png" },
            { 057, ".psd" },
            { 058, ".pspimage" },
            { 059, ".tga" },
            { 060, ".thm" },
            { 061, ".tif" },
            { 062, ".tiff" },
            { 063, ".yuv" },
            #endregion
            #region Vector Image Files
            { 064, ".ai" },
            { 065, ".eps" },
            { 066, ".ps" },
            { 067, ".svg" },
            #endregion
            #region Page Layout Files
            { 068, ".indd" },
            { 069, ".pct" },
            { 070, ".pdf" },
            #endregion
            #region Spreadsheet Files
            { 071, ".xlr" },
            { 072, ".xls" },
            { 073, ".xlsx" },
            #endregion
            #region Database Files
            { 074, ".accdb" },
            { 075, ".db" },
            { 076, ".dbf" },
            { 077, ".mdb" },
            { 078, ".pdb" },
            { 079, ".sql" },
            #endregion
            #region Executable Files
            { 080, ".apk" },
            { 081, ".app" },
            { 082, ".bat" },
            { 083, ".cgi" },
            { 084, ".com" },
            { 085, ".exe" },
            { 086, ".gadget" },
            { 087, ".jar" },
            { 088, ".pif" },
            { 089, ".wsf" },
            #endregion
            #region Game Files
            { 090, ".dem" },
            { 091, ".gam" },
            { 092, ".nes" },
            { 093, ".rom" },
            { 094, ".sav" },
            #endregion
            #region CAD Files
            { 095, ".dwg" },
            { 096, ".dxf" },
            #endregion
            #region GIS Files
            { 097, ".gpx" },
            { 098, ".kml" },
            { 099, ".kmz" },
            #endregion
            #region Web Files
            { 100, ".asp" },
            { 101, ".aspx" },
            { 102, ".cer" },
            { 103, ".cfm" },
            { 104, ".csr" },
            { 105, ".css" },
            { 106, ".htm" },
            { 107, ".html" },
            { 108, ".js" },
            { 109, ".jsp" },
            { 110, ".php" },
            { 111, ".rss" },
            { 112, ".xhtml" },
            #endregion
            #region Plugin Files
            { 113, ".crx" },
            { 114, ".plugin" },
            #endregion
            #region Font Files
            { 115, ".fnt" },
            { 116, ".fon" },
            { 117, ".otf" },
            { 118, ".ttf" },
            #endregion
            #region System Files
            { 119, ".cab" },
            { 120, ".cpl" },
            { 121, ".cur" },
            { 122, ".deskthemepack" },
            { 123, ".dll" },
            { 124, ".dmp" },
            { 125, ".drv" },
            { 126, ".icns" },
            { 127, ".ico" },
            { 128, ".lnk" },
            { 129, ".sys" },
            #endregion
            #region Settings Files
            { 130, ".cfg" },
            { 131, ".ini" },
            { 132, ".prf" },
            #endregion
            #region Encoded Files
            { 133, ".hqx" },
            { 134, ".mim" },
            { 135, ".uue" },
            #endregion
            #region Compressed Files
            { 136, ".7z" },
            { 137, ".cbr" },
            { 138, ".deb" },
            { 139, ".gz" },
            { 140, ".pkg" },
            { 141, ".rar" },
            { 142, ".rpm" },
            { 143, ".sitx" },
            { 144, ".tar.gz" },
            { 145, ".zip" },
            { 146, ".zipx" },
            #endregion
            #region Disk Image Files
            { 147, ".bin" },
            { 148, ".cue" },
            { 149, ".dmg" },
            { 150, ".iso" },
            { 151, ".mdf" },
            { 152, ".toast" },
            { 153, ".vcd" },
            #endregion
            #region Developer Files
            { 154, ".c" },
            { 155, ".class" },
            { 156, ".cpp" },
            { 157, ".cs" },
            { 158, ".dtd" },
            { 159, ".fla" },
            { 160, ".h" },
            { 161, ".java" },
            { 162, ".lua" },
            { 163, ".m" },
            { 164, ".pl" },
            { 165, ".py" },
            { 166, ".sh" },
            { 167, ".sln" },
            { 168, ".swift" },
            { 169, ".vb" },
            { 170, ".vcxproj" },
            { 171, ".xcodeproj" },
            #endregion
            #region Backup Files
            { 172, ".bak" },
            { 173, ".tmp" },
            { 174, ".crdownload" },
            { 175, ".ics" },
            { 176, ".msi" },
            { 177, ".part" },
            { 178, ".torrent" }
            #endregion
        };
        #endregion

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
        /// Convert an int to n number of bytes using the big endian convention
        /// <para>  For example, using 2 bytes, 1920 in binary is: 0000 0111 1000 0000</para>
        /// <para>  Converting each byte back to decimal, we get 7 and 128</para>
        /// <para>  So, this function would return { 7, 128 }</para>
        /// </summary>
        /// <param name="value">The int to convert</param>
        /// <param name="numBytes">The number of bytes to turn it into (maximum 4 since we used int is 32 bit)</param>
        /// <returns>The list of bytes representing the int</returns>
        static private List<byte> intToBytes(int value, int numBytes)
        {
            if (numBytes > 0 && numBytes <= 4)
            {
                List<byte> retVal = new List<byte>();
                string bits = Convert.ToString(value, 2);
                if (bits.Length <= (8 * numBytes))
                {
                    string paddedBits = bits.PadLeft((8 * numBytes), '0');
                    for (int i = 0; i < (8 * numBytes); i += 8)
                    {
                        retVal.Add(Convert.ToByte(paddedBits.Substring(i, 8), 2));
                    }
                }
                else
                {
                    throw new Exception("Not enough bytes to contain the int provided");
                }
                return retVal;
            }
            else
            {
                throw new Exception("The number of bytes must be greater than 0 and less than or equal to 4");
            }
        }

        /// <summary>
        /// Convert an array of bytes into an int using the big endian convention
        /// <para>  For example, pass in { 7, 128 }</para>
        /// <para>  Converting these bytes to binary gives you: 0000 0111 1000 0000</para>
        /// <para>  Parse that binary as one int and you get 1920</para>
        /// </summary>
        /// <param name="list">The list of bytes to convert to an int</param>
        /// <returns>The int obtained from the byte list</returns>
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
            Bitmap retVal = new Bitmap(width, height);

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
        static private List<byte> encodeByteList(List<byte> superList, List<byte> subList)
        {
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
                        resultList.Add(Convert.ToByte(resultBits, 2));
                    }
                    else
                    {
                        // Otherwise, just use the super byte

                        resultList.Add(superList[superIndex]);
                    }
                }

                return resultList.Concat(extraneousBytes).ToList();
            }
            else
            {
                throw new Exception("The container image is not large enough to contain the secret data.");
            }
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
                decodedByteList.Add(Convert.ToByte(resultantByteBinary, 2));
            }

            return decodedByteList;
        }

        #endregion

        #region Image processing functions

        /// <summary>
        /// Gets the ImageFormat property befitting the file passed in. If it's a non-supported type, it returns ImageFormat.Bmp.
        /// </summary>
        /// <param name="filePath">The full filepath of the file</param>
        /// <returns>The ImageFormat property</returns>
        static private ImageFormat getImageFormat(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            if (!string.IsNullOrEmpty(extension))
            {
                switch (extension)
                {
                    case @".bmp":
                        return ImageFormat.Bmp;
                    case @".png":
                        return ImageFormat.Png;
                    default:
                        return ImageFormat.Bmp;
                }
            }
            else
            {
                throw new Exception("File has no extension");
            }
        }

        /// <summary>
        /// Saves a Bitmap object to a file. Works like the Bitmap.Save(path) built-in method, but doesn't throw the "Generic Error Occurred in GDI+" error
        /// </summary>
        /// <param name="image">The Bitmap object</param>
        /// <param name="filePath">The path to save the file, including the filename and extension</param>
        static private void saveBitmap(Bitmap image, string filePath)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    image.Save(memory, getImageFormat(fs.Name));
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// Saves an encoded version of an image. Supported image types are bmp, png, jpg, and jpeg
        /// </summary>
        /// <param name="superImagePath">The filepath of the container image</param>
        /// <param name="subImagePath">The filepath of the secret image</param>
        /// <param name="resultImagePath">The filepath to save the resultant, encoded image</param>
        static public void saveEncodedFile(string superImagePath, string filePath, string resultImagePath)
        {
            // Get super bytes
            Bitmap superImage = new Bitmap(superImagePath);
            var superBytes = bitmapToBytes(superImage);

            // Get file bytes
            var fileBytes = File.ReadAllBytes(filePath).ToList();

            // Create header
            var header = new List<byte>();
            header.Add(_fileTypeMapping.FirstOrDefault(x => x.Value == Path.GetExtension(filePath)).Key);
            header.AddRange(intToBytes(fileBytes.Count, 4));

            // Prepend header
            fileBytes.InsertRange(0, header);

            // Save image
            var encodedBytes = encodeByteList(superBytes, fileBytes);
            Bitmap resultImage = bytesToBitmap(encodedBytes, superImage.Width, superImage.Height);
            saveBitmap(resultImage, resultImagePath);
        }

        /// <summary>
        /// Saves a decoded file
        /// </summary>
        /// <param name="encodedImagePath">The filepath of the encoded image</param>
        /// <param name="resultPath">The filepath to save the resultant, decoded file</param>
        static public void saveDecodedFile(string encodedImagePath)
        {
            // Get decoded bytes
            Bitmap encodedImage = new Bitmap(encodedImagePath);
            var encodedBytes = bitmapToBytes(encodedImage);
            var decodedBytes = decodeByteList(encodedBytes);

            // Get file type
            string fileType = _fileTypeMapping[decodedBytes[0]];
            if (fileType == null)
            {
                throw new Exception(string.Format("No hidden file found in {0}", encodedImagePath));
            }

            // Get file length
            int fileLength = bytesToInt(decodedBytes.Skip(1).Take(4).ToList());

            // Remove header
            decodedBytes.RemoveRange(0, 5);

            // Save file
            File.WriteAllBytes(Path.ChangeExtension(encodedImagePath, "decoded" + fileType), decodedBytes.Take(fileLength).ToArray());
        }

        #endregion
    }
}