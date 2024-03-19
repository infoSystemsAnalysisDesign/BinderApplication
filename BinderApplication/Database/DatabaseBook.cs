﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BinderApplication.Database
{
    public class DatabaseBook
    {
        private readonly IMongoDatabase database;

        public DatabaseBook()
        {
            this.database = DatabaseConnection.Instance.GetDatabase();
        }

        public List<BookModel> RetrieveBooksFromDatabase()
        {
            string genre = "Fiction";

            var collection = database.GetCollection<BsonDocument>("Books-" + genre);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var books = collection.Find(filter).ToList();
            var bsonString = books.ToJson();
            var cleanedBsonString = BsonStringCleaner.CleanBsonString(bsonString);
            return DeserializeBooks(cleanedBsonString);
        }

        public static List<BookModel> DeserializeBooks(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<BookModel>>(jsonString);
        }
    }
}
