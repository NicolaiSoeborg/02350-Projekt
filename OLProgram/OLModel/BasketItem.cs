using System;

namespace OLModel
{
    public class BasketItem : Product
    {
        private int _count;
        private string _name;
        private String _countAndName;

        public int Count { get { return _count; } set { _count = value; NotifyPropertyChanged(); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }

        public String CountAndName { get { return _countAndName; } set { _countAndName = value; NotifyPropertyChanged(); } }

        public BasketItem(Product product) : this(product, 1) { }

        public BasketItem(Product product, int amount) : base(product)
        {
            Count = Math.Max(amount, 1);
            Name = product.ProductName;
            CountAndName = String.Format("{0} x {1}", Count, product.ProductName);
        }

        public void SetCount(int newCount)
        {
            Count = newCount;
            CountAndName = String.Format("{0} x {1}", Count, ProductName);
        }
    }
}
