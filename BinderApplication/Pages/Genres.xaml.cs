using BinderApplication.Database;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Linq.Expressions;
using BinderApplication.States;

namespace BinderApplication.Pages;

public partial class Genres : ContentPage
{
    //Why C# gotta be quirky and call Hashmaps dictionaries
    //Hashmap to store whether a switch is flipped on or off
    Dictionary<string, bool> stringBooleanMap;
    int switchCount = 0;
    DatabaseGenre dbGenre = DatabaseGenre.Instance;

    public Genres()
	{
		InitializeComponent();
        /*
         * Example: 
         * Drama.IsToggled = true;
         * switchCount++;
         */

        SaveButton.Clicked += OnSaveButtonClicked;

        Drama.Toggled += OnSwitchToggle;
        Essay.Toggled += OnSwitchToggle;
        Fiction.Toggled += OnSwitchToggle;
        History.Toggled += OnSwitchToggle;
        Horror.Toggled += OnSwitchToggle;
        NonFiction.Toggled += OnSwitchToggle;
        Novel.Toggled += OnSwitchToggle;
        Philosophy.Toggled += OnSwitchToggle;
        Poetry.Toggled += OnSwitchToggle;
        Politics.Toggled += OnSwitchToggle;
        Psychology.Toggled += OnSwitchToggle;
        Romance.Toggled += OnSwitchToggle;
        Science.Toggled += OnSwitchToggle;
        Spirituality.Toggled += OnSwitchToggle;
        Suspense.Toggled += OnSwitchToggle;
        Thriller.Toggled += OnSwitchToggle;


        stringBooleanMap = dbGenre.ReadGenresForAccount();
        foreach (KeyValuePair<string, bool> genre in stringBooleanMap)
        {
            if (genre.Value == true)
            {
                var switchControl = this.FindByName<Microsoft.Maui.Controls.Switch>(genre.Key);
                if (switchControl != null)
                {
                    switchControl.IsToggled = true;
                }
            }
        }
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
	{
        if (switchCount < 4)
        {
            await DisplayAlert("Oops", "Please select at least 4 genres!", "OK");
        }
        else
        {
            await DisplayAlert("Success", "Save Success", "OK");

            //Code this bad should get me put in prison
            dbGenre.UpdateGenresForAccount(
            stringBooleanMap["Drama"],
            stringBooleanMap["Essay"],
            stringBooleanMap["Fiction"],
            stringBooleanMap["History"],
            stringBooleanMap["Horror"],
            stringBooleanMap["NonFiction"],
            stringBooleanMap["Novel"],
            stringBooleanMap["Philosophy"],
            stringBooleanMap["Poetry"],
            stringBooleanMap["Politics"],
            stringBooleanMap["Psychology"],
            stringBooleanMap["Romance"],
            stringBooleanMap["Science"],
            stringBooleanMap["Spirituality"],
            stringBooleanMap["Suspense"],
            stringBooleanMap["Thriller"]
        );

            var firstTimeUserState = FirstTimeUserState.Instance;
            bool firstTimeUser = firstTimeUserState.GetFirstTimeUserState();

            if (firstTimeUser == true)
                DisplayAlert("How to use Binder:",
                    "\n* SWIPE to see new books." +
                    "\n* TAP a book cover to see its description." +
                    "\n* You can LIKE or DISLIKE books to view them later in the Profile tab." +
                    "\n* Create Reading Journals in the Journals tab." +
                    "\n* You can logout of your account in the Account menu located in the Profile tab." +
                    "\n* Most importantly, have fun!",
                    "Let's get started!");

            firstTimeUserState.DisableFirstTimeUserState();

            //This is so stupid but it is the only way I can get it working
            SignInPage signInPage = new SignInPage();
            signInPage.ReturnToMainPage();
        }
    }

    private void OnSwitchToggle(object sender, EventArgs e)
    {
        string switchName;
        var switchSender = (Microsoft.Maui.Controls.Switch)sender;

        if (switchSender.IsToggled)
        {
            switchName = switchSender.AutomationId;
            stringBooleanMap[switchName] = true;
            switchCount++;
        }
        if (!switchSender.IsToggled)
        {
            switchName = switchSender.AutomationId;
            stringBooleanMap[switchName] = false;
            switchCount--;
        }
    }
}