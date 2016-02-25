using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                args = new string[] { @"C:\Users\Greg\Music\SoundCloud.html" };
            }

            //user opens this program with the html 
            // from http://9soundclouddownloader.com/ after finding all songs in a playlist

            // get file name from args
            string fileName = "";
            if (args.Length > 0 && args[0].Length > 0)
            {
                fileName = args[0];
            }

            // load file using htmlagilitypack
            HtmlDocument doc = new HtmlDocument();
            doc.Load(fileName);

            // parse for song names and links
            HtmlNode docNode = doc.DocumentNode;
            HtmlNodeCollection collection = docNode.SelectNodes("//ol//a");

            // create webclient to download files
            //WebClient wc = new WebClient();
            //wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            // download each of the files
            foreach (HtmlNode node in collection)
            {
                string name = node.InnerText;
                string url = node.Attributes.First().Value;
                Uri uri = new Uri(url);
                string saveLocation = @"C:\Users\Greg\Music\Ladies\";

                // rename downloaded file
                foreach (string item in url.Split(new char[] {'/', '?'}))
                {
                    if (item.Contains(".mp3"))
                    {
                        try
                        {
                            File.Move(saveLocation + item, saveLocation + name + ".mp3");
                        }
                        catch (System.IO.FileNotFoundException e)
                        {
                            // file doesn't exist
                        }
                    }
                }
                

                Console.WriteLine(name);
                //wc.DownloadFile(uri, saveLocation + name);

                //Thread.Sleep(5000);
            }

            // save file as name.
        }
    }
}
