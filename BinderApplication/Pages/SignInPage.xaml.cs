using MongoDB.Bson;
using MongoDB.Driver;
using BinderApplication.Database;
using BinderApplication.Services;

namespace BinderApplication;

public partial class SignInPage : ContentPage
{
    public SignInPage()
	{
		
	}
	private async void OnSignInButtonClicked(object sender, EventArgs e)
	{


        //add stuff to make sure has account 
        // Get a reference to the Users collection
        var dbConnection = DatabaseConnection.Instance;
        var client = new MongoClient("mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority");
        var database = client.GetDatabase("Binder");

        // Get a reference to the Users collection
        var usersCollection = database.GetCollection<BsonDocument>("Login");

        // Check if there is a user with the provided email and password
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email.Text) & Builders<BsonDocument>.Filter.Eq("Password", password.Text);
        var user = await usersCollection.Find(filter).FirstOrDefaultAsync();

        if (user != null)
        {
            // User found, navigate to MainPage
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            // User not found or incorrect credentials
            // Handle the case where login fails, show an alert or display a message to the user
        }
        //  await Navigation.PushAsync(new MainPage());
    }

    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, TappedEventArgs e)
    {
        //await Shell.Current.GoToAsync("//SignUp");
        await Navigation.PushAsync(new SignUpPage());
    }
}