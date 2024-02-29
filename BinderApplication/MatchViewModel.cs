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
                List<BookModel> results = databaseConnection.RetrieveBooksFromDatabase();
                foreach (var bookItem in results)
                {
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
