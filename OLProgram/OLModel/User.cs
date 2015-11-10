using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.OLModel
{
    public class User
    {
        private static int UserIdCounter = 0;

        public int UserID { get; set; }

        public string Name { get; set; }

        public Dictionary<int,int> ProductsBought = new Dictionary<int,int>();

        public User() : this(UserIdCounter, "")
        {
            UserIdCounter++;
        }

        public User(int UserID, string Name)
        {
            this.UserID = UserID;
            this.Name = Name;
        }

        // Product is added to the users list and bought is incremented 
        public void ManageProducts(Product product, int purchase)
        {
            
            if (ProductsBought.ContainsKey(product.ProductId))
            {
                ProductsBought[product.ProductId] += purchase;
            }
            else
            {
                ProductsBought.Add(product.ProductId, purchase);
            }
            product.Bought += purchase;
        }
    }
    /*
    public class StudyProgramme
    {
        private static int ProgrammeIdCounter = 0;

        public int ProgrammeId { get; }

        public string ProgrammeName { get; set; }
        public Boolean IsBEngProgramme { get; set; } //diplom?

        public StudyProgramme(String ProgrammeName, Boolean IsBEngProgramme)
        {
            this.ProgrammeId = ProgrammeIdCounter++;
            this.ProgrammeName = ProgrammeName;
            this.IsBEngProgramme = IsBEngProgramme;
        }
    }*/
}
