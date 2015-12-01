using OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Command
{
    public class ReSaveBillCommand : ICommand
    {
        private string _billPath { get { return OLProgram.ViewModel.AdminVM.billPath; } }
        public ObservableCollection<User> Users { get { return OLProgram.ViewModel.BaseVM.Users; } }
        public ObservableCollection<Product> Products { get { return OLProgram.ViewModel.BaseVM.Products; } }

#pragma warning disable
        public event EventHandler CanExecuteChanged;
#pragma warning restore

        public bool CanExecute(object parameter)
        {
            return _billPath != null && File.Exists(_billPath);
        }

        public void Execute(object parameter)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendFormat("{0}\t{1}\r\n", "Name", "Bought Total");

            var result = (
                from user in Users
                select new {
                    name = user.Name,
                    sum = calcSum(user.ProductsBought)
                }
            );
            
            foreach (var user in result)
                csv.AppendFormat("{0};{1}\r\n", trimCSV(user.name), user.sum);
            
            File.WriteAllText(_billPath, csv.ToString());
        }

        private string trimCSV(string csv)
        {
            return csv.Replace(@"\", @"\\").Replace(",", @"\"); // TODO: Much better!
        }

        private int calcSum(Dictionary<string, int> productsBought) // productsBought<Product.ProductId, Amount>
        {
            int sum = 0;
            int svindPerUser = 0; // TODO: Regner ikke svind med!

            foreach (var p in productsBought) {
                Product prod = Products.First(x => x.ProductId.Equals(p.Key));
                System.Windows.MessageBox.Show("Product " + prod.ProductName + " bought: " + prod.Bought);
                sum += prod.Price * p.Value; 
            }
            return sum + svindPerUser;
        }
    }
}
