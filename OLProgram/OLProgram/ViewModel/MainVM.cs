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

        public ICommand LoginCommand { get { return new RelayCommand(DoLogin); } }
        public ICommand ChangeToAdminCommand { get { return new RelayCommand(ShowAdminLoginGUI); } }
        public ICommand ChangeToMainCommand { get { return new RelayCommand(ShowMainLoginGUI);  } }
        public ICommand ChangeToAddUserCommand { get { return new RelayCommand(ShowAddUserGUI); } }

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?
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

        private void ShowAdminLoginGUI()
        {
            Window Box = new Window();//window for the inputbox
            Box.Title = "Admin password";
            Box.Width = 300; //Box.Height = 200;
            Box.Topmost = true;
            StackPanel sp = new StackPanel();

            TextBlock overskrift = new TextBlock(); overskrift.Text = "Admin Password:";
            sp.Children.Add(overskrift);

            TextBox input = new TextBox();
            sp.Children.Add(input);

            PasswordBox test = new PasswordBox();
            sp.Children.Add(test);
            
            Button ok = new Button(); ok.Content = "Login";
            ok.AddHandler(Button.ClickEvent, new RoutedEventHandler(DoAdminLogin_Click));
            sp.Children.Add(ok);

            Box.Content = sp;
            Box.ShowDialog();
        }

        private void DoAdminLogin_Click(object o, RoutedEventArgs e)
        {
            var ButtonEvent = e.OriginalSource as Button;
            if (ButtonEvent != null)
                MainWindow.Content = new View.AdminUC();
        }

        private void ShowMainLoginGUI()
        {
                MainWindow.Content = new View.LoginUC();
        }

        private void ShowAddUserGUI()
        {
            MainWindow.Content = new View.AddUserUC();
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}