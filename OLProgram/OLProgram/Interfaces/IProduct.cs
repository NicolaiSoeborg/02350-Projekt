using System.Collections.ObjectModel;

namespace OLProgram.Interfaces
{
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
}
