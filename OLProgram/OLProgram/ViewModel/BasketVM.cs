﻿using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLModel;
using System;
using System.Windows.Input;

namespace OLProgram.ViewModel
{
    public class BasketVM : BaseVM
    {
        public static Basket Basket { get; set; }
        public string HelloTxtUsername { get { return loggedInUser == null ? "NoUserLoggedIn" : String.Format("Velkommen {0}!", loggedInUser.Name); } }

        public ICommand AddProductToBasketCommand { get; }
        public ICommand DecreaseBasketItemCommand { get; }
        public ICommand DeleteBasketItemCommand { get; }
        public ICommand ClearBasketCommand { get; }
        public RelayCommand CheckOutCommand { get; }

        public BasketVM()
        {
            if (Basket == null) Basket = new Basket();

            // Commands to access from UI:
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DecreaseBasketItemCommand = new RelayCommand<Product>(DecreaseBasketItem);
            DeleteBasketItemCommand = new RelayCommand<Product>(DeleteBasketItem);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            CheckOutCommand = new RelayCommand(CheckOutBasket);
        }

        private void CheckOutBasket()
        {
            MainWindow.Content = new View.LoginUC();
        }

        private void AddProductToBasket(Product product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, 1));
        }

        private void DecreaseBasketItem(Product product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, -1));
        }
        private void DeleteBasketItem(Product product)
        {
            int count = Basket.getCount(product);
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, -count));
        }

        private void ClearBasket()
        {
            undoRedoController.AddAndExecute(new ClearBasketCommand(Basket));
        }
    }
}