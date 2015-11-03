using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Commands;
using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        public ObservableCollection<String> users { get; set; }
        public ObservableCollection<Item> Items { get; set; }

        public ICommand AddItemCommand { get; }

        private void AddItem()
        {
            new AddItemCommand(Items, new Item()).Execute();
        }
    }
}
