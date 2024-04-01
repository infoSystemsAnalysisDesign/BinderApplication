using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
 */ 


namespace BinderApplication.Database
{
    public class DatabaseBook
    {
        private readonly IMongoDatabase database;

        // Determines the amount of books that can be pulled at once
        private readonly int swipeLimit = 40;

        public DatabaseBook()
        {
            this.database = DatabaseConnection.Instance.GetDatabase();
        }

        public List<BookModel> RetrieveBooksFromDatabase()
        {
            //All genres (how we do it for now for testing)
            List<string> genres = new List<string> { "Drama", "Essay", "Fiction", "History", "Horror", "NonFiction", "Novel", "Philosophy", "Poetry", "Politics", "Psychology", "Romance", "Science", "Spirituality", "Suspense", "Thriller" };

            //Randomly select amount of genres (also for testing)
            Random random = new Random();
            int numGenresToPullFrom = random.Next(5, 17);

            //Randomize the genres list and take the first 'numGenresToPullFrom' genres
            genres = genres.OrderBy(g => random.Next()).Take(numGenresToPullFrom).ToList();

            //Calculate how many books to pull from each genre
            int booksPerGenre = swipeLimit / numGenresToPullFrom;

            //This list should store all books
            List<BookModel> allBooks = new List<BookModel>();

            foreach (string genre in genres)
            {
                var collection = database.GetCollection<BsonDocument>("Books-" + genre);
                var filter = Builders<BsonDocument>.Filter.Empty;

                //Pulls everything from the genre (will optimize later, trying to just get something working first)
                var books = collection.Find(filter).ToList();

                //Shuffle the documents and take 'booksPerGenre' randomly
                books = books.OrderBy(b => random.Next()).Take(booksPerGenre).ToList();

                //Deserialize and add the books to the list
                var bsonString = books.ToJson();
                var cleanedBsonString = BsonStringCleaner.CleanBsonString(bsonString);
                allBooks.AddRange(DeserializeBooks(cleanedBsonString));
            }

            //Shuffle the list of all books and return the first 40
            allBooks = allBooks.OrderBy(b => random.Next()).Take(swipeLimit).ToList();
            return allBooks;
        }

        public static List<BookModel> DeserializeBooks(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<BookModel>>(jsonString);
        }
    }
}
