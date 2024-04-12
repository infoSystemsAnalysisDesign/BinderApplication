using BinderApplication.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BinderApplication.Pages
{
    public partial class Liked : ContentPage
    {
        public Liked()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#778899"); // Set background color
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
                        var previewLink = volumeInfo["PreviewLink"].AsString;

                        // Create labels for each book and add them to the stack layout
                        var titleLabel = new Label { Text = $"Title: {title}", Margin = new Thickness(10, 5, 10, 0) }; // Add margin for spacing
                        var authorsLabel = new Label { Text = $"Authors: {string.Join(", ", authors)}", Margin = new Thickness(10, 0) }; // Add margin for spacing
                        var publisherLabel = new Label { Text = $"Publisher: {publisher}", Margin = new Thickness(10, 0) }; // Add margin for spacing
                        var publishedDateLabel = new Label { Text = $"Published Date: {publishedDate}", Margin = new Thickness(10, 0) }; // Add margin for spacing
                        var previewLinkLabel = new Label { Text = $"Link to book on Google Books: {previewLink}", Margin = new Thickness(10, 0, 10, 5) }; // Add margin for spacing

                        // Add tap gesture recognizer to previewLinkLabel
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            // On tap, copy the previewLink to the clipboard
                            await Clipboard.SetTextAsync(previewLink);
                            await DisplayAlert("Copied to Clipboard", $"Link to \"{title}\" copied to clipboard", "OK");
                        };
                        previewLinkLabel.GestureRecognizers.Add(tapGestureRecognizer);

                        // Add border to each entry
                        var frame = new Frame
                        {
                            Content = new StackLayout
                            {
                                Children = { titleLabel, authorsLabel, publisherLabel, publishedDateLabel, previewLinkLabel }
                            },
                            Margin = new Thickness(10), // Add margin for spacing between entries
                            BackgroundColor = Color.FromHex("#ffffff"), // Set background color of each entry
                            CornerRadius = 10, // Rounded corners
                            HasShadow = true // Add shadow
                        };

                        // Add the frame to the main stack layout
                        mainStackLayout.Children.Add(frame);
                    }
                }
                else
                {
                    // No entries found message
                    var noEntriesLabel = new Label { Text = "No liked entries found.", Margin = new Thickness(10) }; // Add margin for spacing
                    mainStackLayout.Children.Add(noEntriesLabel);
                }
            }
            catch (Exception ex)
            {
                // Display error message if there's an exception
                var errorLabel = new Label { Text = $"Error retrieving liked entries: {ex.Message}", Margin = new Thickness(10) }; // Add margin for spacing
                mainStackLayout.Children.Add(errorLabel);
            }

            // Create a ScrollView and add the stackLayout to it
            var scrollView = new ScrollView();
            scrollView.Content = mainStackLayout;

            // Set the content of the page to the scrollView
            Content = scrollView;
        }
    }
}
