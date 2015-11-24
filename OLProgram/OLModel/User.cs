using System;
using System.Collections.Generic;

namespace OLProgram.OLModel
{
    public class User : NotifyBase
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public Dictionary<int,int> ProductsBought { get; }
        public static int UserIDCounter = 2000;
        
        public User(int UserID, string Name)
        {
            if(UserID == -1)
            {
                this.UserID = UserIDCounter++;
            }
            else { this.UserID = UserID; }

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