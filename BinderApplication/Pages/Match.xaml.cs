using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        public Match()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = "https://www.googleapis.com/books/v1/volumes?q=subject:fiction&maxResults=10";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        responseLabel.Text = responseBody;
                    }
                    else
                    {
                        responseLabel.Text = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                responseLabel.Text = $"Exception: {ex.Message}";
            }
        }
    }
}

