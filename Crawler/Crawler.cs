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
        List<Link> links;
        string startLink = "";

        public Crawler(List<Link> linksIn, string startlinkIn)
        {
            startLink = startlinkIn;
            links = linksIn;
            Crawl();
        }


        public void Crawl()
        {
            //Get initial links
            if (links.Count == 0) GetLinksFromSite(startLink);

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
                foreach (Link link in links)
                {
                    if (link.link == url)
                    {
                        goto here;
                    }
                }

                links.Add(new Link(url, currentLink));
                linksOnSite.Add(url);

                //Insert link into MongoDB
                BsonDocument bson = new BsonDocument { { "url", url }, { "origin", currentLink }, { "visited", false } };
                MongoConnection.InsertToDB(bson);

            here:;
            }
        }
    }
}
