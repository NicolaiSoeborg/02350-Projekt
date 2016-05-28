using System;

namespace OLModel
{
    public class BasketItem : Product
    {
        private int _count;
        private string _name;

        public int Count { get { return _count; } set { _count = value; NotifyPropertyChanged(); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
        //public string CountAndName { get { return String.Format("{0} x {1}", Count, Name); } }

        public BasketItem(Product product) : this(product, 1) { }

        public BasketItem(Product product, int amount) : base(product)
        {
            Count = Math.Max(amount, 1);
            Name = product.ProductName;
        }

        public void SetCount(int newCount)
        {
            Count = newCount;
        }
    }
}
