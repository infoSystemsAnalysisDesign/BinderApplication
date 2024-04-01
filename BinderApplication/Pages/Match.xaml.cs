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

            Content = carouselView;
        }
    }
}