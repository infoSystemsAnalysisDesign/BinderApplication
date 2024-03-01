using BinderApplication.Services;
namespace BinderApplication.Pages;


public partial class Loading : ContentPage
{
    private readonly AuthService _authService;

    public Loading(AuthService authService)
    {
        _authService = authService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.IsAuthenticatedAsync())
        {
            //user is logged in
            //redirect to main page
            await Shell.Current.GoToAsync($"//{nameof(Match)}");
        }
        else
        {
            //user is not logged in
            //redirect to sign in page
            await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
        }
    }
}

