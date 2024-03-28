using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using BinderApplication.Database;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace BinderApplication.Pages
{
    public partial class Binder : ContentPage
    {
        List<JournalEntryModel> journalEntries;

        public Binder()
        {
            InitializeComponent();
            journalEntries = new List<JournalEntryModel>();
            BindingContext = this;
            
        }

        public Binder(JournalEntryModel journalEntry) : this() //construt. to get from other page
        {
            AddJournalEntry(journalEntry);
        }

        public void AddJournalEntry(JournalEntryModel entry)
        {
            journalEntries.Add(entry);
            UpdateDisplay();
        }

        private async void UpdateDisplay()
        {
            var dbConnection = DatabaseConnection.Instance;
            var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase("Binder");

            // Get a reference to the Users collection
            var journalCollection = database.GetCollection<BsonDocument>("Journal");

            // Retrieve the stored email
            var dbLogin = DatabaseLogin.Instance;
            string storedEmail = dbLogin.GetEmail(); // Assuming GetEmail() returns the correct email

            Console.WriteLine($"Stored Email: {storedEmail}"); // Debug statement

            // Create a filter to find the user by email
            var filter = Builders<BsonDocument>.Filter.Eq("Email", storedEmail);

            try
            {
                // Find the user document
                var userJournal = await journalCollection.Find(filter).FirstOrDefaultAsync();

                if (userJournal != null)
                {
                    // Extract and display relevant information from the 'userJournal' document
                    var title = userJournal["Title"].AsString;
                    var date = userJournal["Date"].ToUniversalTime().ToString("yyyy-MM-dd HH:mm");
                    var textNotes = userJournal["Entry"].AsString;

                    Debug.WriteLine("\n\n\n\n\nJOURNAL DATA HERE:\n");
                    Debug.WriteLine(userJournal["Title"].AsString + "\n" + userJournal["Date"].ToUniversalTime().ToString("yyyy-MM-dd HH:mm") + "\n" + userJournal["Entry"].AsString);
                    Debug.WriteLine("\n\n\n\n\n");

                    // Display the journal entry details
                    Console.WriteLine($"Title: {title}\nDate: {date}\nEntry:\n{textNotes}\n");       
                }
                else
                {
                    Console.WriteLine("User not found in the Journal collection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving journal entry: {ex.Message}");
            }
            JournalListView.ItemsSource = journalEntries;
        }


        private void OnJournalEntryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JournalEntry(this));
        }
    }
}

