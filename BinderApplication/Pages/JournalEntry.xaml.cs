//namespace BinderApplication.Pages;

////public partial class JournalEntry : ContentPage
////{
////	public JournalEntry()
////	{
////		InitializeComponent();
////	}
////}

//// JournalEntryPage.xaml.cs
//using Microsoft.Maui.Controls;


//{
//    public partial class JournalEntry : ContentPage
//    {
//        public JournalEntry()
//        {
//            InitializeComponent();
//        }

//        private void OnSaveClicked(object sender, EventArgs e)
//        {
//            // Save the journal entry and navigate back to the Binder page
//            string journalEntryText = entryJournal.Text;

//            // Add your logic to save the journal entry (e.g., use a database, etc.)

//            // Navigate back to the Binder page
//            Navigation.PopAsync();
//        }
//    }

// JournalEntry.xaml.cs
using Microsoft.Maui.Controls;
using System;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BinderApplication.Pages
{
    public partial class JournalEntry : ContentPage
    {
        private readonly Binder _binderPage;

        // Constructor to receive the Binder instance
        public JournalEntry(Binder binderPage)
        {
            InitializeComponent();
            _binderPage = binderPage;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Save the journal entry and send it back to the Binder page
            JournalEntryModel journalEntry = new JournalEntryModel
            {
                Title = entryTitle.Text,
                Date = DateTime.Now, // You can set a specific date if needed
            };

            // Add the text notes to the journal entry
            journalEntry.TextNotes.Add(entryJournal.Text);

            // Pass the journal entry back to the Binder page
            _binderPage?.AddJournalEntry(journalEntry);

            //Call database (((REFACTOR)))
            JournalDatabase();

            // Navigate back to the Binder page
            Navigation.PopAsync();
        }

        /*
         * THIS ABSOLUTELY NEEDS TO BE REFACTORED INTO ITS OWN CLASS
         * this is just so we can demonstrate to Dr.B
         */
        public void JournalDatabase()
        {
            

            /* Refactor this to be an independent class.
             * This is for testing */

            const string connectionUri = "mongodb+srv://Binder:AlsoBinder1@cluster0.ibrxa6e.mongodb.net/?retryWrites=true&w=majority";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //connectionUri
            MongoClient dbClient = new MongoClient(connectionUri);

            //stores all databases into var that can be displayed later (for testing)
            var dbList = dbClient.ListDatabases().ToList();

            //Connects to the Binder database and then the Journal table
            var database = dbClient.GetDatabase("Binder");
            var collection = database.GetCollection<BsonDocument>("Journal");
            
            //Finds and displays the first entry found in the Journal table
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(firstDocument);

            //Adding the date
            DateTime date = DateTime.Now;

            //Formatting and insertion of the data 
            var document = new BsonDocument
            {
                { "Date", date},
                { "Title", entryTitle.Text},
                { "Entry", entryJournal.Text}
            };

            collection.InsertOne(document);
        }
    }
}