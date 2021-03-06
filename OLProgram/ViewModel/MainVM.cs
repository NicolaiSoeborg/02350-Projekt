using System;
using OLModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;

namespace OLProgram.ViewModel
{
    public class MainVM : BaseVM
    {
        public static List<string> PublicLog { get { return Model.Instance.UserLog; } }

        // Tekstbox p� LoginUC:
        public string loginTextBox { get; set; }
        
        private string _txtUsername = "";
        public string TxtUsername
        {
            get { if (loggedInUser == null) return _txtUsername; /*else*/ return loggedInUser.Name; }
            set { if (value != null) _txtUsername = value; }
        }

        public RelayCommand<String> LoginCommand { get; }
        public RelayCommand<int> keyPressedCommand { get; }

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?

            // Commands:
            LoginCommand = new RelayCommand<String>(DoLogin);
        }

        private void DoLogin(String BarCode)
        {
            if (BarCode != null && BarCode.Length == 4)
            {
                foreach (User user in Model.Instance.Users)
                {
                    if (BarCode.Equals(user.UserID))
                    {
                        loggedInUser = user;
                        MainWindow.Content = new View.UserUC();
                        break;
                    }
                }
                if (loggedInUser == null)
                {
                    MessageBox.Show("Selected user does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    RaisePropertyChanged(() => loginTextBox);
                }
            }
            else
            {
                MessageBox.Show("Not a valid barcode", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);                
                RaisePropertyChanged(() => loginTextBox);
            }
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}