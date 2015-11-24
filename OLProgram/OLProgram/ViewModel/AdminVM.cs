using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLProgram.OLModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        // Admin Commands for AddUserUC
        public RelayCommand<User> DeleteSelectedUserCommand { get; }
        public RelayCommand<User> SaveCurrentUserInformationCommand { get; }
        public RelayCommand addNewUserCommand { get; }

        public ICommand ChangeToAdminCommand { get { return new RelayCommand(ShowAdminLoginGUI); } }

        private void ShowAdminLoginGUI()
        {
           MainWindow.Content = new View.AdminUC();
        }

        public AdminVM()
        {
            // Intet skal initialiseres når Admin ViewModel laves (ud over commands)?

            // Commands:
            AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);
            //AddUserCommand = new RelayCommand(AddUser);
            CloseApplicationCommand = new RelayCommand(CloseApplication);

            // Admin Commands for AddUserUC
            DeleteSelectedUserCommand = new RelayCommand<User> (DeleteSelectedUser);
            addNewUserCommand = new RelayCommand(addNewUser);
            

            //SaveCurrentUserInformationCommand = new RelayCommand<User, String>(saveCurrentUserInformation);


        }

        private void addNewUser()
        {
            Users.Add(new User(-1, "new User"));
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
        
        private void DeleteSelectedUser(User selectedUser)
        {
            if(selectedUser != null)
            {
                var response = MessageBox.Show("Do you really want to delete user " + selectedUser.ToString(), "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                    Users.Remove(selectedUser);
            }
            else
            {
                MessageBox.Show("No User Selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

    }
}