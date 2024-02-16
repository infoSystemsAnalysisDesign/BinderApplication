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

            // Navigate back to the Binder page
            Navigation.PopAsync();
        }
    }
}