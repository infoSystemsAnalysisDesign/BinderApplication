using BinderApplication.Services;
using BinderApplication.Pages;
using Microsoft.Maui.ApplicationModel.Communication;
namespace BinderApplication.Pages;

public partial class ResetPassword : ContentPage
{
    public ResetPassword()
    {
        InitializeComponent();
    }

    private async void OnResetButton_Clicked(object sender, EventArgs e)
    {
        // Implements the logic for handling the 'forgot your password' action 
        await DisplayAlert("Confirm Password Reset", "Please confirm password reset", "OK");
        // Navigate to the forgot password page
        await Navigation.PushAsync(new SignInPage());
    }

    private async void CancelResetButton_Clicked(object sender, EventArgs e)
    {
        // Implements the logic for handling the 'forgot your password' action 
        await DisplayAlert("Return To Login", "Return to the Login Page?", "OK");
        // Navigate to the forgot password page
        await Navigation.PushAsync(new SignInPage());

    }
}