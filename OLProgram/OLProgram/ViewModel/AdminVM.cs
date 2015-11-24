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
using System.Security.Cryptography;


namespace OLProgram.ViewModel
{
    public class AdminVM : BaseVM
    {
        private static string ADMIN_PASSWORD = "R4JrVIuzP2DEGtCkcrpX8bJUhOfmAow2wpH2XgOfL5o="; // "OLProgram"
        public string TxtAdminPassword {
            get { return ""; }
            set { if (value != null && TestAdminPassword(value)) MainWindow.Content = new View.AdminUC(); }
        }

        public RelayCommand AddProductToGlobalCommand { get; }
        public RelayCommand AddUserCommand { get; }
        public RelayCommand CloseApplicationCommand { get; }

        public AdminVM()
        {
            // Intet skal initialiseres når Admin ViewModel laves (ud over commands)?
            
            // Commands:
            AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);
            AddUserCommand = new RelayCommand(AddUser);
            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        private bool TestAdminPassword(string pwd)
        {
            try
            {
                byte[] salt = new byte[] { 0x4F, 0x4C, 0x50, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D }; // "OLProgram" (must be 8 bytes or larger)
                int iterations = 10000; // default is 1000
                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(pwd, salt, iterations); // PBKDF2

                // hash.Equals(byte[] sameBytes) => giver altid false ...
                if (Convert.ToBase64String(hash.GetBytes(32)).Equals(ADMIN_PASSWORD))
                    return true;
                
            }
            catch { MessageBox.Show("Fejl i TestAdminPassword"); }
            return false;
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