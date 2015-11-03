using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Commands
{
    public class AddProductToBasketCommand : IUndoRedoCommand   
    {
        #region Fields

        private ObservableCollection<Product> productsInBasket;
        private Product productInBasket;

        #endregion
            
        #region Constructor 

        public AddProductToBasketCommand(ObservableCollection<Product> _products, Product _product)
        {
            productInBasket = _product;
            productsInBasket = _products;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            productsInBasket.Add(productInBasket);
        }

        public void UnExecute()
        {
            productsInBasket.Remove(productInBasket);
        }

        #endregion



    }
}
