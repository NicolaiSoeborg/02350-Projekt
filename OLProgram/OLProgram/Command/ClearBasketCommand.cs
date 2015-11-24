using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Command
{
    public class ClearBasketCommand : IUndoRedoCommand
    {
        #region Fields

        private Basket _basket;
        private BasketItem[] _clearedItemsBackup;
        #endregion

        #region Constructor 

        public ClearBasketCommand(Basket basket)
        {
            _basket = basket;
            _clearedItemsBackup = new BasketItem[basket.BasketItems.Count];
            basket.BasketItems.CopyTo(_clearedItemsBackup, 0);
        }
    
        #endregion

        #region Methods

        public void Execute()
        {
            while (_basket.BasketItems.Count > 0)
                _basket.BasketItems.RemoveAt(0);
        }

        public void UnExecute()
        {
            for(int i = 0; i < _clearedItemsBackup.Length; i++)
            {
                _basket.BasketItems.Add(_clearedItemsBackup[i]);
            }
        }

        #endregion

    }
}
