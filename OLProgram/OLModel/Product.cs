using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class Product
    {
        // Overvej at slette, da den ikke bliver genmt ned i XML og derved potentielt kan fucke det op
        private static int ProductIdCounter = 0;

        public int ProductId { get; }

        public string Name { get; set; }
        public string ImageFileName { get; set; }

        public Product() : this(ProductIdCounter, "<NAME>", "default.png")
        {
            ProductIdCounter++;
        }

        public Product(int ProductID, string Name, string ImageFileName)
        {
            this.ProductId = ProductID;
            this.Name = Name;
            this.ImageFileName = ImageFileName;
        }
    }

    public class BasketItem : NotifyBase
    {
        public Product Product { get; }
        public int Count { get; set; }

        public BasketItem()
        {
            Count++;
        }

        public BasketItem(Product Product) : this(Product, 1) { }

        public BasketItem(Product Product, int Count)
        {
            this.Product = Product;
            this.Count = Math.Max(Count, 1);
        }
    }
}
