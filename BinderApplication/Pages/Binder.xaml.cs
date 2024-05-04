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

/*
Edit Entry Color:
BackgroundColor = Color.FromRgba(221, 221, 221, 255)

Delete Entry Color:
BackgroundColor = Color.FromRgba(153, 153, 153, 255),
*/


namespace BinderApplication.Pages
{
    public partial class Binder : ContentPage
    {
        List<JournalEntryModel> journalEntries;
        int journalId = 0;

        public Binder()
        {
            InitializeComponent();
            journalEntries = new List<JournalEntryModel>();
            UpdateDisplay();
        }

        public Binder(JournalEntryModel journalEntry) : this() //constructor to get from other page
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
                Text = "Add Journal Entry",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromRgba(153, 153, 153, 255),
                TextColor = Color.FromRgba(0, 0, 0, 255)
            };
            navigateButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new JournalEntry(this));
            };

            var mainStackLayout = new StackLayout
            {
                Padding = new Thickness(10),
                Spacing = 15,
                BackgroundColor = Color.FromRgba(221, 221, 221, 255) // Set background color of the page to match the edit button
            };

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

                        var titleLabel = new Label
                        {
                            Text = $"Title: {title}",
                            FontAttributes = FontAttributes.Bold, // Making the title bold
                            Margin = new Thickness(0, 0, 0, 5), // Adding margin at the bottom for spacing
                            LineHeight = 1.2 // Adjust line spacing
                        };
                        var dateLabel = new Label
                        {
                            Text = $"Date: {date}",
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), // Setting smaller font size for date
                            Margin = new Thickness(0, 0, 0, 5), // Adding margin at the bottom for spacing
                            LineHeight = 1.2 // Adjust line spacing
                        };
                        var entryLabel = new Label
                        {
                            Text = $"Entry: {textNotes}",
                            Margin = new Thickness(0, 0, 0, 10), // Adding margin at the bottom for spacing
                            LineHeight = 1.2 // Adjust line spacing
                        };


                        var editButton = new Button
                        {
                            Text = "Edit Entry",
                            BackgroundColor = Color.FromRgba(221, 221, 221, 255), // Light gray background color for edit button
                            TextColor = Color.FromRgba(0, 0, 0, 255) // Black text color for edit button
                        };
                        editButton.Clicked += async (sender, args) =>
                        {
                            await Navigation.PushAsync(new JournalEntry(this, title, textNotes));
                        };

                        var deleteButton = new Button
                        {
                            Text = "Delete Entry",
                            BackgroundColor = Color.FromRgba(153, 153, 153, 255), // Darker gray background color for delete button
                            TextColor = Color.FromRgba(0, 0, 0, 255) // Black text color for delete button
                        };
                        deleteButton.Clicked += async (sender, args) =>
                        {
                            bool result = await DisplayAlert("Confirmation", "Are you sure you want to delete this journal entry?", "OK", "Cancel");
                            if (result)
                            {
                                DatabaseJournal dbJournal = new DatabaseJournal();
                                dbJournal.DeleteJournalEntry(title);
                                UpdateDisplay();
                            }
                            else
                            {
                                await DisplayAlert("Deletion Cancelled", "The journal deletion was cancelled.", "OK");
                            }
                        };

                        var buttonStackLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 10 // Adjust spacing between buttons as needed
                        };
                        buttonStackLayout.Children.Add(editButton);
                        buttonStackLayout.Children.Add(deleteButton);

                        // Create a Frame for each journal entry with a gray background
                        var entryFrame = new Frame
                        {
                            BackgroundColor = Color.FromRgba(239, 239, 239, 255), // Gray background color for each journal entry
                            CornerRadius = 10, // Rounded corners
                            Margin = new Thickness(10) // Margin for spacing between entries
                        };

                        // Add labels and button layout to the frame
                        entryFrame.Content = new StackLayout
                        {
                            Children = { titleLabel, dateLabel, entryLabel, buttonStackLayout }
                        };

                        mainStackLayout.Children.Add(entryFrame);
                    }

                }
            }
            catch (Exception ex)
            {
                var errorLabel = new Label { Text = $"Error retrieving journal entries: {ex.Message}" };
                mainStackLayout.Children.Add(errorLabel);
            }

            var scrollView = new ScrollView();
            scrollView.Content = mainStackLayout;

            Content = scrollView;
        }
    }
}
