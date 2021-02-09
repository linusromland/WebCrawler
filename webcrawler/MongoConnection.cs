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

            

            var database = dbClient.GetDatabase ("AntennsCrawler");
            collection = database.GetCollection<BsonDocument> ("Links");
        }

        public static void InsertToDB(BsonDocument doc)
        {
            collection.InsertOne(doc);
        }
    }
}