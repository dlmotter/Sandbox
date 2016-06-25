using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ws_util
{
    class Program
    {
        private const string configPath = "ws_config.xml";
        public const string baseURL = "http://www.thepaperwall.com";

        private static Dictionary<string, string> catDict = new Dictionary<string, string>() {
            { "Animals", "97ae48254c8b6f5ebc1cd28d498b522652e1fb6f" },
            { "Architecture", "1908ef97b29529db21aea7748289e1e9e7a07f5c" },
            { "Bikes", "c82ac345f52aeeb0d8afdce734166747fce53541" },
            { "Cars", "8a8dfa2a2b7efe842f886c707d1af7ea54eb0f04" },
            { "Cartoon/Comic", "f8bfa9dc486502000c40688173928ba3ac276806" },
            { "CityScape", "8bdd7bd2e50218e0113c31081d82d61af1487f86" },
            { "Computer/Tech", "da9dbb19af79ba9727f82a45d07e31e1902efdfe" },
            { "Digital/Artwork", "6428f62b9f30541d074dd6dc396e4ed512295d20" },
            { "Dual Monitor", "eeab36abfc4dc4e40bb3ed01286848431957ee69" },
            { "Fantasy", "9341a3a4c9acf6aa892f31119b43197a8ead2d6d" },
            { "Food/Drink", "7ad3e0b00865cef25d6a4cf2ac4f4a0203b0f01b" },
            { "Girls", "b9852f3cb23163976c69d3587f43e4ad5ecbdf2f" },
            { "Guns", "66081351108b1d3ff5be77ce8980094cdb64cb08" },
            { "Holiday", "9e4ec607f5e4b911f3fecfc7a512dd4132845376" },
            { "Humor", "42cd11957e1833eda69ea8c1194265b5a65cd73b" },
            { "Industrial", "ff0cc7802169849597065a7717833987d8dd3639" },
            { "Informational", "d80a5db3f1d6a90a683f4b3bb7e108f44b863c2d" },
            { "Insects", "8bf032c27bdac9576b07e41cfb06fb4094ed0a91" },
            { "iPhone/Mobile", "906e00bfaccd6138901f335da19aadc45703b146" },
            { "Love/Hate", "bdb0a973c81e1660554e14a3160fa7a7bb68c026" },
            { "Misc", "ea5e4f09756e6c08976b38baa94078901b0e957a" },
            { "Movies", "8bd3e724702a32ef4679dd6d0a6b7c27025f8372" },
            { "Music", "a09d9bb46ff5d472b1dc0fa9f1db346deb818d4d" },
            { "Nature", "0bcd9385efa3d6e5f7770df57bbbbb31ad0ea22a" },
            { "People", "865f174e89f243c3cea6503dfa417f73c7641edb" },
            { "Quotes/Worded", "0d070b33f42daa623b094cac92398e0270969681" },
            { "Sci-Fi", "330dcff84dd4f4a72302bd9ca15ecd3bdcb19af2" },
            { "Science", "be42faa61e0ec05f65b3d4c1a5eb47fb1b7ce9a4" },
            { "Space", "98020701bc0956f200825e301acabbf8a9b51254" },
            { "Sports", "6cc6b80bb685e537dc2e47a8d6e07e61efcd8bea" },
            { "Television", "8f370355f4b6a4a01c35d62a39f36d5eda94b8d8" },
            { "Video Games", "62bd7ee3dcffed15e0817808182c923c010cf6c2" },
            { "Vintage/Historical", "8e0878c979be37dac22c03aa7e44102a0b657d0b" },
            { "War/Military", "969b3881d7010b3aaeb2b7f9281bd73fe66b77ae" }
        };

        private static string type;
        private static string category;
        private static string filter;
        private static string filterVal;
        private static string file;
        private static bool keep;

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        static void Main(string[] args)
        {
            LoadConfig();

            string message = type;
            if (!string.IsNullOrWhiteSpace(category))
            {
                message = string.Format("most {0} wallpaper in the {1} category", filter, category).ToLower();
            }

            Console.WriteLine(string.Format("Setting wallpaper to the {0}.", message));
            Console.WriteLine(string.Format("Saving file to {0}", file));

            SetWallpaper(GetWallpaperURL());
        }

        static string GetWallpaperURL()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc;
            string imageSrc ="";
            string imageURL = "";

            if (type == "Category")
            {
                // Get the id string
                string catId = "";
                if (category.Equals("RANDOM"))
                {
                    Random rand = new Random();
                    catId = catDict.ElementAt(rand.Next(0, catDict.Count)).Value;
                }
                else
                {
                    catId = catDict[category];
                }

                // Category. Must build URL.
                string pageUrl = string.Format("{0}/category.php?action=catcontent&c={1}&t=a&l=20&r=&cat={2}", baseURL, filterVal, catId);

                doc = web.Load(pageUrl);
                imageSrc = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/div[1]/a/img").Attributes["src"].Value;
                imageURL = string.Format("{0}{1}", baseURL, imageSrc.Substring(imageSrc.IndexOf("image=") + "image=".Length)).Replace("small/small", "big/big");  
            }
            else
            {
                // Wallpaper of the Day. Use baseUrl.
                doc = web.Load(baseURL);
                imageSrc = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_leftcol\"]/div[2]/div/div[1]/a/img").Attributes["src"].Value;
                imageURL = string.Format("{0}{1}", baseURL, imageSrc.Substring(imageSrc.IndexOf("image=") + "image=".Length));
            }

            return imageURL;
        }

        static void SetWallpaper(string URL)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(URL, file);

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, file, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }

        static void LoadConfig()
        {
            XDocument config = XDocument.Load(configPath);

            foreach (XElement elem in config.Root.Elements())
            {
                switch (elem.Name.LocalName)
                {
                    case "type":
                        type = elem.Value;
                        break;
                    case "category":
                        category = elem.Value;
                        break;
                    case "filter":
                        filter = elem.Value;
                        filterVal = elem.Value == "Recent" ? "0" : "1";
                        break;
                    case "file":
                        file = Environment.ExpandEnvironmentVariables(elem.Value);
                        break;
                    case "keep":
                        keep = elem.Value == "Yes";
                        break;
                }

            }
        }
    }
}
