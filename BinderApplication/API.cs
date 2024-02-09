using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinderApplication
{
    public class API
    {
        private Book.BookVolume resultFromAPI;

        private async Task LoadData()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = "https://www.googleapis.com/books/v1/volumes?q=subject:fiction&maxResults=1";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        resultFromAPI = JsonSerializer.Deserialize<Book.BookVolume>(jsonResult);
                    }
                    else
                    {
                        resultFromAPI = null; //MAKE ERROR HANDLING
                    }
                }
            }
            catch (Exception ex)
            {
                resultFromAPI = null;   //MAKE ERROR HANDLING
            }
        }

        public async Task<Book.BookVolume> GetResultFromAPI()
        {
            await LoadData();
            return resultFromAPI;
        }
    }
}
