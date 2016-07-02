using System;
using System.IO;
using System.Collections.ObjectModel;

namespace OLModel
{
    public class Product : NotifyBase
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFileName { get; }
        public int Stock { get; set; } // TODO
        public int Bought {
            get {
                int amount = 0;
                foreach (Transaction t in Model.Instance.Transactions)
                {
                    if (this.ProductId.Equals(t.productId))
                        amount += t.amount;
                }
                return amount;
            }
        }
        public int Price { get; set; }

        public Product(string productName, int price)
            : this(null, productName, price, "../Images/default.png") { }

        public Product(string productId, string productName, int price)
            : this(productId, productName, price, "../Images/default.png") { }

        // This Constructor is used for making basketitems, making clone of the parameter Product
        public Product(Product product)
        {
            if (product == null) return;
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Price = product.Price;
            ImageFileName = product.ImageFileName;
        }

        public Product(string productId, string productName, int price, string imageFileName)
        {
            if (productId == null) productId = "TODO";
            this.ProductId = productId;
            this.ProductName = productName;
            this.Price = price;
            this.ImageFileName = imageFileName;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", ProductId, ProductName);
        }
    }    

}