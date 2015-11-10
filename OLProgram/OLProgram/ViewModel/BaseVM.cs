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
    /**
      * Alle ViewModels skal extende denne "BaseVM".
      * BaseVM extender "ViewModelBase" som foretager "MvvmLight" magi.
      * Skal data deles mellem ViewModels, så kan denne klasse bruges,
      * dog skal denne model ikke indeholde alt mulig crap, flyt selve logikken til ViewModelen.
    **/
    public class BaseVM : ViewModelBase
    {
        // Undo/Redo controller + commands that the UI can be bound to.
        public UndoRedoController undoRedoController = UndoRedoController.Instance;
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        // Liste over brugere og produkter
        public static ObservableCollection<User> Users { get; set; }
        public static ObservableCollection<Product> Products { get; set; }

        // Vigtigt at field er static, så værdien deles over alle klasser der extender BaseVM
        // I Undo/Redo er det vigtigt at den IKKE er static, så vi får nye Undo/Redo for hver klasse der extender BaseVM
        private static string _username = ""; // From LoginUC
        public string TxtUsername { get { return _username; } set { _username = value; } }
        public string HelloTxtUsername { get { return String.Format("Velkommen {0}!", _username); } }

        public BaseVM()
        {
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);
        }
        
    }
}
