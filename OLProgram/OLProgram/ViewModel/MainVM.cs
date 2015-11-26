using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLProgram.OLModel;
using OLProgram.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

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