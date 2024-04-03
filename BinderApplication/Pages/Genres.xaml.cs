using BinderApplication.States;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BinderApplication.Pages;

public partial class Genres : ContentPage
{
    //Why C# gotta be quirky and call Hashmaps dictionaries
    Dictionary<string, bool> stringBooleanMap;
    int switchCount = 0;

    public Genres()
	{
		InitializeComponent();

        //Hashmap to store whether a switch is flipped on or off
        stringBooleanMap = new Dictionary<string, bool>();

        /*
         * We are going to need a way to read in the hashmap values
         * from the database.
         * We can then use those values to have the toggles that should be on be on already
         * 
         * Example: 
         * Drama.IsToggled = true;
         * switchCount++;
         * 
         * I need to make it so you are given a default hashmap of all false values tied
         * to your account at account creation.
         * This will require gettin grid of all current accounts :(
         * If genres < 4 (so it covers below min genres and when 
         * account is first created with all false) Have the app pull up a modified
         * version of the genre page that requires you to pick at leats 4 genres.
         * The genres page will also need to read in the hashmap values.
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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var firstTimeUser = EnableFirstTimeUserState.Instance;
        bool firstTimeState = firstTimeUser.IsFirstTimeUser;

        if (firstTimeState == true)
        {
            FirstTimeUserState();
            firstTimeUser.IsFirstTimeUser = false;
        }
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
	{
        if (switchCount < 4)
        {
            DisplayAlert("Oops", "Please select at least 4 genres!", "OK");
        }
        else
        {
            DisplayAlert("Success", "Save Success", "OK");

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

    private void FirstTimeUserState()
    {
        //string message = "Welcome to Binder!\n\nWe only want you to see books that you will love, so please select at least 4 genres of literature you enjoy!";
        //DisplayAlert("Welcome!", message, "Let's Go!");
    }
}