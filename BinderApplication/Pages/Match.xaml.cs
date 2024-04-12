using Microsoft.Maui.Controls;
using System;
using BinderApplication.Database;


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
                        return "background1.jpeg";
                    case DayOfWeek.Tuesday:
                        return "background2.jpeg";
                    case DayOfWeek.Wednesday:
                        return "background1.jpeg";
                    case DayOfWeek.Thursday:
                        return "background2.jpg";
                    case DayOfWeek.Friday:
                        return "background1.jpeg";
                    case DayOfWeek.Saturday:
                        return "background2.jpeg";
                    case DayOfWeek.Sunday:
                        return "background1.jpeg";
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
                        Padding = new Thickness(10)
                    };

                    // Create Front Side
                    var frontLayout = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var frontImage = new Image
                    {
                        WidthRequest = 200,
                        HeightRequest = 300,
                        Aspect = Aspect.AspectFit // or Aspect.AspectFill depending on your preference
                    };
                    frontImage.SetBinding(Image.SourceProperty, "VolumeInfo.ImageLinks.Thumbnail");

                    frontLayout.Children.Add(frontImage);

                    // Create Back Side
                    var backLayout = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false // Initially hidden
                    };

                    var backLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20
                    };
                    backLabel.SetBinding(Label.TextProperty, "VolumeInfo.Title");

                    backLayout.Children.Add(backLabel);

                    cardLayout.Children.Add(frontLayout);
                    cardLayout.Children.Add(backLayout);

                    // Create Buttons
                    var buttonLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.End
                    };

                    var likeButton = new Button
                    {
                        Text = "Like",
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End
                    };
                    likeButton.Clicked += LikedButton;

                    var hateButton = new Button
                    {
                        Text = "Hate",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.End
                    };
                    hateButton.Clicked += HateButton;

                    buttonLayout.Children.Add(likeButton);
                    buttonLayout.Children.Add(hateButton);

                    cardLayout.Children.Add(buttonLayout);

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        // Toggle between front and back sides on tap
                        frontLayout.IsVisible = !frontLayout.IsVisible;
                        backLayout.IsVisible = !backLayout.IsVisible;
                    };


                    cardLayout.GestureRecognizers.Add(tapGestureRecognizer);

                    return cardLayout;
                })
            };

           
           

            carouselView.SetBinding(CarouselView.ItemsSourceProperty, "BookItems");
            ((Grid)Content).Children.Add(carouselView);
            //Content = carouselView;
        }


        private async void LikedButton(object sender, EventArgs e)
        {
            try
            {
                var dbLogin = DatabaseLogin.Instance;
                var currentBook = (BookModel)carouselView.CurrentItem;
                currentBook.Email = dbLogin.GetEmail();
                await databaseConnection.SaveCarouselLiked(currentBook);
                await DisplayAlert("HubbaHubba", "You liked me!", ";)");
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
