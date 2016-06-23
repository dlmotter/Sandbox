using HtmlAgilityPack;
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace ws_util
{
    class Program
    {
        // TODO unhardcode this
        private const string configPath = @"C:\GitHubRepos\Sandbox\Wallpaper Setter\ws_config.xml";
        public const string baseURL = "http://www.thepaperwall.com";

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        static void Main(string[] args)
        {
            SetWallpaper(GetWallpaperURL());
        }

        static string GetWallpaperURL()
        {
            // This gets the wallpaper of the day url
            // TODO read from config file and return the correct url depending on the settings

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(baseURL);
            string imageSrc = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_leftcol\"]/div[2]/div/div[1]/a/img").Attributes["src"].Value;
            string imageURL = string.Format("{0}{1}", baseURL, imageSrc.Substring(imageSrc.IndexOf("image=") + "image=".Length));

            return imageURL;
        }

        static void SetWallpaper(string URL)
        {
            // TODO get this from config file
            string fullFileName = Environment.ExpandEnvironmentVariables("%USERPROFILE%/daily_wallpaper.bmp");

            WebClient webClient = new WebClient();
            webClient.DownloadFile(URL, fullFileName);

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fullFileName, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }
    }
}
