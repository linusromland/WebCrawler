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

            string hej = "";


            links.Add(new Link(hej, ""));
            return links;
        }
    }
}
