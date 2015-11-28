using OLModel;

namespace OLProgram.Command
{
    public class AddProductToBasketCommand : IUndoRedoCommand
    {
        private Basket _basket;
        private Product _product;
        private int _count;

        public AddProductToBasketCommand(Basket basket, Product product, int count)
        {
            _product = product;
            _count = count;
            _basket = basket;
        }

        public void Execute()
        {
            _basket.Increase(_product, _count);
        }

        public void UnExecute()
        {
            _basket.Increase(_product, -_count);
        }
    }
}
