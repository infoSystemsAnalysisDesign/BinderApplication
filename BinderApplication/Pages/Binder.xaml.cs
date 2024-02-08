////namespace BinderApplication.Pages;

////public partial class Binder : ContentPage
////{
////	public Binder()
////	{
////		InitializeComponent();
////	}
////}

//// Binder.xaml.cs
//using Microsoft.Maui.Controls;

//namespace BinderApplication.Pages;

//public partial class BinderPage : ContentPage
//{
//    public BinderPage()
//    {
//        InitializeComponent();
//    }

//    private void OnJournalEntryClicked(object sender, EventArgs e)
//    {
//        // Navigate to the Journal Entry page
//        Navigation.PushAsync(new JournalEntry());
//    }
//}
// BinderPage.xaml.cs
using Microsoft.Maui.Controls;

namespace BinderApplication.Pages;

public partial class Binder : ContentPage
{
    public Binder()
    {
        InitializeComponent();
    }

    // Constructor to receive the journal entry from JournalEntryPage
    public Binder(string journalEntry)
        : this()
    {
        // Display the entered journal entry
        labelJournalEntry.Text = journalEntry;
    }

    private void OnJournalEntryClicked(object sender, EventArgs e)
    {
        // Navigate to the Journal Entry page
        Navigation.PushAsync(new JournalEntry());
    }
}
