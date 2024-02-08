using Microsoft.Maui.Controls;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        private API api;

        public Match()
        {
            InitializeComponent();
            api = new API();
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                Book.BookVolume result = await api.GetResultFromAPI();
                responseLabel.Text = "TEST\n\n";

                DisplayBookInfo(result);
            }
            catch (Exception ex)
            {
                responseLabel.Text = $"Exception: {ex.Message}";
            }
        }

        private void DisplayBookInfo(Book.BookVolume bookVolume)
        {
            if (bookVolume != null && bookVolume.items.Count > 0)
            {
                var firstBookItem = bookVolume.items[0];
                var volumeInfo = firstBookItem.volumeInfo;

                // Build a readable string with book information
                StringBuilder displayText = new StringBuilder();
                displayText.AppendLine($"Title: {volumeInfo.title}");
                displayText.AppendLine($"Authors: {string.Join(", ", volumeInfo.authors)}");
                displayText.AppendLine($"Publisher: {volumeInfo.publisher}");
                displayText.AppendLine($"Published Date: {volumeInfo.publishedDate}");
                displayText.AppendLine($"Description: {volumeInfo.description}");
                displayText.AppendLine($"Page Count: {volumeInfo.pageCount}");
                displayText.AppendLine($"Print Type: {volumeInfo.printType}");
                displayText.AppendLine($"Categories: {string.Join(", ", volumeInfo.categories)}");
                displayText.AppendLine($"Average Rating: {volumeInfo.averageRating}");
                displayText.AppendLine($"Ratings Count: {volumeInfo.ratingsCount}");
                displayText.AppendLine($"Maturity Rating: {volumeInfo.maturityRating}");
                displayText.AppendLine($"Language: {volumeInfo.language}");
                displayText.AppendLine($"Preview Link: {volumeInfo.previewLink}");
                displayText.AppendLine($"Info Link: {volumeInfo.infoLink}");

                // Additional properties
                foreach (var identifier in volumeInfo.industryIdentifiers)
                {
                    displayText.AppendLine($"Identifier Type: {identifier.type}");
                    displayText.AppendLine($"Identifier: {identifier.identifier}");
                }

                // Create a grid for label and image
                var grid = new Grid();

                // Add label to the grid
                var label = new Label
                {
                    Text = displayText.ToString(),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start
                };
                grid.Children.Add(label);

                // Image links
                if (volumeInfo.imageLinks != null)
                {
                    displayText.AppendLine($"Small Thumbnail: {volumeInfo.imageLinks.smallThumbnail}");
                    displayText.AppendLine($"Thumbnail: {volumeInfo.imageLinks.thumbnail}");

                    Image smallThumbnail = new Image
                    {
                        Source = volumeInfo.imageLinks.smallThumbnail,
                        WidthRequest = 100,
                        HeightRequest = 150
                    };

                    grid.Children.Add(smallThumbnail);
                    Grid.SetRow(smallThumbnail, 1); // Set the row for the image
                }

                // Define rows in the grid
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Auto-sized row for the label
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Auto-sized row for the image, if added

                Content = grid;
            }
        }
    }
}
