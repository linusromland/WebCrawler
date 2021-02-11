using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace webcrawler
{
    class MongoConnection
    {
        public static IMongoCollection<BsonDocument> collection;
        public static void ConnectToDB()
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017/");



            IMongoDatabase database = dbClient.GetDatabase("AntennsCrawler");
            collection = database.GetCollection<BsonDocument>("Links");
        }

        public static void InsertToDB(BsonDocument doc)
        {
            collection.InsertOne(doc);
        }

        public static void UpdateDB(string link)
        {
            HtmlNode filter = Builders<BsonDocument>.Filter.Eq("url", link);
            HtmlNode update = Builders<BsonDocument>.Update.Set("visited", true);
            collection.UpdateOne(filter, update);
        }


        public static List<Link> ReadAllDB()
        {
            List<Link> links = new List<Link>();

            HtmlNode documents = collection.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in documents)
            {
                links.Add(new Link(doc["url"].AsString, doc["origin"].AsString, doc["visited"].AsBoolean));
            }

            return links;
        }
    }
}