using BinderApplication.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BinderApplication.Pages
{
    public partial class UserInfo : ContentPage
    {
        public UserInfo()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#D3D3D3"); // Set background color to light grey
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateDisplay();
        }

        private async void UpdateDisplay()
        {
            var dbConnection = DatabaseConnection.Instance;
            var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase("Binder");
            var users = database.GetCollection<BsonDocument>("Login");

            var dbLogin = DatabaseLogin.Instance;
            string storedEmail = dbLogin.GetEmail();

            StackLayout mainStackLayout = new StackLayout();


            var filter = Builders<BsonDocument>.Filter.Eq("Email", storedEmail);

            try
            {
                var userInfo = await users.Find(filter).FirstOrDefaultAsync();

                if (userInfo != null)
                {
                    var nameHeader = new Label
                    {

                        Text = "Name:",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#808080"), // Set text color to grey
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold  };

                    var nameLabel = new Label
                    {
                        BackgroundColor = Color.FromHex("#C0C0C0"), // Set background color to silver
                        Text = $"{userInfo["Name"].AsString}",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#000000"), // Set text color to black
                        FontSize = 22,
                        Padding = new Thickness(5),
                        HeightRequest = 40,
                        WidthRequest=350
                    };


                    var emailHeader = new Label
                    {

                        Text = "Email:",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#808080"), // Set text color to grey
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold
                    };
                    var emailLabel = new Label
                    {
                        BackgroundColor = Color.FromHex("#C0C0C0"), // Set background color to silver
                        Text = $"{userInfo["Email"].AsString}",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#000000"), // Set text color to black
                        FontSize = 22,
                        Padding = new Thickness(5),
                        HeightRequest = 40,
                        WidthRequest = 350
                    };



                    var phoneNumberHeader = new Label
                    {

                        Text = "Phone Number:",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#808080"), // Set text color to grey
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold
                    };
                    var phoneNumberLabel = new Label
                    {
                        BackgroundColor = Color.FromHex("#C0C0C0"), // Set background color to silver
                        Text = $"{userInfo["Phone Number"].AsString}",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#000000"), // Set text color to black
                        FontSize = 22,
                        Padding = new Thickness(5),
                        HeightRequest = 40,
                        WidthRequest = 350
                    };


                    var passwordHeader = new Label
                    {

                        Text = "Password:",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#808080"), // Set text color to grey
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold
                    };
                    var passwordLabel = new Label
                    {
                        BackgroundColor = Color.FromHex("#C0C0C0"), // Set background color to silver
                        Text = $"{userInfo["Password"].AsString}",
                        Margin = new Thickness(10),
                        TextColor = Color.FromHex("#000000"), // Set text color to black
                        FontSize = 22,
                        Padding = new Thickness(5),
                        HeightRequest = 40,
                        WidthRequest = 350
                    };
      
                    var backButton = new Button 
                    { Text = "Sign Out", Margin = new Thickness(10), 
                        BackgroundColor = Color.FromHex("#696969"), 
                        TextColor = Color.FromHex("#ffffff"), 
                        FontSize = 22, // Set button color to dim grey
                    };
                    backButton.Clicked += async (s, e) =>
                    {
                        // Reset the main page of your application to SignInPage
                        Application.Current.MainPage = new NavigationPage(new SignInPage());
                    };




                    // Add the button to the stack layout
                    mainStackLayout.Children.Add(backButton);

                    // Add the labels to the stack layout
                    mainStackLayout.Children.Add(nameHeader);
                    mainStackLayout.Children.Add(nameLabel);
                    mainStackLayout.Children.Add(emailHeader);
                    mainStackLayout.Children.Add(emailLabel);
                    mainStackLayout.Children.Add(phoneNumberHeader);
                    mainStackLayout.Children.Add(phoneNumberLabel);
                   // mainStackLayout.Children.Add(passwordHeader);
                   // mainStackLayout.Children.Add(passwordLabel);
                }
                else
                {
                    // No entries found message
                    var noEntriesLabel = new Label { Text = "No user info found.", Margin = new Thickness(10), TextColor = Color.FromHex("#000000") };
                    mainStackLayout.Children.Add(noEntriesLabel);
                }
            }
            catch (Exception ex)
            {
                // Display error message if there's an exception
                var errorLabel = new Label { Text = $"Error retrieving user info: {ex.Message}", Margin = new Thickness(10), TextColor = Color.FromHex("#696969") }; // Set error message color to dim grey
                mainStackLayout.Children.Add(errorLabel);
            }
            // Create a ScrollView and add the stackLayout to it
            var scrollView = new ScrollView();
            scrollView.Content = mainStackLayout;

            // Set the content of the page to the scrollView
            Content = scrollView;
        }
    }
}


