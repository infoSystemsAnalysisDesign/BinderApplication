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

            var dbConnection = DatabaseConnection.Instance;
            DatabaseJournal dbJournal = new DatabaseJournal();

            dbJournal.SaveJournalEntry(entryTitle.Text, entryJournal.Text);

            // Navigate back to the Binder page
            Navigation.PopAsync();
        }
    }
}