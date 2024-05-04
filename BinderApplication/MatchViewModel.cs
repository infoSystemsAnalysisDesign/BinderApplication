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

                var dbGenre = DatabaseGenre.Instance;
                var genreList = dbGenre.GetTrueGenresAsList();

                List<BookModel> results = dbBook.RetrieveBooksFromDatabase(genreList);
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
                            if (volInfo.ImageLinks.SmallThumbnail.Equals("https://N/A"))
                            {
                                volInfo.ImageLinks.SmallThumbnail = "https://cdn.vectorstock.com/i/1000v/88/26/no-image-available-icon-flat-vector-25898826.jpg";
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
                            if (volInfo.ImageLinks.Thumbnail.Equals("https://N/A"))
                            {
                                volInfo.ImageLinks.Thumbnail = "https://cdn.vectorstock.com/i/1000v/88/26/no-image-available-icon-flat-vector-25898826.jpg";
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
