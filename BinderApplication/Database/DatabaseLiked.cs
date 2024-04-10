using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BinderApplication.Database
{
    public class DatabaseLiked
    {
            private readonly IMongoDatabase database;

            public DatabaseLiked()
            {
                this.database = DatabaseConnection.Instance.GetDatabase();
            }

            
            public List<BookModel> RetrieveJournalEntries()
            {
                var collection = database.GetCollection<BsonDocument>("Liked");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var journals = collection.Find(filter).ToList();
                var bsonString = journals.ToJson();
                var cleanedBsonString = BsonStringCleaner.CleanBsonString(bsonString);
                return DeserializeJournals(cleanedBsonString);
            }

            private static List<BookModel> DeserializeJournals(string jsonString)
            {
                return JsonConvert.DeserializeObject<List<BookModel>>(jsonString);
            }
        }
    }


