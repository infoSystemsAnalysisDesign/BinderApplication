using System.Net.Http;
using System.Threading.Tasks;

namespace BinderApplication.Services
{
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;

        public GoogleBooksService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetTopFictionBooksAsync()
        {
            string apiUrl = "https://www.googleapis.com/books/v1/volumes?q=subject:fiction&maxResults=10";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
            }
        }
    }
}
