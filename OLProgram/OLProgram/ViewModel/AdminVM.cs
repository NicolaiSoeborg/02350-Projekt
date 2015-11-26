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
using System.Security.Cryptography;


namespace OLProgram.ViewModel
{
    public class AdminVM : BaseVM
    {
        public string TxtAdminPassword {
            get { return ""; }
            set { if (value != null && TestAdminPassword(value)) MainWindow.Content = new View.AdminUC(); }
        }

        // Global commands for Admins
        public RelayCommand CloseApplicationCommand { get; }

        // Admin Commands for Users
        public RelayCommand<User> DeleteSelectedUserCommand { get; }
        public RelayCommand<User> SaveCurrentUserInformationCommand { get; }
        public RelayCommand addNewUserCommand { get; }

        //Admin Commands for Prodcuts
        public RelayCommand AddProductToGlobalCommand { get; }
        public RelayCommand<Product> DeleteSelectedProductCommand { get; }
        public RelayCommand AddNewProductCommand { get; }

        public RelayCommand ChangeToAdminCommand { get { return new RelayCommand(ShowAdminLoginGUI); } }

        private void ShowAdminLoginGUI()
        {
           MainWindow.Content = new View.AdminUC();
        }

        public AdminVM()
        {
            // Intet skal initialiseres når Admin ViewModel laves (ud over commands)?

            // Commands:
            AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);
            CloseApplicationCommand = new RelayCommand(CloseApplication);

            // Admin Commands for Users
            DeleteSelectedUserCommand = new RelayCommand<User> (DeleteSelectedUser);
            addNewUserCommand = new RelayCommand(addNewUser);

            // Admin Commands for Products
            DeleteSelectedProductCommand = new RelayCommand<Product>(DeleteSelectedProduct);
            AddNewProductCommand = new RelayCommand(AddNewProduct);



            //SaveCurrentUserInformationCommand = new RelayCommand<User, String>(saveCurrentUserInformation);


        }

        private void DeleteSelectedProduct(Product SelectedProduct)
        {
            if (SelectedProduct != null)
            {
                var response = MessageBox.Show("Do you really want to delete Product " + SelectedProduct.ToString(), "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                    Products.Remove(SelectedProduct);
            }
            else
            {
                MessageBox.Show("No product selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewProduct()
        {
            Products.Add(new Product("New product"));
        }

        private void addNewUser()
        {
            Users.Add(new User("New user"));
        }

        private bool TestAdminPassword(string pwd)
        {
            try
            {
                byte[] salt = new byte[] { 0x4F, 0x4C, 0x50, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D }; // (must be 8 bytes or larger)
                int iterations = 10000; // default is 1000
                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(pwd, salt, iterations); // PBKDF2

                // hash.Equals(byte[] sameBytes) => giver altid false ...
                string adminpw = Properties.Settings.Default.adminpwd;
                if (Convert.ToBase64String(hash.GetBytes(32)).Equals(adminpw))
                    return true;
                
            }
            catch { MessageBox.Show("Fejl i TestAdminPassword"); }
            return false;
        }

        private void AddProductToGlobal() { } // TODO

        private void AddUser()
        {
            undoRedoController.AddAndExecute(new AddUserCommand(Users, new User("TODO")));
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
                MessageBox.Show("No user selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

    }
}