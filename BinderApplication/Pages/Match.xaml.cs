using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        public List<Book> Books { get; set; }

        public Match()
        {
            InitializeComponent();

            // Manually add books to the list; event replace to link with Google Books
            Books = new List<Book>
            {
                new Book { Name = "The Great Gatsby", Author = "F. Scott Fitzgerald", Bio = "A novel about the American Dream." },
                new Book { Name = "To Kill a Mockingbird", Author = "Harper Lee", Bio = "A classic novel exploring racial injustice." },
                new Book { Name = "1984", Author = "George Orwell", Bio = "A dystopian novel about totalitarianism." },
              
            };

            BindingContext = this; // Set the BindingContext to the current instance of the page
        }
    }

    // Book class
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Bio { get; set; }
    }
}

