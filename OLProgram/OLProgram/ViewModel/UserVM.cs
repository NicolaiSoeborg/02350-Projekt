using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
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
    public class UserVM : BaseVM
    {
        public static Basket Basket { get; set; }

        public string HelloTxtUsername { get { return loggedInUser == null ? "NoUserLoggedIn" : String.Format("Velkommen {0}!", loggedInUser.Name); } }

        public ICommand AddProductToBasketCommand { get; }
        public ICommand DeleteBasketCommand { get; }

        public UserVM()
        {
            if (Basket == null) Basket = new Basket();

            // Commands to access from UI:
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DeleteBasketCommand = new RelayCommand(DeleteBasket);
        }
       
        private void AddProductToBasket(Product Product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, Product, 1));
        }

        private void DeleteBasket()
        {
            //TODO Make DeleteBasket
            //undoRedoController.AddAndExecute(new DeleteBasketCommand(Basket));
        }

    }
}