using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace OLModel
{
    public class Product : NotifyBase
    {
        // Overvej at slette, da den ikke bliver genmt ned i XML og derved potentielt kan fucke det op
        public static int ProductIdCounter = 0;

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFileName { get; }
        public int Stock { get; set; }
        public int Bought { get; set; }

        internal Product() : this("", "") { } // Used by serializer

        public Product(string productName) : this(productName, "../Images/default.png") { }

        // This Constructor is used for making basketitems, making clone of the parameter Product
        public Product(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            ImageFileName = product.ImageFileName;
        }

        public Product(string productName, string imageFileName)
        {
            ProductId = ProductIdCounter++;
            this.ProductName = productName;
            this.ImageFileName = imageFileName;
        }

        public MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(Product));
            serializer.Serialize(stream, this);
            return stream;
        }

        public static Product Deserialize(Stream serialization)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Product));
            return serializer.Deserialize(serialization) as Product; // TODO: What if we can't deserialize.
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", ProductId, ProductName);
        }
    }    

}