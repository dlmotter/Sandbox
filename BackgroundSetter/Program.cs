using HtmlAgilityPack;
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace BackgroundSetter
{
    class Program
    {
        public const string baseURL = "http://www.thepaperwall.com";
        public const string fileName = @"%USERPROFILE%\daily_wallpaper.bmp";

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        static void Main(string[] args)
        {
            Console.WindowWidth = 128;
            Console.WriteLine("I am getting the wallpaper of the day from:");

            string URL = GetWallpaperURL();
            Console.WriteLine(URL);

            SetWallpaper(URL);
        }

        static string GetWallpaperURL()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(baseURL);
            string imageSrc = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_leftcol\"]/div[2]/div/div[1]/a/img").Attributes["src"].Value;
            string imageURL = string.Format("{0}{1}", baseURL, imageSrc.Substring(imageSrc.IndexOf("image=") + "image=".Length));

            return imageURL;
        }

        static void SetWallpaper(string URL)
        {
            string fullFileName = Environment.ExpandEnvironmentVariables(fileName);

            WebClient webClient = new WebClient();
            webClient.DownloadFile(URL, fullFileName);

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fullFileName, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }
    }
}
