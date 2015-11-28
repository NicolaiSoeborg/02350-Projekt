using System.Collections.Generic;

namespace OLProgram.Interfaces
{
    public interface IUser
    {
        int UserID { get; set; }
        string Name { get; set; }
        Dictionary<int, int> ProductsBought { get; set; }
        int UserIDCounter { get; }

        void User(string Name);
        void User(int UserID, string Name);
        
        void BuyProducts(IProduct product, int amountBought);
    }
}
