using BinderApplication.Services;
using BinderApplication.Pages;
using Microsoft.Maui.ApplicationModel.Communication;
using BinderApplication.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BinderApplication.Pages;

public partial class ResetPassword : ContentPage
{
    DatabaseConnection dbConnection = DatabaseConnection.Instance;
    DatabaseLogin dbLogin = DatabaseLogin.Instance;

    public ResetPassword()
    {
        InitializeComponent();
    }

    private async void OnResetButton_Clicked(object sender, EventArgs e)
    {
        bool validity;
        validity = await CheckValidityOfFields();

        if (validity)
            await Navigation.PushAsync(new SignInPage());
    }

    private async void CancelResetButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage());
    }

    private async Task<bool> CheckValidityOfFields()
    {
        //Validate user inputs
        if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(phonenumber.Text)
            || string.IsNullOrEmpty(newPassword.Text) || string.IsNullOrEmpty(confirmPassword.Text))
        {
            await DisplayAlert("Oops!", "Please fill out all fields", "OK");
            return false;
        }

        //Database Connection
        var database = dbConnection.GetDatabase();
        var usersCollection = database.GetCollection<BsonDocument>("Login");

        //Check if there is a user with the provided email and phone number
        var filter = Builders<BsonDocument>.Filter.Eq("Email", email.Text) & Builders<BsonDocument>.Filter.Eq("Phone Number", phonenumber.Text);
        var user = await usersCollection.Find(filter).FirstOrDefaultAsync();

        if (user != null)
        {
            var update = Builders<BsonDocument>.Update.Set("Password", newPassword.Text);
            await usersCollection.UpdateOneAsync(filter, update);
            await DisplayAlert("Password Change SUCCESS", "Password has been changed successfully.", "OK");
            return true;
        }
        else
        {
            await DisplayAlert("Password Change FAILED", "Invalid email or phone number.", "OK");
            return false;
        }
    }
}