using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BinderApplication.Database;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        private readonly DatabaseConnection databaseConnection = DatabaseConnection.Instance;
        private MatchViewModel viewModel;

        public Match()
        {
            //InitializeComponent();
            //LoadData();

            InitializeComponent();
            viewModel = new MatchViewModel(databaseConnection);
            BindingContext = viewModel;
        }

        private async Task LoadData()
        {
            try
            {
                // Retrieve books from the database
                DatabaseBook dbBook = new DatabaseBook();

                var books = dbBook.RetrieveBooksFromDatabase();
                DisplayBookInfo(books);
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        private void DisplayBookInfo(List<BookModel> books)
        {
            var stackLayout = new StackLayout();

            foreach (var book in books)
            {
                var volumeInfo = book.VolumeInfo;

                StringBuilder displayText = new StringBuilder();
                displayText.AppendLine($"Title: {volumeInfo.Title}");
                displayText.AppendLine($"Authors: {string.Join(", ", volumeInfo.Authors ?? new List<string>())}");
                displayText.AppendLine($"Publisher: {volumeInfo.Publisher}");
                displayText.AppendLine($"Published Date: {volumeInfo.PublishedDate}");
                displayText.AppendLine($"Description: {volumeInfo.Description}");
                displayText.AppendLine($"Page Count: {volumeInfo.PageCount}");
                displayText.AppendLine($"Print Type: {volumeInfo.PrintType}");
                displayText.AppendLine($"Categories: {string.Join(", ", volumeInfo.Categories ?? new List<string>())}");
                displayText.AppendLine($"Average Rating: {volumeInfo.AverageRating}");
                displayText.AppendLine($"Ratings Count: {volumeInfo.RatingsCount}");
                displayText.AppendLine($"Maturity Rating: {volumeInfo.MaturityRating}");
                displayText.AppendLine($"Language: {volumeInfo.Language}");
                displayText.AppendLine($"Preview Link: {volumeInfo.PreviewLink}");
                displayText.AppendLine($"Info Link: {volumeInfo.InfoLink}");

                foreach (var identifier in volumeInfo.IndustryIdentifiers ?? new List<IndustryIdentifier>())
                {
                    displayText.AppendLine($"Identifier Type: {identifier.Type}");
                    displayText.AppendLine($"Identifier: {identifier.Identifier}");
                }

                var grid = new Grid();

                if (volumeInfo.ImageLinks != null)
                {
                    string smallThumbURL = volumeInfo.ImageLinks.SmallThumbnail;

                    if (smallThumbURL.StartsWith("http://"))
                    {
                        smallThumbURL = smallThumbURL.Replace("http://", "https://");
                    }
                    else if (!smallThumbURL.StartsWith("https://"))
                    {
                        smallThumbURL = "https://" + smallThumbURL;
                    }

                    Image smallThumbnail = new Image
                    {
                        Source = smallThumbURL,
                        WidthRequest = 100,
                        HeightRequest = 150
                    };

                    grid.Children.Add(smallThumbnail);
                    Grid.SetRow(smallThumbnail, 0);
                }

                var label = new Label
                {
                    Text = displayText.ToString(),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start
                };

                grid.Children.Add(label);
                Grid.SetRow(label, 1);

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                stackLayout.Children.Add(grid);
            }

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }
}
