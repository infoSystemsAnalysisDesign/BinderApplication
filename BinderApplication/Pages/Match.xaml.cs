using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        public List<Book> Books { get; set; }
        private const string GoogleBooksApiUrl = "https://www.googleapis.com/books/v1/volumes?q=programming"; // Replace with your actual API endpoint

        public Match()
        {
            InitializeComponent();

            // Fetch books from the API
            FetchBooksFromApi();

            BindingContext = this; // Set the BindingContext to the current instance of the page
        }

        private async void FetchBooksFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                // Make a GET request to the Google Books API
                HttpResponseMessage response = await client.GetAsync(GoogleBooksApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var apiBooks = await JsonSerializer.DeserializeAsync<GoogleBooksApiResponse>(stream);

                        // Transform the API response into your Book objects
                        Books = new List<Book>();
                        foreach (var item in apiBooks.Items)
                        {
                            Books.Add(new Book
                            {
                                Name = item.VolumeInfo.Title,
                                Author = string.Join(", ", item.VolumeInfo.Authors),
                                Bio = item.VolumeInfo.Description
                            });
                        }
                    }
                }
                else
                {
                    // Handle the error, e.g., log it or show a message to the user
                }
            }
        }
    }

    // Classes for deserializing Google Books API response
    public class VolumeInfo
    {
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string Description { get; set; }
    }

    public class Item
    {
        public VolumeInfo VolumeInfo { get; set; }
    }

    public class GoogleBooksApiResponse
    {
        public List<Item> Items { get; set; }
    }

    // Book class
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Bio { get; set; }
    }
}
