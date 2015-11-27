using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OLProgram.OLModel
{
    public class User : NotifyBase
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        [XmlIgnore] public Dictionary<int,int> ProductsBought { get; } // TODO: NOT XmlSerializer-able...
        public static int UserIDCounter = 2000;

        internal User() : this(0, "") { } // Used by serializer
        public User(string Name) : this(UserIDCounter++, Name) { }

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