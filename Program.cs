using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace GachalogDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string downloadPath = Path.GetDirectoryName (System.Reflection.Assembly.GetEntryAssembly().Location);
            string filePath = downloadPath +"\\list.txt";

            if (!File.Exists(filePath))
            {
                Console.Write("Can't find file list.txt in folder " + downloadPath + ". Please make sure the file is placed in the same folder as this exe.\n");

                return;
            }
            else
            {
                var lines = File.ReadLines(filePath, Encoding.UTF8);

                foreach (var line in lines)
                {
                    Regex regex = new Regex(@"\d+");

                    int deckId = int.Parse(regex.Match(line).Value);
                    string uri = "https://www.gachalog.com/download_image/" + deckId;

                    WebClient client = new WebClient();
                    Console.WriteLine("Downloading image for deck " + deckId);

                    client.DownloadFile(uri, downloadPath +  "\\" + deckId.ToString() + ".png");
                }
            }

            Console.WriteLine("All downloads complete.");
        }
    }
}
