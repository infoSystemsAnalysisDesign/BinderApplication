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

                    return cardLayout;
                })
            };

            carouselView.SetBinding(CarouselView.ItemsSourceProperty, "BookItems");

            Content = carouselView;
        }


        private async void LikedButton(object sender, EventArgs e)
        {
            try
            {
                var dbLogin = DatabaseLogin.Instance;
                var currentBook = (BookModel)carouselView.CurrentItem;
                currentBook.Email = dbLogin.GetEmail();
                await databaseConnection.SaveCarouselLiked(currentBook);
                await DisplayAlert("Success", "Carousel data saved successfully.", "OK");
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
                await DisplayAlert("Success", "Carousel data saved successfully.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
//C# to work with the saving to table, this is with the commented out part in DB Connection and actually saves the entire carosuel to your DB
//private async void LikedButton(object sender, EventArgs e)
//{
//    try
//    {
//        // Assuming DatabaseBook class has a method to save carousel information
//        await databaseConnection.SaveCarouselDataAsync(viewModel.BookItems.ToList());
//        await DisplayAlert("Success", "Carousel data saved successfully.", "OK");
//    }
//    catch (Exception ex)
//    {
//        await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
//    }
//}
//Previous Carosuel Code **Can Delete Later**
//{
//    public partial class Match : ContentPage
//    {
//        private readonly DatabaseConnection databaseConnection = DatabaseConnection.Instance;
//        private MatchViewModel viewModel;
//        private CarouselView carouselView;

//        public Match()
//        {
//            InitializeComponent();
//            viewModel = new MatchViewModel(databaseConnection);
//            BindingContext = viewModel;

//            SetupUI();
//        }

//        private void SetupUI()
//        {
//            carouselView = new CarouselView
//            {
//                ItemTemplate = new DataTemplate(() =>
//                {
//                    var cardLayout = new StackLayout
//                    {
//                        Padding = new Thickness(10)
//                    };

//                    // Create Front Side
//                    var frontLayout = new StackLayout
//                    {
//                        HorizontalOptions = LayoutOptions.Center,
//                        VerticalOptions = LayoutOptions.Center
//                    };

//                    var frontImage = new Image
//                    {
//                        WidthRequest = 200,
//                        HeightRequest = 300,
//                        Aspect = Aspect.AspectFit // or Aspect.AspectFill depending on your preference
//                    };
//                    frontImage.SetBinding(Image.SourceProperty, "VolumeInfo.ImageLinks.Thumbnail");

//                    frontLayout.Children.Add(frontImage);

//                    // Create Back Side
//                    var backLayout = new StackLayout
//                    {
//                        HorizontalOptions = LayoutOptions.Center,
//                        VerticalOptions = LayoutOptions.Center,
//                        IsVisible = false // Initially hidden
//                    };

//                    var backLabel = new Label
//                    {
//                        HorizontalOptions = LayoutOptions.Center,
//                        VerticalOptions = LayoutOptions.Center,
//                        HorizontalTextAlignment = TextAlignment.Center,
//                        FontSize = 20
//                    };
//                    backLabel.SetBinding(Label.TextProperty, "VolumeInfo.Title");

//                    backLayout.Children.Add(backLabel);

//                    cardLayout.Children.Add(frontLayout);
//                    cardLayout.Children.Add(backLayout);

//                    var tapGestureRecognizer = new TapGestureRecognizer();
//                    tapGestureRecognizer.Tapped += (s, e) =>
//                    {
//                        // Toggle between front and back sides on tap
//                        frontLayout.IsVisible = !frontLayout.IsVisible;
//                        backLayout.IsVisible = !backLayout.IsVisible;
//                    };


//                    cardLayout.GestureRecognizers.Add(tapGestureRecognizer);

//                    var swipeGestureRecognizer = new SwipeGestureRecognizer();
//                    {

//                    }

//                    return cardLayout;
//                })
//            };

//            carouselView.SetBinding(CarouselView.ItemsSourceProperty, "BookItems");

//            Content = carouselView;
//        }
//    }
//}