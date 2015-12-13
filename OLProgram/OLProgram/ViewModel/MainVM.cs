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
                {
                    MessageBox.Show("Selected user does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    
                    RaisePropertyChanged(() => loginTextBox);
                }
            }
            else
            {
                MessageBox.Show("Not a valid Barcode", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
                RaisePropertyChanged(() => loginTextBox);
            }
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}