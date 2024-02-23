using Microsoft.Maui.Platform;
using BinderApplication.Handlers;
using BinderApplication.Database;

namespace BinderApplication;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DatabaseConnection databaseConnection = new DatabaseConnection();

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


        MainPage = new AppShell();
    }
}