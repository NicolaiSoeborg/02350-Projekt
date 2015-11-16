using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLProgram.OLModel;
using OLProgram.Command;
using System.Collections.ObjectModel;
using OLModel;
using System.Windows;

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
            LoginCommand = new RelayCommand(DoLogin);
        }

        private void DoLogin()
        {
            // TODO: Check login
            loggedInUser = new User(0, _txtUsername);

            if (TxtUsername.ToLower().Equals("admin")) {
                MainWindow.Content = new View.AdminUC();
            } else {
                MainWindow.Content = new View.UserUC();
            }
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}