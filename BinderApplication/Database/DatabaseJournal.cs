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

        public bool CheckTitleExists(string title)
        {
            var collection = database.GetCollection<BsonDocument>("Journal");

            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();

            // Check if a journal with the same title already exists for the user
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Email", email),
                Builders<BsonDocument>.Filter.Eq("Title", title)
            );
            var existingJournal = collection.Find(filter).FirstOrDefault();

            return existingJournal != null;
        }

        public void DeleteJournalEntry(string title)
        {
            var collection = database.GetCollection<BsonDocument>("Journal");

            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();

            //Check if a journal with the same title already exists for the user
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Email", email),
                Builders<BsonDocument>.Filter.Eq("Title", title)
            );
            var existingJournal = collection.Find(filter).FirstOrDefault();

            //If the journal exists, delete it
            if (existingJournal != null)
            {
                collection.DeleteOne(filter);
            }
        }


        private static List<JournalEntryModel> DeserializeJournals(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<JournalEntryModel>>(jsonString);
        }
    }
}
