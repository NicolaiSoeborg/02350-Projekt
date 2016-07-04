using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace OLModel
{
    public class Product : NotifyBase
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageFileName { get; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int Bought {
            get {
                return Model.Instance.Transactions
                    .Where(t => ProductId.Equals(t.productId))
                    .Sum(t => t.amount);
            }
        }
        


        public Product(string productName, int price)
            : this(null, productName, price, 0, "../Images/default.png") { }

        // This Constructor is used for making basketitems, making clone of the parameter Product
        public Product(Product product)
        {
            if (product == null) return;
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Price = product.Price;
            Stock = product.Stock;
            ImageFileName = product.ImageFileName;
        }

        public Product(string productId, string productName, int price, int stock, string imageFileName)
        {
            if (productId == null)
                productId = getNewProductId();
            this.ProductId = productId;
            this.ProductName = productName;
            this.Price = price;
            this.Stock = stock;
            this.ImageFileName = imageFileName;
        }

        private string getNewProductId()
        {
            // TODO: SELECT max(productID AS INTEGER) FROM products;
            int i = 1;
            while (true)
            {
                bool idAlreadInUse = false;
                foreach (Product p in Model.Instance.Products)
                {
                    idAlreadInUse |= p.ProductId.Equals(i.ToString());
                }
                if (idAlreadInUse)
                    i++;
                else break;
            }
            return i.ToString();
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", ProductId, ProductName);
        }
    }    

}