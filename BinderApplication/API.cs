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
            WriteToFile(resultFromAPI); // Writes result of the API call to the document folder (for testing)
            return resultFromAPI;
        }

        /*
        Writes the data to a json file (you can open it in Notepad) to see how the data outputs.
        It SHOULD appear in your documents folder as "OFILE"
        */
        private static void WriteToFile(Book.BookVolume bookVolume)
        {
            if (bookVolume != null)
            {
                string jsonResult = JsonSerializer.Serialize(bookVolume);
                string[] lines = { jsonResult };
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                File.WriteAllLines(Path.Combine(docPath, "OFILE.json"), lines);
            }
        }
    }
}
