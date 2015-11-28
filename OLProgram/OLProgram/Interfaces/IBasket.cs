using System.Collections.ObjectModel;

namespace OLProgram.Interfaces
{
    public interface IBasket
    {
        ObservableCollection<IBasketItem> BasketItems { get; set; }
        void Basket();
        void Increase(IProduct product, int count);
        int getCount(IProduct product);
    }

    public interface IBasketItem : IProduct
    {
        int Count { get; set; }
        string CountAndName { get; set; }
        void BasketItem(IProduct Product);
        void BasketItem(IProduct Product, int Amount);
        void SetCount(int NewCount);
    }
}
