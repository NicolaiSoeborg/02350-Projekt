using OLProgram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram
{
    // Recommendations for Abstract Classes vs. Interfaces
    // https://msdn.microsoft.com/en-us/library/scsyfw1d(v=vs.71).aspx
    // TODO: Læs og find ud af om interfacet ser korrekt ud
    
    public interface IUser
    {
        int UserID { get; set; }
        string Name { get; set; }
        Dictionary<int, int> ProductsBought { get; set; }

        void BuyProducts(IProduct product, int amountBought);
    }
}
