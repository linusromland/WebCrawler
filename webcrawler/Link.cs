using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webcrawler
{
    public class Link
    {
        public string link = "default link";
        public string originLink = "origin link"; //TODO: make it possible to add several origins

        public Link(string inLink, string inOrigin){
            link = inLink;
            originLink = inOrigin;
        }
    }
}
