using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinderApplication
{
    public class API
    {
        private List<Book.BookItem> resultsFromAPI;

        private async Task LoadData(string genre)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = $"https://www.googleapis.com/books/v1/volumes?q=subject:{genre}&maxResults=10";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var deserializedResult = JsonSerializer.Deserialize<Book.BookVolume>(jsonResult);
                        resultsFromAPI = deserializedResult.items;
                    }
                    else
                    {
                        resultsFromAPI = null; // Error handling
                    }
                }
            }
            catch (Exception ex)
            {
                resultsFromAPI = null; // Error handling
            }
        }

        public async Task<List<Book.BookItem>> GetResultsFromAPI(string genre)
        {
            await LoadData(genre);
            return resultsFromAPI;
        }
    }
}
