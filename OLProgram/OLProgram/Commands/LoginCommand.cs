using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.Commands
{
    class LoginCommand
    {
        private ObservableCollection<String> users; // String skal være en Model.User
        public string loggedInUser = null;

        public Boolean Login(String username)
        {
            if (!users.Contains(username))
                return false;

            loggedInUser = username;
            return true;
        }


    }
}
