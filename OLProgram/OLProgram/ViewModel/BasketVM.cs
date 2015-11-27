using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLProgram.OLModel;
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

        public BasketVM()
        {
            if (Basket == null) Basket = new Basket();

            // Commands to access from UI:
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DecreaseBasketItemCommand = new RelayCommand<Product>(DecreaseBasketItem);
            DeleteBasketItemCommand = new RelayCommand<Product>(DeleteBasketItem);
            ClearBasketCommand = new RelayCommand(ClearBasket);
        }
       
        private void AddProductToBasket(Product Product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, Product, 1));
        }

        private void DecreaseBasketItem(Product Product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, Product, -1));
        }
        private void DeleteBasketItem(Product Product)
        {
            int Count = Basket.getCount(Product);
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, Product, -Count));
        }

        private void ClearBasket()
        {
            undoRedoController.AddAndExecute(new ClearBasketCommand(Basket));
        }
    }
}