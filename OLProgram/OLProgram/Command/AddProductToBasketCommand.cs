using OLModel;

namespace OLProgram.Command
{
    public class AddProductToBasketCommand : IUndoRedoCommand
    {
        #region Fields

        private Basket _basket;
        private Product _product;
        private int _count;

        #endregion

        #region Constructor 

        public AddProductToBasketCommand(Basket basket, Product product, int count)
        {
            _product = product;
            _count = count;
            _basket = basket;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            _basket.Increase(_product, _count);
        }

        public void UnExecute()
        {
            _basket.Increase(_product, -_count);
        }

        #endregion

    }
}
