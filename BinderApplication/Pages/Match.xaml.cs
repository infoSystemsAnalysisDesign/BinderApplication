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

                // Image links
                if (volumeInfo.imageLinks != null)
                {
                    displayText.AppendLine($"Small Thumbnail: {volumeInfo.imageLinks.smallThumbnail}");
                    displayText.AppendLine($"Thumbnail: {volumeInfo.imageLinks.thumbnail}");
                }

                // Access Info
                if (firstBookItem.accessInfo != null)
                {
                    displayText.AppendLine($"Country: {firstBookItem.accessInfo.country}");
                    displayText.AppendLine($"Viewability: {firstBookItem.accessInfo.viewability}");
                    displayText.AppendLine($"Embeddable: {firstBookItem.accessInfo.embeddable}");
                    displayText.AppendLine($"Public Domain: {firstBookItem.accessInfo.publicDomain}");
                    displayText.AppendLine($"Text to Speech Permission: {firstBookItem.accessInfo.textToSpeechPermission}");
                    displayText.AppendLine($"Web Reader Link: {firstBookItem.accessInfo.webReaderLink}");
                    displayText.AppendLine($"Access View Status: {firstBookItem.accessInfo.accessViewStatus}");
                    displayText.AppendLine($"Quote Sharing Allowed: {firstBookItem.accessInfo.quoteSharingAllowed}");
                }

                // Sale Info
                if (firstBookItem.saleInfo != null)
                {
                    displayText.AppendLine($"Sale Country: {firstBookItem.saleInfo.country}");
                    displayText.AppendLine($"Saleability: {firstBookItem.saleInfo.saleability}");
                    displayText.AppendLine($"Is Ebook: {firstBookItem.saleInfo.isEbook}");
                }

                var label = new Label
                {
                    Text = displayText.ToString(),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };

                Content = new StackLayout
                {
                    Children = { label }
                };
            }
        }

    }
}
