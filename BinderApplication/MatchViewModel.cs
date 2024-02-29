using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication
{
    // ViewModel class
    public class MatchViewModel
    {
        private readonly API api;

        public ObservableCollection<Book.BookItem> BookItems { get; set; } = new ObservableCollection<Book.BookItem>();

        public MatchViewModel(API api)
        {
            this.api = api;
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                List<Book.BookItem> results = await api.GetResultsFromAPI("fiction");
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
