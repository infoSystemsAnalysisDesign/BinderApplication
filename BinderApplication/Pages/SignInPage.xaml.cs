namespace BinderApplication;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}
	private async void OnSignInButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());
    }

    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//SignUp");
    }
}