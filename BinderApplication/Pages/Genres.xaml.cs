using BinderApplication.Database;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BinderApplication.Pages;

public partial class Genres : ContentPage
{
    //Why C# gotta be quirky and call Hashmaps dictionaries
    //Hashmap to store whether a switch is flipped on or off
    // Initialize the dictionary with all genres set to false
    Dictionary<string, bool> stringBooleanMap = new Dictionary<string, bool>()
        {
            { "Drama", false },
            { "Essay", false },
            { "Fiction", false },
            { "History", false },
            { "Horror", false },
            { "NonFiction", false },
            { "Novel", false },
            { "Philosophy", false },
            { "Poetry", false },
            { "Politics", false },
            { "Psychology", false },
            { "Romance", false },
            { "Science", false },
            { "Spirituality", false },
            { "Suspense", false },
            { "Thriller", false }
        };
    int switchCount = 0;
    DatabaseGenre dbGenre = new DatabaseGenre();

    public Genres()
	{
		InitializeComponent();

        

        // Now all genres are initialized to false in the stringBooleanMap dictionary


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

    private void OnSaveButtonClicked(object sender, EventArgs e)
	{
        if (switchCount < 4)
        {
            DisplayAlert("Oops", "Please select at least 4 genres!", "OK");
        }
        else
        {
            DisplayAlert("Success", "Save Success", "OK");

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