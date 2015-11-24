using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLProgram.OLModel;
using OLProgram.Command;
using System.Collections.ObjectModel;
using OLModel;
using System.Windows;
using System.Windows.Controls;

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

        public ICommand LoginCommand { get { return new RelayCommand(DoLogin); } }
        public ICommand ChangeToAdminCommand { get { return new RelayCommand(ShowAdminLoginGUI); } }

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?
        }

        private void DoLogin()
        {
            // TODO: Check login
            loggedInUser = new User(0, _txtUsername); // TODO: Load user
            MainWindow.Content = new View.UserUC();
        }

        private void ShowAdminLoginGUI()
        {
            (new View.AdminLoginWindow()).ShowDialog();
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}