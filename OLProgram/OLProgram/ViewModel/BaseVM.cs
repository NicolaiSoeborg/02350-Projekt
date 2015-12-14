using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLModel;
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

        // Liste over brugere og produkter
        public static ObservableCollection<User> Users { get; set; }
        public static ObservableCollection<Product> Products { get; set; }
        public static ObservableCollection<String> Log { get; set; }
        public static ObservableCollection<String> LogForUsers { get; set; }
        public static User loggedInUser { get; set; }

        // Ref til MainWindow, brug MainWindow.Content = new View.ViewUC(); for at skrifte UC.
        public static Window MainWindow { get; set; }

        // Commands
        public ICommand ChangeToAdminCommand { get; }
        public ICommand ChangeToMainCommand { get; }

        public BaseVM()
        {

            // Standard værdier, hvis intet er defineret.
            if (Products == null) {
                Products = new ObservableCollection<Product>() { new Product("Grøn Tuborg", "../Images/tuborg.png"), new Product("Guld Tuborg", "../Images/guldtuborg.png"), new Product("Somersby", "../Images/somersby.png") };
                Product svaneke = new Product("Svaneke Grunge IPA", "../Images/svaneke.JPG");
                svaneke.ProductId = "5708429004221";
                Products.Add(svaneke);
            }
            if (Users == null) Users = new ObservableCollection<User>() { new User(1106, "Bjarne"), new User(1002, "Nicolai"), new User(1003, "Silas"), new User(1004, "Greven") };
            if (Log == null) Log = new ObservableCollection<string>() { };
            if (LogForUsers == null) LogForUsers = new ObservableCollection<string>() { };


            // Commands
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);
            ChangeToAdminCommand = new RelayCommand(ShowAdminLoginGUI);
            ChangeToMainCommand = new RelayCommand(ShowMainLoginGUI);
        }

        private static void ShowAdminLoginGUI()
        {
            (new View.AdminLoginWindow()).ShowDialog();
        }

        private static void ShowMainLoginGUI()
        {
            MainWindow.Content = new View.LoginUC();
        }

        // Bruges både i AdminVM og BasketVM, så defineres kun én gang her:
        public static String getTimeStamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}