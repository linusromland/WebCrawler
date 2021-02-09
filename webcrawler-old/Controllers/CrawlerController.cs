using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace webcrawler.Controllers
{
    [ApiController]
    [Route("/")]
    public class CrawlerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Link> Get()
        {
            List<Link> links = new List<Link>();

            var html = new HtmlWeb();
            var document = html.Load("https://romland.space");

            var page = document.DocumentNode;

            foreach(var item in page.QuerySelectorAll("a"))
            {
                Console.WriteLine(item.GetAttributes("href").First().Value);
                links.Add(new Link(item.GetAttributes("href").First().Value, "https://romland.space"));
            }

            return links;
        }
    }
}
