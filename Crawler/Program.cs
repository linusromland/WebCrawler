using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using MongoDB.Bson;


namespace webcrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoConnection.ConnectToDB();
            links = MongoConnection.ReadAllDB();
            Crawler.Crawl();
        }

        public static List<Link> links = new List<Link>();

    }
}
