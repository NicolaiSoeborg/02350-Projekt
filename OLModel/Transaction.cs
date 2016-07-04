using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    public class Transaction
    {
        public string studentId { get; }
        public string productId { get; }
        public int amount { get; }

        public Transaction(string studentId, string productId, int amount)
        {
            // TODO: null check? amount != 0, etc?
            this.studentId = studentId;
            this.productId = productId;
            this.amount = amount;
        }

        public override string ToString()
        {
            return String.Format("{0} bought {1} of {2}", studentId, amount, productId);
        }
    }
}
