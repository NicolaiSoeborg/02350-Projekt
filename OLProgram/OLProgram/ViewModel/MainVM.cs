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

    public class MainVM : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        /*public ICommand LoginCommand
        {
            get { return new Commands.LoginCommand(); }
        }*/
        
        public ICommand closeApplicationCommand { get; }

        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        //closeApplicationCommand = new RelayCommand(closeApplication(0));

        // Commands that the UI can be bound to.
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddUserCommand { get; }

        // Bindings to UI
        public ICommand AddProductToGlobalCommand { get; }





        public MainVM()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            Products = new ObservableCollection<Product>();

            Users = new ObservableCollection<User>()
            {
                new User() { UserID = 1001, Name = "Rasmus" },
                new User() { UserID = 1002, Name = "Nicolai" },
                new User() { UserID = 1003, Name = "Silas" },
                new User() { UserID = 1004, Name = "Greven" }
            }; 

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);

            //AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);


            // Commands
            AddUserCommand = new RelayCommand(AddUser);

        }
        private void AddUser()
        {
            undoRedoController.AddAndExecute(new AddUserCommand(Users, new User()));
        }

        //private void AddProductToGlobal()
        //{
        //    undoRedoController.AddAndExecute(new AddProductToBasketCommand(Products, new Product()));
        //}

        private void closeApplication(int status)
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            //if (response = MessageBoxResult.No)
            // {

            //} else
            // {
            //     Environment.Exit(0);
            // }



        }


    }
}