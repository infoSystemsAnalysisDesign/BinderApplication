using BinderApplication.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BinderApplication.Pages;

public partial class Liked : ContentPage
{
	public Liked()
	{
		InitializeComponent();
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
        var likedBooks = database.GetCollection<BsonDocument>("Liked");

        var dbLogin = DatabaseLogin.Instance;
        string storedEmail = dbLogin.GetEmail();

        StackLayout mainStackLayout = new StackLayout();

        var filter = Builders<BsonDocument>.Filter.Eq("Email", storedEmail);

        try
        {
            var booksLiked = await likedBooks.Find(filter).ToListAsync();

            if (booksLiked.Count > 0)
            {
                foreach (var book in booksLiked)
                {
                    var volumeInfo = book["VolumeInfo"].AsBsonDocument;
                    var title = volumeInfo["Title"].AsString;
                    var authors = volumeInfo["Authors"].AsBsonArray.Select(b => b.AsString).ToList();
                    var publisher = volumeInfo["Publisher"].AsString;
                    var publishedDate = volumeInfo["PublishedDate"].AsString;
                    var description = volumeInfo["Description"].AsString;

                    // Create labels for each book and add them to the stack layout
                    var titleLabel = new Label { Text = $"Title: {title}" };
                    var authorsLabel = new Label { Text = $"Authors: {string.Join(", ", authors)}" };
                    var publisherLabel = new Label { Text = $"Publisher: {publisher}" };
                    var publishedDateLabel = new Label { Text = $"Published Date: {publishedDate}" };
                    var descriptionLabel = new Label { Text = $"Description: {description}" };

                    // Add some padding between each entry
                    var padding = new Thickness(0, 5);

                    // Add the labels to the stack layout
                    mainStackLayout.Children.Add(titleLabel);
                    mainStackLayout.Children.Add(authorsLabel);
                    mainStackLayout.Children.Add(publisherLabel);
                    mainStackLayout.Children.Add(publishedDateLabel);
                    mainStackLayout.Children.Add(descriptionLabel);

                    // Add padding
                    mainStackLayout.Children.Add(new BoxView { HeightRequest = 10 });
                }
            }
            else
            {
                // No entries found message
                var noEntriesLabel = new Label { Text = "No liked entries found." };
                mainStackLayout.Children.Add(noEntriesLabel);
            }
        }
        catch (Exception ex)
        {
            // Display error message if there's an exception
            var errorLabel = new Label { Text = $"Error retrieving liked entries: {ex.Message}" };
            mainStackLayout.Children.Add(errorLabel);
        }
        // Create a ScrollView and add the stackLayout to it
        var scrollView = new ScrollView();
        scrollView.Content = mainStackLayout;

        // Set the content of the page to the scrollView
        Content = scrollView;
    }

}