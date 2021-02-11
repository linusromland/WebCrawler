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
    class Crawler
    {
        string startLink = "";
        List<BsonDocument> links;

        public Crawler(string startlinkIn)
        {
            startLink = startlinkIn;
            links = MongoConnection.ReadAllDB();
            Console.WriteLine(links.Count);
            Crawl();
        }


        public void Crawl()
        {
            //Get initial links
            int count = links.Count;
            if (count == 0) GetLinksFromSite(startLink);
            int j = 0;
            while (true)
            {
                try
                {
                    count = links.Count;
                    links = MongoConnection.ReadAllDB();
                    if (!links[j]["visited"].AsBoolean)
                    {
                        MongoConnection.UpdateDB(links[j]["url"].AsString);

                        GetLinksFromSite(links[j]["url"].AsString);
                    }
                }
                catch
                {
                    break;
                }
                
                j++;
            }
        }

        public void GetLinksFromSite(string currentLink)
        {
            HtmlWeb html = new HtmlWeb();
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

            HtmlNode page = document.DocumentNode;

            List<string> linksOnSite = new List<string>();

            foreach (HtmlNode item in page.QuerySelectorAll("a"))
            {
                if (item.GetAttributes("href").First() == null) continue;

                string url = item.GetAttributes("href").First().Value;

                if (!url.StartsWith("https")) continue;

                //Remove URL fragment
                int hashPos = url.IndexOf('#');
                if (hashPos != -1)
                {
                    url = url.Substring(0, hashPos);
                }

                //Check if link is already on same site
                foreach (string link in linksOnSite)
                {
                    if (link == url)
                    {
                        goto here;
                    }
                }

                //Check if link is already in list
                foreach (BsonDocument link in links)
                {
                    if (link["url"].AsString == url)
                    {
                        goto here;
                    }
                }

                linksOnSite.Add(url);

                //Insert link into MongoDB
                BsonDocument bson = new BsonDocument { { "url", url }, { "origin", currentLink }, { "visited", false } };
                MongoConnection.InsertToDB(bson);

            here:;
            }
        }
    }
}
