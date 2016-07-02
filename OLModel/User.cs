using System;
using System.Collections.Generic;
using System.IO;

namespace OLModel
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }

        public User(string name) : this(null, name) { }

        public User(string userID, string name)
        {
            if (userID == null)
                userID = ""; // TODO
            this.UserID = userID;
            this.Name = name;
        }

        // Product is added to the users list and bought is incremented 
        public void BuyProducts(string ProductID, int amountBought)
        {
            // Add "product*amount" to user
            /*
            if (ProductsBought.ContainsKey(ProductID))
                ProductsBought[ProductID] += amountBought;
            else
                ProductsBought.Add(ProductID, amountBought);
            */
        }

        public override String ToString()
        {
            return String.Format("{0}: {1}", UserID, Name);
        }
    }
}