using Microsoft.Maui.Platform;
using BinderApplication.Handlers;
using BinderApplication.Database;

namespace BinderApplication;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        //var databaseConnection = DatabaseConnection.Instance;   //technically not needed unless we ever have sign-in permanence
        MainPage = new NavigationPage(new SignInPage());

        DatabaseGenre dbGenre = new DatabaseGenre();
       
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
    }

    //     protected override void OnStart()
    //{
    //    // Handle when your app starts

    //    // Set the initial main page to SignInPage
    //    MainPage = new NavigationPage(new SignInPage());
    //}

}