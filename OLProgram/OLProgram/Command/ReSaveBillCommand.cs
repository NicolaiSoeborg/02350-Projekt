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
            return _billPath != null;// && File.Exists(_billPath);
        }

        public void Execute(object parameter)
        {
            StringBuilder csv = new StringBuilder();
            

            /*csv.AppendFormat("{0},\t{1}", );

            Products.ElementAt(0);
                Users.Where(
                    x => 
                ).ToString()
            );*/

            
        }
    }
}
