﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Commands;
using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace OLProgram.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        //public ObservableCollection<String> users { get; set; }
        //public ObservableCollection<BasketItem> BasketItems { get ; set; }
        public Basket basket { get; set; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        // Bindings to UI
        public ICommand AddProductToBasketCommand { get; }


        public UserViewModel()
        {
            //BasketItems = new ObservableCollection<BasketItem>();

            basket = new Basket();

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);



            AddProductToBasketCommand = new RelayCommand(AddProductToBasket);

        }
       
        private void AddProductToBasket()
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(basket, new Product(), 1));
        }
    }
}
