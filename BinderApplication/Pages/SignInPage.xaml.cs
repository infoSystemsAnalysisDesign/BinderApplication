namespace BinderApplication;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}
	private async void checkLogin(object sender, EventArgs e)
	{
		
	}

    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//SignUp");
    }
}