using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
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
    public class UserVM : BaseVM
    {
        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        public Basket Basket { get; set; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        // Bindings to UI
        public ICommand AddProductToBasketCommand { get; }


        public UserVM()
        {
            Basket = new Basket();

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            AddProductToBasketCommand = new RelayCommand(AddProductToBasket);
        }
       
        private void AddProductToBasket()
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, new Product(), 1));
        }
    }
}