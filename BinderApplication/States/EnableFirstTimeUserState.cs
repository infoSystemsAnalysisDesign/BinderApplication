using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * This class controls the state of if this is the first time user
 * The state is activated by the sign-in page when it detects a user has < 4 genres selected
 * That will then force them to the genres page, wherein the state will then be deactivated after the welcome message is given
 */ 

namespace BinderApplication.States
{
    public class EnableFirstTimeUserState
    {
        private static readonly EnableFirstTimeUserState instance = new EnableFirstTimeUserState();

        private EnableFirstTimeUserState() { }

        public static EnableFirstTimeUserState Instance => instance;

        public bool IsFirstTimeUser { get; set; }
    }
}
