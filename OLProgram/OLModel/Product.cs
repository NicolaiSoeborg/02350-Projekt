using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    public class Product
    {
        private static int ProductIdCounter = 0;

        public int ProductId { get; }

        public string Name { get; set; }
        public string ImageFileName { get; set; }
        
        public Product() : this("<NAME>", "default.png") { }

        public Product(string Name, string ImageFileName)
        {
            this.ProductId = ProductIdCounter++;
            this.Name = Name;
            this.ImageFileName = ImageFileName;
        }
    }

    public class BasketItem
    {
        public Product Product { get; }
        public int Count { get; set; }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Count)
        {
            this.Product = Product;
            this.Count = Math.Max(Count, 1);
        }
    }
}
