using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    class Product
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

    class BasketItem
    {
        public int ProductId { get; }
        public int Count { get; set; }

        public BasketItem(int ProductId) : this(ProductId, 1) { }

        public BasketItem(int ProductId, int Count)
        {
            this.ProductId = ProductId;
            this.Count = Math.Max(Count, 1);
        }
    }
}
