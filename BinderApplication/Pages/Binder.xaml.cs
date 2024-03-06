using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace BinderApplication.Pages
{
    public partial class Binder : ContentPage
    {
        List<JournalEntryModel> journalEntries;

        public Binder()
        {
            InitializeComponent();
            journalEntries = new List<JournalEntryModel>();
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

        private void UpdateDisplay()
        {
            labelJournalEntry.Text = string.Join("\n\n", journalEntries.Select(entry =>
                $"{entry.Title}\n{entry.Date.ToString("yyyy-MM-dd HH:mm")}\n{string.Join("\n", entry.TextNotes)}"));
        }

        private void OnJournalEntryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JournalEntry(this));
        }
    }
}

