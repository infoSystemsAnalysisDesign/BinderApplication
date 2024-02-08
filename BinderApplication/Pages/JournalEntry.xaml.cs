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

// JournalEntryPage.xaml.cs
using Microsoft.Maui.Controls;

namespace BinderApplication.Pages
{
    public partial class JournalEntry : ContentPage
    {
        public JournalEntry()
        {
            InitializeComponent();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Save the journal entry and send it back to the Binder page
            string journalEntryText = entryJournal.Text;

            // Pass the journal entry back to the Binder page
            Navigation.PushAsync(new Binder(journalEntryText));
        }
    }
}
