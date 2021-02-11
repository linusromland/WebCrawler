using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using MongoDB.Bson;
using System.Threading;

namespace webcrawler
{
    class Program
    {

        public static int index = 0;
        static void Main(string[] args)
        {
            MongoConnection.ConnectToDB();
            List<Thread> Threads = new List<Thread>();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("New");
                Threads.Add(new Thread(createCrawler));
                Threads[i].Start();
            }

        }
        public static void createCrawler()
        {
            Crawler tmp = new Crawler("https://romland.space/");
        }
    }
}
