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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _billPath != null && File.Exists(_billPath);
        }

        public void Execute(object parameter)
        {
            StringBuilder csv = new StringBuilder();

            var result = (
                from user in Users
                select new {
                    name = user.Name,
                    sum = calcSum(user.ProductsBought)
                }
            ).ToList();
            
            foreach (var user in result)
                csv.AppendFormat("{0},{1}\r\n", trimCSV(user.name), user.sum);
            
            File.WriteAllText(_billPath, csv.ToString());
        }

        private string trimCSV(string csv)
        {
            return csv.Replace(@"\", @"\\").Replace(",", @"\"); // TODO: Much better!
        }

        private int calcSum(Dictionary<int, int> productsBought) // productsBought<Product.ProductId, Amount>, TODO: Korrekt?
        {
            int sum = 0;
            foreach (var p in productsBought) {
                Product prod = Products.First(x => x.ProductId.Equals(p.Key));
                sum += prod.Price * p.Value; // TODO: Regner ikke svind med!
            }
            return sum;
        }
    }
}
