namespace BinderApplication.Pages;
using Microsoft.Maui.Controls; 
public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}
    private void OnUserInfoButtonClicked(object sender, EventArgs e)
    {
        // Use the Navigation service to navigate to UserInfo.xaml
        Navigation.PushAsync(new UserInfo());
    }
    private void OnLikedButtonClicked(object sender, EventArgs e)
    {
        // Use the Navigation service to navigate to UserInfo.xaml
        Navigation.PushAsync(new Liked());
    }
    private void OnHistoryButtonClicked(object sender, EventArgs e)
    {
        // Use the Navigation service to navigate to UserInfo.xaml
        Navigation.PushAsync(new History());
    }
    private void OnGenreButtonClicked(object sender, EventArgs e)
    {
        // Use the Navigation service to navigate to UserInfo.xaml
        Navigation.PushAsync(new Genres());
    }
}