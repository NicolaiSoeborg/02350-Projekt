using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OLModel
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; } = 0;
        [XmlIgnore] public Dictionary<int,int> ProductsBought { get; } // TODO: NOT XmlSerializer-able!
        public static int UserIDCounter = 2000;
        

        internal User() : this(0, "") { } // Used by serializer
        public User(string name) : this(UserIDCounter++, name) { }

        public User(int userID, string name)
        {
            this.UserID = userID;
            this.Name = name;
            this.ProductsBought = new Dictionary<int, int>();
        }

        // Product is added to the users list and bought is incremented 
        public void BuyProducts(int ProductID, int amountBought)
        {
            // Add "product*amount" to user
            if (ProductsBought.ContainsKey(ProductID))
                ProductsBought[ProductID] += amountBought;
            else
                ProductsBought.Add(ProductID, amountBought);

            ProductsCount += amountBought;
        }

        public MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            serializer.Serialize(stream, this);
            return stream;
        }

        public static User Deserialize(Stream serialization)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            return serializer.Deserialize(serialization) as User; // TODO: What if we can't deserialize.
        }

        public override String ToString()
        {
            return String.Format("{0}: {1}", UserID, Name);
        }
    }
}