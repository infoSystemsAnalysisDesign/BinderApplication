using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using BinderApplication.Database;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Windows.Input;


namespace BinderApplication.Pages
{
    public partial class Binder : ContentPage
    {
        List<JournalEntryModel> journalEntries;

        public Binder()
        {
            UpdateDisplay();
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateDisplay();
        }

        private async void UpdateDisplay()
        {
            var dbConnection = DatabaseConnection.Instance;
            var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase("Binder");
            var journalCollection = database.GetCollection<BsonDocument>("Journal");
            
            var dbLogin = DatabaseLogin.Instance;
            string storedEmail = dbLogin.GetEmail();
            var navigateButton = new Button
            {
                Text = "Add Journal Entry"
            };
            navigateButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new JournalEntry(this));
            };

            StackLayout mainStackLayout = new StackLayout();
            
            mainStackLayout.Children.Add(navigateButton);

            var filter = Builders<BsonDocument>.Filter.Eq("Email", storedEmail);

            try
            {
                var userJournals = await journalCollection.Find(filter).ToListAsync();

                if (userJournals.Count > 0)
                {
                    foreach (var userJournal in userJournals)
                    {
                        var title = userJournal["Title"].AsString;
                        var date = userJournal["Date"].ToUniversalTime().ToString("yyyy-MM-dd HH:mm");
                        var textNotes = userJournal["Entry"].AsString;
                        

                        // Create labels for each journal entry and add them to the stack layout
                        var titleLabel = new Label { Text = $"Title: {title}" };
                        var dateLabel = new Label { Text = $"Date: {date}" };
                        var entryLabel = new Label { Text = $"Entry: {textNotes}" };

                        var editButton = new Button 
                        { 
                            Text = "Edit Entry", 
                        };
                        editButton.Clicked += async (sender, args) =>
                        {
                            await Navigation.PushAsync(new JournalEntry(this));
                        };

                        var deleteButton = new Button 
                        { 
                            Text = "Delete Entry" 
                        };
                        deleteButton.Clicked += async (sender, args) =>
                        {
                            //LOGIC TO DELETE JOURNAL BUTTON
                        };

                        // Add some padding between each entry
                        var padding = new Thickness(0, 5);

                        // Add the labels to the stack layout
                        mainStackLayout.Children.Add(titleLabel);
                        mainStackLayout.Children.Add(dateLabel);
                        mainStackLayout.Children.Add(entryLabel);
                        mainStackLayout.Children.Add(editButton);
                        mainStackLayout.Children.Add(deleteButton);

                        // Add padding
                        mainStackLayout.Children.Add(new BoxView { HeightRequest = 10 });
                    }
                }
                else
                {
                    // No entries found message
                    var noEntriesLabel = new Label { Text = "No journal entries found." };
                    mainStackLayout.Children.Add(noEntriesLabel);
                }
            }
            catch (Exception ex)
            {
                // Display error message if there's an exception
                var errorLabel = new Label { Text = $"Error retrieving journal entries: {ex.Message}" };
                mainStackLayout.Children.Add(errorLabel);
            }
            // Create a ScrollView and add the stackLayout to it
            var scrollView = new ScrollView();
            scrollView.Content = mainStackLayout;

            // Set the content of the page to the scrollView
            Content = scrollView;
        }


        private void OnJournalEntryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JournalEntry(this));
        }

    }
}

