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
        // Tekstbox p� LoginUC:
        private string _txtUsername = "";
        public string TxtUsername {
            get { if (loggedInUser == null) return _txtUsername; /*else*/ return loggedInUser.Name; }
            set { _txtUsername = value; }
        }

        public ICommand LoginCommand { get { return new RelayCommand(DoLogin); } }
        public ICommand ChangeToAdminCommand { get; } = new RelayCommand(ShowAdminLoginGUI);
        public ICommand ChangeToMainCommand { get; } = new RelayCommand(ShowMainLoginGUI);
        public ICommand ChangeToAddUserCommand { get; } = new RelayCommand(ShowAddUserGUI);

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?
        }

        private void DoLogin()
        {
            // TODO: Check login
            loggedInUser = new User(_txtUsername); // TODO: Load user
            MainWindow.Content = new View.UserUC();
        }

        private static void ShowAdminLoginGUI()
        {
            (new View.AdminLoginWindow()).ShowDialog();
        }

        private static void ShowMainLoginGUI()
        {
                MainWindow.Content = new View.LoginUC();
        }

        private static void ShowAddUserGUI()
        {
            MainWindow.Content = new View.AddUserUC();
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}