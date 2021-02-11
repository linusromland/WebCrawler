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



            IMongoDatabase database = dbClient.GetDatabase("crawler");
            collection = database.GetCollection<BsonDocument>("Links");
        }

        public static void InsertToDB(BsonDocument doc)
        {
            collection.InsertOne(doc);
        }

        public static void UpdateDB(string link)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("url", link);
            var update = Builders<BsonDocument>.Update.Set("visited", true);
            collection.UpdateOne(filter, update);
        }


        public static List<BsonDocument> ReadAllDB()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public static BsonDocument CheckExistDB(string link)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("url", link);
            return collection.Find(filter).FirstOrDefault();
        }
    }
}