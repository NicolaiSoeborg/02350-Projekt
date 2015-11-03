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

        private ObservableCollection<Product> basketItems;
        private Product baskeItem;

        #endregion
            
        #region Constructor 

        public AddProductToBasketCommand(ObservableCollection<Product> _basketItems, Product _basketItem)
        {
            baskeItem = _basketItem;
            basketItems = _basketItems;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            basketItems.Add(baskeItem);
        }

        public void UnExecute()
        {
            basketItems.Remove(baskeItem);
        }

        #endregion



    }
}
