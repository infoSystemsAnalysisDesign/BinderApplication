using BinderApplication.Pages;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication.Database
{
/*
 * PERSONAL NOTE:
 * Have DatabaseBook check if its new day or same day
 * 
 * If its a new day:
 * Clear table data
 * Store date
 * Store BSON
 * 
 * If is the same day
 * Ignore data
 * Pull Books
 * 
 * Both use this function from Books, build a seperate check date function and use that to determine
 * whether to pull new books or the same.
 * 
 * May need to refactor the DatabaseBook into a "SelectBooks" and "GrabBooks"
 */

    public class DatabaseGenre
    {
        private readonly IMongoDatabase database;

        public DatabaseGenre()
        {
            this.database = DatabaseConnection.Instance.GetDatabase();
            //GenerateGenresForAccount(); //TESTING!!
            //UpdateGenresForAccount(); //TESTING!!
        }

        public bool DoesGenresExist()
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var genresCollection = database.GetCollection<BsonDocument>("User-Genres");

            var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
            var document = genresCollection.Find(filter).Limit(1).FirstOrDefault();

            if (document != null)
            {
                Debug.WriteLine("\n\n\n\nDocument exists in the 'User-Genres' collection!\n\n\n\n"); //testing
                return true;
            }
            else
            {
                Debug.WriteLine("\n\n\n\nDocument does not exist in the 'User-Genres' collection!\n\n\n\n"); //testing
                return false;
            }
        }


        public void UpdateGenresForAccount(bool drama, bool essay, bool fiction, bool history, 
            bool horror, bool nonfiction, bool novel, bool philosophy, bool poetry, bool politics,
            bool psychology, bool romance, bool science, bool spirituality, bool suspense, bool thriller)
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();

            var genresCollection = database.GetCollection<BsonDocument>("User-Genres");
            var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
            var update = Builders<BsonDocument>.Update
                .Set("Drama", drama)
                .Set("Essay", essay)
                .Set("Fiction", fiction)
                .Set("History", history)
                .Set("Horror", horror)
                .Set("NonFiction", nonfiction)
                .Set("Novel", novel)
                .Set("Philosophy", philosophy)
                .Set("Poetry", poetry)
                .Set("Politics", politics)
                .Set("Psychology", psychology)
                .Set("Romance", romance)
                .Set("Science", science)
                .Set("Spirituality", spirituality)
                .Set("Suspense", suspense)
                .Set("Thriller", thriller);

            genresCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public Dictionary<string, bool> ReadGenresForAccount()
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var genresCollection = database.GetCollection<BsonDocument>("User-Genres");
            var filter = Builders<BsonDocument>.Filter.Eq("Email", email);
            var document = genresCollection.Find(filter).FirstOrDefault();

            if (document == null)
            {
                Dictionary<string, bool> stringBooleanMap = new Dictionary<string, bool>()
                {
                    { "Drama", false },
                    { "Essay", false },
                    { "Fiction", false },
                    { "History", false },
                    { "Horror", false },
                    { "NonFiction", false },
                    { "Novel", false },
                    { "Philosophy", false },
                    { "Poetry", false },
                    { "Politics", false },
                    { "Psychology", false },
                    { "Romance", false },
                    { "Science", false },
                    { "Spirituality", false },
                    { "Suspense", false },
                    { "Thriller", false }
                };

                return stringBooleanMap;
            }

            var genres = new Dictionary<string, bool>();
            foreach (var element in document.Elements)
            {
                if (element.Name != "_id" && element.Name != "Email" && element.Value.IsBoolean)
                {
                    genres.Add(element.Name, element.Value.AsBoolean);
                }
            }

            return genres;
        }

    }
}
