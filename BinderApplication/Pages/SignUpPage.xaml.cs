
using BinderApplication.Database;
using MongoDB.Driver.Core.Authentication;


namespace BinderApplication;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
    }
    private async void TapGestureRecognizer_Tapped_For_SignIN(object sender, TappedEventArgs e)
    {
      

        var dbConnection = DatabaseConnection.Instance;
        dbConnection.SaveLogin(name.Text, email.Text, phoneNumber.Text, password.Text);
        
        await Shell.Current.GoToAsync("//SignIn");
    }
}