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
            DatabaseLogin dbLogin = DatabaseLogin.Instance;

            public DatabaseLiked()
            {
                this.database = DatabaseConnection.Instance.GetDatabase();
            }

            
            public List<BookModel> RetrieveLikedBooks()
            {
                var collection = database.GetCollection<BsonDocument>("Liked");

                string email = dbLogin.GetEmail();
                var filter = Builders<BsonDocument>.Filter.Eq("Email", email);

                var journals = collection.Find(filter).ToList();
                var bsonString = journals.ToJson();
                var cleanedBsonString = BsonStringCleaner.CleanBsonString(bsonString);
                return DeserializeLikedBooks(cleanedBsonString);
            }

            private static List<BookModel> DeserializeLikedBooks(string jsonString)
            {
                return JsonConvert.DeserializeObject<List<BookModel>>(jsonString);
            }
        }
    }


