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
        private static User loggedInUser = new User(0, ""); // From LoginUC
        public string TxtUsername { get { return loggedInUser.Name; } set { loggedInUser = new User(0, value); } } // TODO: fix setter
        public string HelloTxtUsername { get { return String.Format("Velkommen {0}!", loggedInUser.Name); } } // TODO: Kan flyttes til UserVM?

        public BaseVM()
        {
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            // TODO: Skal ikke være med i den endelige version (load fra OLModel?):
            Products = new ObservableCollection<Product>();
            Users = new ObservableCollection<User>() { new User(1001, "Rasmus"), new User(1002, "Nicolai"), new User(1003, "Silas"), new User(1004, "Greven") };
        }
        
    }
}