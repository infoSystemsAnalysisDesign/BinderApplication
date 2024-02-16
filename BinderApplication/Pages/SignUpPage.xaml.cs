namespace BinderApplication;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped_For_SignIN(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//SignIn");
    }
}