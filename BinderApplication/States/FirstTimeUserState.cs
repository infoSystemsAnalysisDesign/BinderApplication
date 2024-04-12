using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication.States
{
    public class FirstTimeUserState
    {
        private bool firstTimeUserState = false;
        private static readonly Lazy<FirstTimeUserState> instance = new Lazy<FirstTimeUserState>(() => new FirstTimeUserState());

        private FirstTimeUserState()
        {

        }

        public static FirstTimeUserState Instance => instance.Value;

        public void EnableFirstTimeUserState() { firstTimeUserState = true; }
        public void DisableFirstTimeUserState() { firstTimeUserState = false; }
        public bool GetFirstTimeUserState() {  return firstTimeUserState; }
    }
}

