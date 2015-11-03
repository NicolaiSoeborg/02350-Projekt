using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Commands
{
    public class AddItemCommand
    {
        #region Fields

        private ObservableCollection<Item> items;
        private Item item;
        public ICommand AddShapeCommand { get; }

        #endregion

        #region Constructor 

        public AddItemCommand(ObservableCollection<Item> _items, Item _item)
        {
            items = _items;
            item = _item;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            items.Add(item);
        }

        public void UnExecute()
        {
            items.Remove(item);
        }

        #endregion



    }
}
