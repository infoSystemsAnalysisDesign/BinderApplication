using Microsoft.Maui.Controls;
using System;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using BinderApplication.Database;
using System.Data.Common;

namespace BinderApplication.Pages
{
    public partial class JournalEntry : ContentPage
    {
        private readonly Binder _binderPage;
        string editTitle;
        string editEntry;
        bool editMode;

        // Constructor to receive the Binder instance
        public JournalEntry(Binder binderPage)
        {
            InitializeComponent();
            _binderPage = binderPage;

            editTitle = null;
            editEntry = null;
            editMode = false;
        }

        public JournalEntry(Binder binderPage, string title, string entry)
        {
            InitializeComponent();
            _binderPage = binderPage;

            editTitle = title;
            editEntry = entry;
            editMode = true;

            entryTitle.Text = editTitle;
            entryJournal.Text = editEntry;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var dbConnection = DatabaseConnection.Instance;
            DatabaseJournal dbJournal = new DatabaseJournal();

            // Check if a journal with the same title already exists
            bool titleExists = dbJournal.CheckTitleExists(entryTitle.Text);

            if (titleExists)
            {
                // Display an alert if a journal with the same title exists
                await DisplayAlert("Title Exists", "You already have a journal with that title, please rename it", "OK");
            }
            else if (entryTitle.Text == null || entryJournal.Text == null
                || entryTitle.Text == "" || entryJournal.Text == "")
            {
                await DisplayAlert("Oops!", "The journal must have a title and entry.", "OK");
            }
            else
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

                dbJournal.SaveJournalEntry(entryTitle.Text, entryJournal.Text);

                // Navigate back to the Binder page
                await Navigation.PopAsync();
            }
        }

    }
}