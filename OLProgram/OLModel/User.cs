using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class User 
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public Dictionary<int,int> ProductsBought { get; }
        
        public User(int UserID, string Name)
        {
            this.UserID = UserID;
            this.Name = Name;
            this.ProductsBought = new Dictionary<int, int>();
        }

        // Product is added to the users list and bought is incremented 
        public void BuyProducts(Product product, int amountBought)
        {
            // Add "product*amount" to user
            if (ProductsBought.ContainsKey(product.ProductId))
                ProductsBought[product.ProductId] += amountBought;
            else
                ProductsBought.Add(product.ProductId, amountBought);
            

            // Add "amount bought" to product
            product.Bought += amountBought;
        }

        override
        public String ToString()
        {
            return ("" + this.UserID + " " + this.Name) ;
        }
    }
}