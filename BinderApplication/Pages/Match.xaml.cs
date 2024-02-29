using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        private API api;
        private MatchViewModel viewModel;

        public Match()
        {
            InitializeComponent();

            //api = new API();
            //viewModel = new MatchViewModel(api);
            //BindingContext = viewModel;


        }

        private async Task LoadData()
        {
            try
            {
                List<Book.BookItem> results = await api.GetResultsFromAPI("fiction");
              //  DisplayBookInfo(results);
            }
            catch (Exception ex)
            {
              //  responseLabel.Text = $"Exception: {ex.Message}";
            }
        }

        private void DisplayBookInfo(List<Book.BookItem> bookItems)
        {
            var stackLayout = new StackLayout();

            foreach (var bookItem in bookItems)
            {
                var volumeInfo = bookItem.volumeInfo;

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

                foreach (var identifier in volumeInfo.industryIdentifiers)
                {
                    displayText.AppendLine($"Identifier Type: {identifier.type}");
                    displayText.AppendLine($"Identifier: {identifier.identifier}");
                }

                var grid = new Grid();

                if (volumeInfo.imageLinks != null)
                {
                    string smallThumbURL = volumeInfo.imageLinks.smallThumbnail;

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
                BindingContext = this;
            }

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }

    }
  
}
