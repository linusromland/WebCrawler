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
            Crawl();
        }


        static string startLink = "https://romland.space";

        static List<Link> links = new List<Link>();

        public static void Crawl()
        {
            //Get initial links
            if(links.Count == 0) GetLinksFromSite(startLink);

            for (int j = 0; j < links.Count; j++)
            {
                Link link = links[j];
                if (!link.visited)
                {
                    links[j].visited = true;
                    MongoConnection.UpdateDB(link.link);

                    GetLinksFromSite(link.link);
                }
            }
        }

        public static void GetLinksFromSite(string currentLink)
        {
            var html = new HtmlWeb();
            HtmlDocument document;
            Console.WriteLine(currentLink);
            try
            {
                document = html.Load(currentLink);
            }
            catch
            {
                return;
            }
            

            var page = document.DocumentNode;

            foreach (var item in page.QuerySelectorAll("a"))
            {
                if (item.GetAttributes("href").First() == null) continue;

                string url = item.GetAttributes("href").First().Value;

                if (!url.StartsWith("https")) continue;

                //Check if link is already in list
                foreach (var link in links)
                {
                    if(link.link == url){
                        goto here;
                    }
                }

                links.Add(new Link(url, currentLink));
                var bson = new BsonDocument { { "url", url }, { "origin", currentLink }, { "visited", false } };
                MongoConnection.InsertToDB(bson);

                here:;
            }
        }
    }
}
