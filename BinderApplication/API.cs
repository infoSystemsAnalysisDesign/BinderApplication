
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinderApplication
{
    public class API
    {
        private string resultFromAPI;

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
                        resultFromAPI = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        resultFromAPI = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                resultFromAPI = $"Exception: {ex.Message}";
            }
        }

        public async Task<string> GetResultFromAPI()
        {
            await LoadData();
            WriteToFile(resultFromAPI); //Writes result of the API call to he document folder (hopefully) of your computer. For testing
            return resultFromAPI;
        }

        private static void WriteToFile(string textToBeWrittenToFile)
        {
            // Create a string array with the lines of text
            string[] lines = { textToBeWrittenToFile };

            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "OFILE.txt".
            File.WriteAllLines(Path.Combine(docPath, "OFILE.txt"), lines);
        }
    }
}