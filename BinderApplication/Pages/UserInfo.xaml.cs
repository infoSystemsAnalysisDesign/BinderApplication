using BinderApplication.Database;
using MongoDB.Bson;
using MongoDB.Driver;
namespace BinderApplication.Pages;

public partial class UserInfo : ContentPage
{
	public UserInfo()
	{
		InitializeComponent();
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
                var nameLabel = new Label { Text = $"Name: {userInfo["Name"].AsString}" };
                var emailLabel = new Label { Text = $"Email: {userInfo["Email"].AsString}" };
                var phoneNumberLabel = new Label { Text = $"Phone Number: {userInfo["Phone Number"].AsString}" };
                var passwordLabel = new Label { Text = $"Password: {userInfo["Password"].AsString}" };
                var backButton = new Button { Text = "Sign Out" };
                backButton.Clicked += (s, e) => Navigation.PushAsync(new SignInPage());

                // Add the button to the stack layout
                mainStackLayout.Children.Add(backButton);

                // Add the labels to the stack layout
                mainStackLayout.Children.Add(nameLabel);
                mainStackLayout.Children.Add(emailLabel);
                mainStackLayout.Children.Add(phoneNumberLabel);
                mainStackLayout.Children.Add(passwordLabel);
            }
            else
            {
                // No entries found message
                var noEntriesLabel = new Label { Text = "No user info found." };
                mainStackLayout.Children.Add(noEntriesLabel);
            }
        }

        catch (Exception ex)
        {
            // Display error message if there's an exception
            var errorLabel = new Label { Text = $"Error retrieving user info: {ex.Message}" };
            mainStackLayout.Children.Add(errorLabel);
        }
        // Create a ScrollView and add the stackLayout to it
        var scrollView = new ScrollView();
        scrollView.Content = mainStackLayout;

        // Set the content of the page to the scrollView
        Content = scrollView;
    }
}
