
using BinderApplication.Database;
using MongoDB.Driver.Core.Authentication;


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
        dbLogin.SaveLogin(name.Text, email.Text, phoneNumber.Text, password.Text);
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