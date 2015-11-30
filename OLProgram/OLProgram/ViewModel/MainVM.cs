using System;
using OLModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System.Windows;

namespace OLProgram.ViewModel
{
    public class MainVM : BaseVM
    {
        // Tekstbox på LoginUC:
        private string _txtUsername = "";
        public string TxtUsername
        {
            get { if (loggedInUser == null) return _txtUsername; /*else*/ return loggedInUser.Name; }
            set { _txtUsername = value; }
        }

        public RelayCommand<String> LoginCommand { get; }
        public RelayCommand<int> keyPressedCommand { get; }
        //public RelayCommand EnterPressedCommand { get; }

        public MainVM()
        {
            // Nothing to initialize when creating the MainWM?

            // Commands:
            LoginCommand = new RelayCommand<String>(DoLogin);
            //EnterPressedCommand = new RelayCommand(EnterPressed);
        }

        private void DoLogin(String BarCode)
        {
            int currentBarCodeValue;
            if (int.TryParse(BarCode, out currentBarCodeValue) && BarCode.Length == 4)
            {
                foreach (User user in Users)
                {
                    if (user.UserID == currentBarCodeValue)
                    {
                        loggedInUser = user;
                        MainWindow.Content = new View.UserUC();
                        break;
                    }
                }
                if (loggedInUser == null)
                    MessageBox.Show("Selected user does not exist in database", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("Not a valid Barcode", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}