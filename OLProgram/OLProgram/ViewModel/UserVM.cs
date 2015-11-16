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
        public Basket Basket { get; set; }
        public string HelloTxtUsername { get { return String.Format("Velkommen {0}!", loggedInUser.Name); } }

        public ICommand AddProductToBasketCommand { get; }
        

        public UserVM()
        {
            Basket = new Basket();

            // Commands:
            AddProductToBasketCommand = new RelayCommand(AddProductToBasket);
        }
       
        private void AddProductToBasket()
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, new Product(), 1));
        }
    }
}