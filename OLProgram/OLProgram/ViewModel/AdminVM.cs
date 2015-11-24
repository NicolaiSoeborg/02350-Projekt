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
    public class AdminVM : BaseVM
    {
        public RelayCommand AddProductToGlobalCommand { get; }
       // public RelayCommand AddUserCommand { get; }
        public RelayCommand CloseApplicationCommand { get; }

        public AdminVM()
        {
            // Intet skal initialiseres når Admin ViewModel laves (ud over commands)?
            
            // Commands:
            AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);
            //AddUserCommand = new RelayCommand(AddUser);
            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        private void AddProductToGlobal() { } // TODO

        private void AddUser()
        {
            undoRedoController.AddAndExecute(new AddUserCommand(Users, new User(123, "TODO")));
        }

        private void CloseApplication()
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Environment.Exit(0); // TODO: More graceful shutdown? Call OLModel.save() first ?
        }

    }
}