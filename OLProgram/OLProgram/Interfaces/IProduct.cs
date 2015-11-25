using System.Collections.ObjectModel;

namespace OLProgram.Interfaces
{
    // Recommendations for Abstract Classes vs. Interfaces
    // https://msdn.microsoft.com/en-us/library/scsyfw1d(v=vs.71).aspx
    // TODO: Læs og find ud af om interfacet ser korrekt ud
    // TODO: Hvordan specificeres at alle produkter skal have en constructor med bestemt signatur?

    public interface IProduct
    {
        int ProductIdCounter { get; }
        int ProductId { get; }
        string Name { get; }
        string ImageFileName { get; }
        int Stock { get; set; }
        int Bought { get; set; }

        void Product(string ProductName);
        void Product(IProduct Product);
        void Product(string ProductName, string ImageFileName);
    }

    public interface IBasketItem : IProduct
    {
        int Count { get; set; }
        string CountAndName { get; set; }
        void BasketItem(IProduct Product);
        void BasketItem(IProduct Product, int Amount);
        void SetCount(int NewCount);
    }

    public interface IBasket
    {
        ObservableCollection<IBasketItem> BasketItems { get; set; }
        void Basket();
        void Increase(IProduct product, int count);
        int getCount(IProduct product);
    }
}
