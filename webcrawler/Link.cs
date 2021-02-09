using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcrawler
{
    public class Link
    {
        public string link = "default link";
        public string originLink = "origin link"; //TODO: make it possible to add several origins

        public bool visited = false;

        public Link(string inLink, string inOrigin, bool inVisited = false){
            link = inLink;
            originLink = inOrigin;
            visited = inVisited;
        }
    }
}
