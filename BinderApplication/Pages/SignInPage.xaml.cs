using MongoDB.Bson;
using MongoDB.Driver;
using BinderApplication.Database;
using BinderApplication.Services;

namespace BinderApplication;

public partial class SignInPage : ContentPage
{
    public SignInPage()
	{
        InitializeComponent();
	}
    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var dbConnection = DatabaseConnection.Instance;
            var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase("Binder");

            // Get a reference to the Users collection
            var usersCollection = database.GetCollection<BsonDocument>("Login");

            // Validate user inputs
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                await DisplayAlert("Login Failed", "Please Enter Login Information", "OK");
                return;
            }


            // Check if there is a user with the provided email and password
            var filter = Builders<BsonDocument>.Filter.Eq("Email", email.Text) & Builders<BsonDocument>.Filter.Eq("Password", password.Text);
            var user = await usersCollection.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                //Store email and password into Database instance
                var dbLogin = DatabaseLogin.Instance;
                dbLogin.StoreLogin(email.Text, password.Text);

                // User found, navigate to MainPage
                App.Current.MainPage = new MainPage();

            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            // Handle or log the exception accordingly
        }
    }


    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, TappedEventArgs e)
    {
        //await Shell.Current.GoToAsync("//SignUp");
        await Navigation.PushAsync(new SignUpPage());
    }
}