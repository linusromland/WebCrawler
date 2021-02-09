﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace webcrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawl();
            PrintLinks();
        }


        static string startLink = "https://github.com";

        static List<Link> links = new List<Link>();
        public static void Crawl()
        {
            var html = new HtmlWeb();
            var document = html.Load(startLink);

            var page = document.DocumentNode;

            foreach (var item in page.QuerySelectorAll("a"))
            {
                string url = item.GetAttributes("href").First().Value;

                if(!url.StartsWith("https")) continue;

                links.Add(new Link(url, startLink));
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
