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

        private Basket basket;
        private Product product;
        private int count;

        #endregion
            
        #region Constructor 

        public AddProductToBasketCommand(Basket _basket, Product _product, int _count)
        {
            product = _product;
            count = _count;
            basket = _basket;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            basket.Increase(product, count);
        }

        public void UnExecute()
        {
            basket.Decrease(product, count);
        }

        #endregion



    }
}
