
using BinderApplication.Database;
using MongoDB.Driver.Core.Authentication;
using System;
using MongoDB.Bson;
using MongoDB.Driver;


namespace BinderApplication;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
    }
    private async void OnClickedSignUp(object sender, EventArgs e)
    {
        var dbLogin = DatabaseLogin.Instance;
        var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
        var database = client.GetDatabase("Binder");

        // Get a reference to the Users collection
        var usersCollection = database.GetCollection<BsonDocument>("Login");

        // Validate user inputs
        if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(phoneNumber.Text))
        {
            await DisplayAlert("Sign Up Failed", "Please Enter Your Information", "OK");
            return;
        }
        if   (  phoneNumber.Text.Length != 10 || !phoneNumber.Text.All(char.IsDigit))
        {
            await DisplayAlert("Sign Up Failed", "Please enter a valid 10-digit phone number.", "OK");
            return;
        }
        if (!email.Text.Contains("@") || !email.Text.Contains("."))
        {
            await DisplayAlert("Sign Up Failed", "Please enter a valid email adress.", "OK");
            return;
        }

        // Check if the email already exists in the database
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email.Text);
        var existingUser = await usersCollection.Find(filter).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            await DisplayAlert("Email Invalid", "The email is already in use. Please use a different email.", "OK");
            return;
        }

        // Save the new user's information
        dbLogin.SaveLogin(name.Text, email.Text, phoneNumber.Text, password.Text);
        await DisplayAlert("Sign Up Successful", "Your account has been created successfully.", "OK");

        // Navigate to the sign-in page
        await Navigation.PushAsync(new SignInPage());
    }

    private async void TapGestureRecognizer_Tapped_For_SignIN(object sender, TappedEventArgs e)
    {
      

      //  var dbConnection = DatabaseConnection.Instance;
      //  dbConnection.SaveLogin(name.Text, email.Text, phoneNumber.Text, password.Text);

        await Navigation.PushAsync(new SignInPage());
        //await Shell.Current.GoToAsync("//SignIn");
    }
}