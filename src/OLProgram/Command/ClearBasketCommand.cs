using OLModel;

namespace OLProgram.Command
{
    public class ClearBasketCommand : IUndoRedoCommand
    {
        private Basket _basket;
        private BasketItem[] _clearedItemsBackup;
        
        public ClearBasketCommand(Basket basket)
        {
            _basket = basket;
            _clearedItemsBackup = new BasketItem[basket.BasketItems.Count];
            basket.BasketItems.CopyTo(_clearedItemsBackup, 0);
        }
    
        public void Execute()
        {
            while (_basket.BasketItems.Count > 0)
                _basket.BasketItems.RemoveAt(0);
        }

        public void UnExecute()
        {
            for (int i = 0; i < _clearedItemsBackup.Length; i++)
            {
                _basket.BasketItems.Add(_clearedItemsBackup[i]);
            }
        }
    }
}
