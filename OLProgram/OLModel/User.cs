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

        public int UserID { get; }

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

        public void ManageProducts(int ProductID, int Purchase)
        {
            if (ProductsBought.ContainsKey(ProductID))
            {
                ProductsBought[ProductID] += Purchase;
            }
            else
            {
                ProductsBought.Add(ProductID, Purchase);
            }
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
