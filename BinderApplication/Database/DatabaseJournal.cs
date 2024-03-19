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
    public class DatabaseJournal
    {
        private readonly IMongoDatabase database;

        public DatabaseJournal()
        {
            this.database = DatabaseConnection.Instance.GetDatabase();
        }

        public void SaveJournalEntry(string title, string entry)
        {
            var collection = database.GetCollection<BsonDocument>("Journal");
            DateTime date = DateTime.Now;

            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();

            var document = new BsonDocument
            {
                { "Email", email},
                { "Date", date},
                { "Title", title},
                { "Entry", entry}
            };
            collection.InsertOne(document);
        }

        public List<JournalEntryModel> RetrieveJournalEntries()
        {
            var collection = database.GetCollection<BsonDocument>("Journal");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var journals = collection.Find(filter).ToList();
            var bsonString = journals.ToJson();
            var cleanedBsonString = BsonStringCleaner.CleanBsonString(bsonString);
            return DeserializeJournals(cleanedBsonString);
        }

        private static List<JournalEntryModel> DeserializeJournals(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<JournalEntryModel>>(jsonString);
        }
    }
}
