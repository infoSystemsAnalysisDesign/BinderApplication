using Microsoft.Maui.Controls;
using System;
using BinderApplication.Database;
using Microsoft.Maui.Controls.Shapes;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        private readonly DatabaseConnection databaseConnection = DatabaseConnection.Instance;
        private MatchViewModel viewModel;
        private CarouselView carouselView;
        private readonly Binder _binderPage;
        public Match()
        {
            // Background Image

            var backgroundImage = new Image
            {
                Source = GetBackgroundImageForDayOfWeek(DateTime.Now.DayOfWeek),
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            string GetBackgroundImageForDayOfWeek(DayOfWeek dayOfWeek)
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        return "weekday.jpg";
                    case DayOfWeek.Tuesday:
                        return "weekday.jpg";
                    case DayOfWeek.Wednesday:
                        return "weekday.jpg";
                    case DayOfWeek.Thursday:
                        return "weekday.jpg";
                    case DayOfWeek.Friday:
                        return "weekday.jpg";
                    case DayOfWeek.Saturday:
                        return "weekend.jpg";
                    case DayOfWeek.Sunday:
                        return "weekend.jpg";
                    default:
                        return null; // No specific background for other days
                }
            }

            // Create a new Grid
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            // Add the background image to the Grid
            grid.Children.Add(backgroundImage);

            // Set the Grid as the page's content
            Content = grid;

            InitializeComponent();
            viewModel = new MatchViewModel(databaseConnection);
            BindingContext = viewModel;
            SetupUI();
        }
        private void SetupUI()
        {
            carouselView = new CarouselView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var cardLayout = new StackLayout
                    {
                        Padding = new Thickness(10),
                        Margin = new Thickness(0, 10, 0, 10) // Add margin to create space between cards

                    };

                    // Create Front Side
                    var frontLayout = new StackLayout
                    {
                        BackgroundColor = Color.FromHex("#ffffff"),
                        Padding = new Thickness(10),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start, // Changed to Start
                        HeightRequest = 450
                    };

                    var frontImage = new Image
                    {
                        WidthRequest = 200,
                        HeightRequest = 300,
                        Aspect = Aspect.AspectFit,
                        VerticalOptions = LayoutOptions.Start, // Align the image to the top
                    };
                    frontImage.SetBinding(Image.SourceProperty, "VolumeInfo.ImageLinks.Thumbnail");

                    ////Padding for book covers
                    //var bookCoverBorder = new Border
                    //{
                    //    Stroke = Brush.Black,
                    //    StrokeThickness = 5,
                    //    StrokeShape = new RoundRectangle(),
                    //    Padding = new Thickness(8, 8),
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    Content = frontImage
                    //};
                    //this.Content = bookCoverBorder;


                    // Add tap gesture recognizer to the image
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) =>
                    {
                        // Get the current book
                        var currentBook = (BookModel)carouselView.CurrentItem;
                        // Show the full description in an alert
                        await DisplayAlert(currentBook.VolumeInfo.Title, currentBook.VolumeInfo.Description, "OK");
                    };
                    frontImage.GestureRecognizers.Add(tapGestureRecognizer);

                    var frontTitle = new Label
                    {
                        WidthRequest = 300,
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold
                    };
                    frontTitle.SetBinding(Label.TextProperty, "VolumeInfo.Title");

                    var frontAuthors = new Label
                    {
                        WidthRequest = 300,
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = 16,
                    };
                    frontAuthors.SetBinding(Label.TextProperty, "VolumeInfo.Authors[0]"); // Assuming authors is a list

                    frontLayout.Children.Add(frontImage);
                    frontLayout.Children.Add(frontTitle);
                    frontLayout.Children.Add(frontAuthors);

                    // Create Back Side
                    var backLayout = new StackLayout
                    {
                        BackgroundColor = Color.FromHex("#ffffff"),
                        Padding = new Thickness(10),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false // Initially hidden
                    };

                    var backDescription = new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = 16,
                        LineBreakMode = LineBreakMode.WordWrap,
                        VerticalOptions = LayoutOptions.StartAndExpand // Allow the description to occupy available space
                    };
                    backDescription.SetBinding(Label.TextProperty, "VolumeInfo.Description");

                    backLayout.Children.Add(backDescription);

                    cardLayout.Children.Add(frontLayout);
                    cardLayout.Children.Add(backLayout);

                    // Create Buttons
                    var buttonLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.End,
                        Padding = new Thickness(10, 5, 10, 10) // Add padding to the button layout
                    };

                    var likeButton = new Button
                    {
                        BackgroundColor = Color.FromHex("#147efb"),
                        Text = "u up?",
                        TextColor = Color.FromHex("#FFFFFF"),
                        BorderColor = Color.FromHex("#147efb"),
                        BorderWidth = 1, // Add this line
                        Margin = new Thickness(0, 0, -10, 0), // Add margin to the right to create space between buttons
                        WidthRequest = 150, // Set width request to make the buttons bigger
                        HeightRequest = 50, // Set height request to make the buttons bigger
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End
                    };
                    likeButton.Clicked += LikedButton;

                    var hateButton = new Button
                    {
                        BackgroundColor = Color.FromHex("#53d769"),
                        Text = "#ghosted",
                        TextColor = Color.FromHex("#FFFFFF"),
                        BorderColor = Color.FromHex("#53d769"),
                        BorderWidth = 1, // Add this line
                        Margin = new Thickness(31, 0, 0, 0), // Add margin to the right to create space between buttons
                        WidthRequest = 150, // Set width request to make the buttons bigger
                        HeightRequest = 50, // Set height request to make the buttons bigger
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.End
                    };
                    hateButton.Clicked += HateButton;

                    buttonLayout.Children.Add(likeButton);
                    buttonLayout.Children.Add(hateButton);

                    cardLayout.Children.Add(buttonLayout);

                    return cardLayout;
                })
            };

            carouselView.SetBinding(CarouselView.ItemsSourceProperty, "BookItems");
            ((Grid)Content).Children.Add(carouselView);
        }



        private async void LikedButton(object sender, EventArgs e)
        {
            try
            {
                var dbLogin = DatabaseLogin.Instance;
                var currentBook = (BookModel)carouselView.CurrentItem;
                currentBook.Email = dbLogin.GetEmail();
                await databaseConnection.SaveCarouselLiked(currentBook);
                await DisplayAlert("HubbaHubba", "You liked me!", "OK ;)");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void HateButton(object sender, EventArgs e)
        {
            try
            {
                var dbLogin = DatabaseLogin.Instance;
                var currentBook = (BookModel)carouselView.CurrentItem;
                currentBook.Email = dbLogin.GetEmail();
                await databaseConnection.SaveCarouselHate(currentBook);
                await DisplayAlert("Nooooo!", "You hate me?", "OK :(");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}

