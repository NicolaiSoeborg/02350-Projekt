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
        public string TxtUsername { get { return loggedInUser.Name; } set { loggedInUser = new User(0, value); } }


        public RelayCommand ShowStatisticCommand { get; }

        public MainVM()
        {
            //ShowStatisticCommand = new RelayCommand(ShowStatistic);
        }

        //private void ShowStatistic()
        //{
        //    TODO
        //}

    }
}