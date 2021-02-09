using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace webcrawler
{
    public class Crawler
    {
        static List<Link> links = new List<Link>();
        public static void Crawl()
        {
            var html = new HtmlWeb();
            var document = html.Load("https://romland.space");

            var page = document.DocumentNode;

            foreach (var item in page.QuerySelectorAll("a"))
            {
                Console.WriteLine(item.GetAttributes("href").First().Value);
                links.Add(new Link(item.GetAttributes("href").First().Value, "https://romland.space"));
            }
        }

        public static void PrintLinks()
        {
            foreach (Link link in links)
            {
                Console.WriteLine("Link: " + link.link + "  Origin: " + link.originLink);
            }
        }
    }
}
