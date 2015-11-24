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
using System.Windows;
using System.Windows.Input;

namespace OLProgram.ViewModel
{
    /**
      * BaseVM extender "ViewModelBase" som foretager "MvvmLight" magi.
      * Alle ViewModels skal extende denne "BaseVM" (som derved laver MvvmLight magi).
      * Skal data deles mellem ViewModels, så kan denne klasse bruges,
      * dog skal denne model ikke indeholde alt mulig crap, flyt selve logikken til den relevante ViewModelen.
      * (delte variabler skal naturligvis være static)
    **/
    public class BaseVM : ViewModelBase
    {
        // Undo/Redo controller + commands that the UI can be bound to.
        public UndoRedoController undoRedoController = UndoRedoController.Instance;

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        // Add Users
        public ICommand AddUserCommand { get; }



        // Liste over brugere og produkter
        public static ObservableCollection<User> Users { get; set; }
        public static ObservableCollection<Product> Products { get; set; }
        public static User loggedInUser { get; set; }

        // Ref til MainWindow, brug MainWindow.Content = new View.ViewUC(); for at skrifte UC.
        public static Window MainWindow { get; set; }

        public BaseVM()
        {
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);
            AddUserCommand = new RelayCommand(NewUser);

            // TODO: Skal ikke være med i den endelige version (load fra OLModel?):
            if (Products == null) Products = new ObservableCollection<Product>() { new Product("Grøn Tuborg", "../Images/tuborg.png"), new Product("Guld Tuborg", "../Images/guldtuborg.png"), new Product("Grøn Tuborg2", "../Images/tuborg.png"), new Product("Grøn Tuborg3", "../Images/tuborg.png"), new Product("Guld Tuborg2", "../Images/guldtuborg.png") };
            if (Users == null) Users = new ObservableCollection<User>() { new User(1001, "Rasmus"), new User(1002, "Nicolai"), new User(1003, "Silas"), new User(1004, "Greven") };
        }

        private void NewUser()
        {

        }

    }
}