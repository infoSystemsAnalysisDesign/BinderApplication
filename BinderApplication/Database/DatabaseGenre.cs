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
 * Store the BSON document before its cleansed with the first entry being the current date
 * Auto-generate the table if not exists (should be automatic)
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
 * 
 * --
 * 
 * After app login, check if genre table for user exists
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
                .Set("Date", DateTime.Now.ToString("yyyy-MM-dd"))
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

    }
}
