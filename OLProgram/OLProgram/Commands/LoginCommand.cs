<<<<<<< HEAD
﻿
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OLProgram.Commands
//{
//    class LoginCommand
//    {
//        private ObservableCollection<String> users; // String skal være en Model.User
//        public string loggedInUser = null;
=======
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ObservableCollection<String> users; // String skal være en Model.User
        public string loggedInUser = null;
>>>>>>> origin/master

//        public Boolean Login(String username)
//        {
//            if (!users.Contains(username))
//                return false;

//            loggedInUser = username;
//            return true;
//        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
//    }
//}
=======
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
>>>>>>> origin/master
