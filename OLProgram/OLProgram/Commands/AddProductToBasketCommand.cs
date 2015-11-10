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

        private ObservableCollection<BasketItem> basketItems;
        private Product product;
        private int count;

        #endregion
            
        #region Constructor 

        public AddProductToBasketCommand(ObservableCollection<BasketItem> _basketItems, Product _product, int _count)
        {
            product = _product;
            count = _count;
            basketItems = _basketItems;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            basketItems.Add(new BasketItem(product, count));
        }

        public void UnExecute()
        {
            //basketItems.Remove(basketItem);
        }

        #endregion



    }
}
