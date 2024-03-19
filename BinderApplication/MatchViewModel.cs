using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinderApplication.Database;

namespace BinderApplication
{
    // ViewModel class
    public class MatchViewModel
    {
        private readonly DatabaseConnection databaseConnection;

        public ObservableCollection<BookModel> BookItems { get; set; } = new ObservableCollection<BookModel>();

        public MatchViewModel(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Retrieve books from the database
                DatabaseBook dbBook = new DatabaseBook();

                List<BookModel> results = dbBook.RetrieveBooksFromDatabase();
                foreach (var bookItem in results)
                {
                    // Access the VolumeInfo directly from the bookItem
                    VolumeInfo volInfo = bookItem.VolumeInfo;

                    // Check if ImageLinks is not null
                    if (volInfo?.ImageLinks != null)
                    {
                        // Manipulate SmallThumbnail link
                        if (!string.IsNullOrEmpty(volInfo.ImageLinks.SmallThumbnail))
                        {
                            if (volInfo.ImageLinks.SmallThumbnail.StartsWith("http://"))
                            {
                                volInfo.ImageLinks.SmallThumbnail = volInfo.ImageLinks.SmallThumbnail.Replace("http://", "https://");
                            }
                            else if (!volInfo.ImageLinks.SmallThumbnail.StartsWith("https://"))
                            {
                                volInfo.ImageLinks.SmallThumbnail = "https://" + volInfo.ImageLinks.SmallThumbnail;
                            }
                        }

                        // Manipulate Thumbnail link
                        if (!string.IsNullOrEmpty(volInfo.ImageLinks.Thumbnail))
                        {
                            if (volInfo.ImageLinks.Thumbnail.StartsWith("http://"))
                            {
                                volInfo.ImageLinks.Thumbnail = volInfo.ImageLinks.Thumbnail.Replace("http://", "https://");
                            }
                            else if (!volInfo.ImageLinks.Thumbnail.StartsWith("https://"))
                            {
                                volInfo.ImageLinks.Thumbnail = "https://" + volInfo.ImageLinks.Thumbnail;
                            }
                        }
                    }

                    // Add the modified bookItem to BookItems
                    BookItems.Add(bookItem);
                }

            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

    }
}
