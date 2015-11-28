using System;
using OLModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace OLProgram.ViewModel
{
    public class MainVM : BaseVM
    {
        // Tekstbox på LoginUC:
        private string _txtUsername = "";
        public string TxtUsername {
            get { if (loggedInUser == null) return _txtUsername; /*else*/ return loggedInUser.Name; }
            set { _txtUsername = value; }
        }

        public ICommand LoginCommand { get; }

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?

            // Commands:
            LoginCommand = new RelayCommand(DoLogin);
        }

        private void DoLogin()
        {
            // TODO: Check login
            loggedInUser = new User(_txtUsername); // TODO: Load user
            MainWindow.Content = new View.UserUC();
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}