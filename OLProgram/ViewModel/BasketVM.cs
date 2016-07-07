using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLModel;
using System;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;

namespace OLProgram.ViewModel
{
    public class BasketVM : BaseVM
    {
        public static Basket Basket { get; set; }
        public string HelloTxtUsername { get { return loggedInUser == null ? "NoUserLoggedIn" : String.Format("Velkommen {0}!", loggedInUser.Name); } }
        public static String InputForBasket { get; set; }

        public ICommand AddProductToBasketCommand { get; }
        public ICommand DecreaseBasketItemCommand { get; }
        public ICommand DeleteBasketItemCommand { get; }
        public ICommand ClearBasketCommand { get; }
        public RelayCommand CheckOutCommand { get; }
        public RelayCommand EnterCommand { get; }
        public RelayCommand<KeyEventArgs> writeInputCommand { get; }
        public RelayCommand HomeCommand { get; }

        public BasketVM()
        {
            if (Basket == null)
                Basket = new Basket();
            InputForBasket = "";

            // Commands to access from UI:
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DecreaseBasketItemCommand = new RelayCommand<Product>(DecreaseBasketItem);
            DeleteBasketItemCommand = new RelayCommand<Product>(DeleteBasketItem);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            CheckOutCommand = new RelayCommand(CheckOutBasket);
            EnterCommand = new RelayCommand(EnterInput);
            writeInputCommand = new RelayCommand<KeyEventArgs>(writeInput);
            HomeCommand = new RelayCommand(HomeButton);
        }
        
        private readonly IReadOnlyDictionary<Key, char> num2Char = new Dictionary<Key, char> { { Key.D0, '0' }, { Key.D1, '1' }, { Key.D2, '2' }, { Key.D3, '3' }, { Key.D4, '4' }, { Key.D5, '5' }, { Key.D6, '6' }, { Key.D7, '7' }, { Key.D8, '8' }, { Key.D9, '9' } };
        private void writeInput(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                EnterInput();
            else
            {
                if (num2Char.ContainsKey(e.Key))
                    InputForBasket += num2Char[e.Key];
            }
        }

        // Kræver optimeringer
        public void EnterInput()
        {            
            if (InputForBasket != null)
            { 
                if (InputForBasket.Length == 4) {
                    EnterInput_User();
                } else {
                    EnterInput_Product();
                }
            }
            InputForBasket = "";
        }

        private void EnterInput_User()
        {
            foreach (User user in Model.Instance.Users)
            {
                if (InputForBasket.Equals(user.UserID))
                {
                    CheckOutBasket();
                    return;
                }
            }
        }

        private void EnterInput_Product()
        {
            foreach (Product product in Model.Instance.Products)
            {
                if (InputForBasket.Equals(product.ProductId))
                {
                    undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, 1));
                    return;
                }
            }
        }

        private void CheckOutBasket()
        {
            foreach (BasketItem basketItem in Basket.BasketItems) {
                Model.Instance.Users[Model.Instance.Users.IndexOf(loggedInUser)].BuyProducts(basketItem.ProductId, basketItem.Count);
                OLModel.Helpers.PublicLog("{0} bought {1} of {2} ({3})", loggedInUser, basketItem.Count, basketItem.Name, basketItem.ProductId);

                foreach (Product product in Model.Instance.Products)
                {
                    if (product.ProductId.Equals(basketItem.ProductId))
                    {
                        product.Stock -= basketItem.Count;
                    }
                }
            }

            loggedInUser = null; // logout
            new ClearBasketCommand(Basket).Execute();
            undoRedoController.ClearUndoRedoStacks();
            MainWindow.Content = new View.LoginUC();
        }

        private void HomeButton()
        {
            new ClearBasketCommand(Basket).Execute();
            undoRedoController.ClearUndoRedoStacks();
            loggedInUser = null;
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