using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;


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

        public List<BookModel> RetrieveBooksFromDatabase(List<string> trueGenres)
        {
            // Get the date from the database
            DateTime? storedDate = GetDateFromDB();

            // If the stored date is the same as today's date, load the books from the database
            if (storedDate.HasValue && storedDate.Value.Date == DateTime.UtcNow.Date)
            {
                var currentBooks = LoadCurrentDaysBooksFromDB();
                return currentBooks;
            }

            // Otherwise, retrieve new books

            //All genres (how we do it for now for testing)
            List<string> genres = trueGenres;
            Random random = new Random();
            int numGenresToPullFrom = genres.Count;

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

            StoreCurrentDaysBooksIntoDB(allBooks);

            return allBooks;
        }


        public static List<BookModel> DeserializeBooks(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<BookModel>>(jsonString);
        }

        public void StoreCurrentDaysBooksIntoDB(List<BookModel> currentDaysBooks)
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var collection = database.GetCollection<BsonDocument>("User-CurrentDaysBooks");

            //Clear the collection so it can change each day
            collection.DeleteMany(_ => true);

            //Insert the current date (without time) as the first document
            var dateDocument = new BsonDocument { { "DateEntry", DateTime.UtcNow.Date } };
            collection.InsertOne(dateDocument);

            //Set the Email field for each book and insert them
            foreach (var book in currentDaysBooks)
            {
                book.Email = email;
                var bookDocument = book.ToBsonDocument();
                collection.InsertOne(bookDocument);
            }
        }

        public List<BookModel> LoadCurrentDaysBooksFromDB()
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var collection = database.GetCollection<BsonDocument>("User-CurrentDaysBooks");

            // Retrieve the date document
            var dateFilter = Builders<BsonDocument>.Filter.Exists("DateEntry");
            var dateDocument = collection.Find(dateFilter).FirstOrDefault();
            DateTime? date = null;
            if (dateDocument != null)
            {
                date = dateDocument["DateEntry"].ToUniversalTime();
            }

            // Retrieve the book documents
            var bookFilter = Builders<BsonDocument>.Filter.Not(dateFilter);
            var bookDocuments = collection.Find(bookFilter).ToList();

            // Deserialize the book documents into BookModel objects
            var books = new List<BookModel>();
            foreach (var bookDocument in bookDocuments)
            {
                var book = BsonSerializer.Deserialize<BookModel>(bookDocument);
                books.Add(book);
            }

            return books;
        }
        
        public DateTime? GetDateFromDB()
        {
            var dbLogin = DatabaseLogin.Instance;
            string email = dbLogin.GetEmail();
            var collection = database.GetCollection<BsonDocument>("User-CurrentDaysBooks");

            // Retrieve the date document
            var dateFilter = Builders<BsonDocument>.Filter.Exists("DateEntry");
            var dateDocument = collection.Find(dateFilter).FirstOrDefault();
            if (dateDocument != null)
            {
                return dateDocument["DateEntry"].ToUniversalTime();
            }

            return null;
        }



    }
}
