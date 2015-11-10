using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLProgram.OLModel;
using OLProgram.Commands;
using System.Collections.ObjectModel;

namespace OLProgram.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        /*public ICommand LoginCommand
        {
            get { return new Commands.LoginCommand(); }
        }*/

        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        //public ObservableCollection<String> users { get; set; }
        public ObservableCollection<Product> Products { get; set; }


        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        // Bindings to UI
        public ICommand AddProductToGlobalCommand { get; }


        public MainViewModel()
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

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);

            AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);

        }

        private void AddProductToGlobal()
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Products, new Product()));
        }
    }
}