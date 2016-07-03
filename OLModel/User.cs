using System;
using System.Collections.Generic;
using System.IO;

namespace OLModel
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }

        public User(string name) : this(null, name) { }

        public User(string userID, string name)
        {
            if (userID == null)
                userID = getNewUserId().ToString();
            this.UserID = userID;
            this.Name = name;
        }
    
        private int getNewUserId()
        {
            // TODO: SELECT max(studentID AS INTEGER) FROM users;
            int i = 1000;
            while (true)
            {
                bool idAlreadInUse = false;
                foreach (User u in Model.Instance.Users)
                {
                    idAlreadInUse |= u.UserID.Equals(i.ToString());
                }
                if (idAlreadInUse)
                    i++; 
                else break;
            }
            return i;
        }

        public void BuyProducts(string ProductID, int amountBought)
        {
            var t = new Transaction(UserID, ProductID, amountBought);
            Model.Instance.Transactions.Add(t);
        }

        public override String ToString()
        {
            return String.Format("{0} ({1})", Name, UserID);
        }
    }
}