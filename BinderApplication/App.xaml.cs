using BinderApplication.Database;

namespace BinderApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseConnection databaseConnection = new DatabaseConnection();

        //    MainPage = new AppShell();
        }
    }
}