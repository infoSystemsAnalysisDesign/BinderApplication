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
        }

        public void DoesGenresExist()
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var collectionNames = database.ListCollectionNames().ToList();

            string genresCollectionOfUser = "User-Genres-";
            genresCollectionOfUser += email;
            Debug.WriteLine("\n\n\n\n" + genresCollectionOfUser + "\n\n\n\n");


            if (collectionNames.Any(name => name == genresCollectionOfUser))
            {
                Debug.WriteLine("\n\n\n\nCollection exists in the 'Binder' database!\n\n\n\n"); //testing
            }
            else
            {
                Debug.WriteLine("\n\n\n\nCollection does not exist in the 'Binder' database!\n\n\n\n"); //testing
            }
        }

        private void GrabGenres()
        {

        }

        private void GenerateHashTable()
        {

        }
    }
}
