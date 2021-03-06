﻿using OLModel;
using OLProgram.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OLProgram.ViewModel
{
    public class AdminVM : BaseVM
    {
        public string TxtAdminPassword { get; set; }
        public static Window _adminLoginWindow = null;
        public List<String> AdminLog { get { return Model.Instance.AdminLog; } }

        // Logs:
        public static List<String> LogForX { get; set; }
        public RelayCommand ShowFullUserLogCommand { get; }
        public RelayCommand ShowFullAdminLogCommand { get; }
        public RelayCommand ShowFullTransactionLogCommand { get; }

        // Global commands for Admins
        public RelayCommand CloseApplicationCommand { get; }
        public RelayCommand AdminLoginCommand { get; } // Benyttes gennem AdminLoginWindow "Login" knappen

        // Used for changing admin password
        public string TxtNewAdminPassword { get; set; }
        public RelayCommand AdminChangePasswordCommand { get; }
        public RelayCommand ShowChangePasswordCommand { get; }

        // Admin Commands for Users
        public RelayCommand<User> DeleteSelectedUserCommand { get; }
        public RelayCommand<User> ShowLogForUser { get; }
        public RelayCommand AddNewUserCommand { get; }

        //Admin Commands for Products
        public RelayCommand<Product> DeleteSelectedProductCommand { get; }
        public RelayCommand<Product> ShowLogForProduct { get; }
        public RelayCommand AddNewProductCommand { get; }

        // Admin commands for Load, Save and New
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand LoadDataCommand { get; }
        public RelayCommand NewDataCommand { get; }
        public RelayCommand GenerateBillCommand { get; }
        
        // For load, Save and new 
        public DialogHelper dialogHelper { get; } = new DialogHelper();

        private void ShowAdminLoginGUI()
        {
           MainWindow.Content = new View.AdminUC();
        }

        public AdminVM()
        {
            // Commands:
            AdminLoginCommand = new RelayCommand(DoAdminLogin);
            CloseApplicationCommand = new RelayCommand(CloseApplication);
            AdminChangePasswordCommand = new RelayCommand(ChangeAdminPassword);
            ShowChangePasswordCommand = new RelayCommand(ShowChangePassword);

            // Admin Commands for users
            DeleteSelectedUserCommand = new RelayCommand<User>(DeleteSelectedUser);
            AddNewUserCommand = new RelayCommand(AddNewUser);
            ShowLogForUser = new RelayCommand<User>(UserShowLog);

            // Admin Commands for products
            DeleteSelectedProductCommand = new RelayCommand<Product>(DeleteSelectedProduct);
            AddNewProductCommand = new RelayCommand(AddNewProduct);
            ShowLogForProduct = new RelayCommand<Product>(ProductShowLog);

            // Log commands:
            ShowFullUserLogCommand = new RelayCommand(ShowUsersLog);
            ShowFullAdminLogCommand = new RelayCommand(ShowAdminLog);
            ShowFullTransactionLogCommand = new RelayCommand(ShowTransactionsLog);

            // Admin commands for Load, Save and New
            SaveDataCommand = new RelayCommand(SaveCurrentData);
            LoadDataCommand = new RelayCommand(LoadExistingData);
            NewDataCommand = new RelayCommand(NewData);
            GenerateBillCommand = new RelayCommand(GenerateBill);
        }

        private void ShowTransactionsLog()
        {
            LogForX = Model.Instance.Transactions
                .Select(t => String.Format("{0} bought {1} x {2}.", t.studentId, t.amount, t.productId))
                .ToList();
            (new View.ShowLog()).ShowDialog();
        }

        private void ShowUsersLog()
        {
            LogForX = Model.Instance.UserLog.ToList();
            (new View.ShowLog()).ShowDialog();
        }

        private void ShowAdminLog()
        {
            LogForX = Model.Instance.AdminLog.ToList();
            (new View.ShowLog()).ShowDialog();
        }


        private void ProductShowLog(Product selectedProduct)
        {
            if (selectedProduct == null) return;
            LogForX = Model.Instance.Transactions
                .Where(t => t.productId.Equals(selectedProduct.ProductId))
                .Select(t => String.Format("{0} bought {1} x {2}.", t.studentId, t.amount, t.productId))
                .ToList();
            (new View.ShowLog()).ShowDialog();
        }

        private void UserShowLog(User selectedUser)
        {
            if (selectedUser == null) return;
            LogForX = Model.Instance.Transactions
                .Where(t => t.studentId.Equals(selectedUser.UserID))
                .Select(t => String.Format("{0} bought {1} x {2}.", selectedUser.Name, t.amount, t.productId))
                .ToList();
            (new View.ShowLog()).ShowDialog();
        }

        private void LoadExistingData()
        {
            throw new NotImplementedException();
        }

        private void SaveCurrentData()
        {
            throw new NotImplementedException();
        }

        private void GenerateBill()
        {
            string path = dialogHelper.ShowSaveBill();
            if (path != null)
            {
                if (File.Exists(path) && MessageBox.Show("File already exists! Overwrite?", "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                    return;

                // TODO: Svind

                /*     ID   NAME   PROD1  PROD2   SUM
                      1337  Admin    3      2      =[YOU DO THE MATH]
                      1001  Alice    1      2   
                      1002  Bob      0      0   
                      1003  Charlie  0      2   
                */
                StringBuilder csv = new StringBuilder();

                // Header
                csv.Append("ID, Name, ");
                foreach (Product p in Model.Instance.Products)
                    csv.AppendFormat("{0}, ", trimCSV(p.ProductName));
                csv.Append("\r\n");

                foreach (User u in Model.Instance.Users)
                {
                    csv.AppendFormat("{0}, {1}, ", trimCSV(u.UserID), trimCSV(u.Name));
                    foreach (Product p in Model.Instance.Products)
                    {
                        // How many product has user bought?
                        int amount = Model.Instance.Transactions
                            .Where(t => u.UserID.Equals(t.studentId)
                                    && p.ProductId.Equals(t.productId))
                            .Sum(t => t.amount);

                        int toPay = amount * p.Price; // TODO: Add svind
                        csv.AppendFormat("{0}, ", toPay);
                    }
                    csv.Append("\r\n");
                }
                
                File.WriteAllText(path, csv.ToString());
                OLModel.Helpers.AdminLog("Bill has been saved at \"{0}\".", path);
            }
        }

        private string trimCSV(string csv)
        {
            return csv.Replace(@"\", @"\\").Replace(",", @"\,");
        }

        private void DeleteSelectedProduct(Product selectedProduct)
        {
            if (selectedProduct != null)
            {
                var response = MessageBox.Show("Do you really want to delete " + selectedProduct.ProductName + "?", "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                {
                    Model.Instance.Products.Remove(selectedProduct);
                    OLModel.Helpers.AdminLog("Product {0} was deleted.", selectedProduct);
                }
            }
        }

        private void DeleteSelectedUser(User selectedUser)
        {
            if (selectedUser != null)
            {
                var response = MessageBox.Show("Do you really want to delete user " + selectedUser.ToString(), "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                {
                    OLModel.Helpers.AdminLog("User {0} was deleted.", selectedUser);
                    Model.Instance.Users.Remove(selectedUser);
                }
            }
        }

        private void AddNewProduct()
        {
            Model.Instance.Products.Add(new Product("New product", 0));
            OLModel.Helpers.AdminLog("New product added.");
        }

        private void AddNewUser()
        {
            Model.Instance.Users.Add(new User("New user"));
            OLModel.Helpers.AdminLog("New user added.");
        }

        private void CloseApplication()
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                Environment.Exit(0); // TODO: More graceful shutdown? Call OLModel.save() first ?
        }

        private string HashPassword(string pwd)
        {
            if (String.IsNullOrEmpty(TxtAdminPassword)) return null;
            byte[] salt = new byte[] { 0x4F, 0x4C, 0x50, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D }; // (must be 8 bytes or larger)
            int iterations = 10000; // default is 1000
            try
            {
                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(TxtAdminPassword, salt, iterations); // PBKDF2
                return Convert.ToBase64String(hash.GetBytes(32));
            }
            catch { MessageBox.Show("Error while hashing password!"); }
            return null; // "null" => en fejl ikke skal give et "korrekt" formatteret password hash
        }

        private void DoAdminLogin()
        {
            string inputPassword = HashPassword(TxtAdminPassword);
            string adminpw = Properties.Settings.Default.adminpwd;
            if (_adminLoginWindow != null
                && inputPassword != null && inputPassword.Equals(adminpw))
            {
                // Login korrekt!
                TxtAdminPassword = ""; // Clear password
                MainWindow.Content = new View.AdminUC();
                _adminLoginWindow.Close();
            }
            //else TxtAdminPassword = ""; // TODO: Clear password field
        }

        private void ShowChangePassword()
        {
            (new View.AdminChangePasswordWindow()).ShowDialog();
        }

        private void ChangeAdminPassword()
        {
            string oldPassHash = HashPassword(TxtAdminPassword);
            string newPassHash = HashPassword(TxtNewAdminPassword);
            string adminpw = Properties.Settings.Default.adminpwd;
            if (newPassHash != null &&
                oldPassHash != null && oldPassHash.Equals(adminpw))
            {
                // Login korrekt!
                Properties.Settings.Default.adminpwd = newPassHash;
                Properties.Settings.Default.Save(); // This doesn't work in "debug mode", does it work in "release mode"?
                TxtAdminPassword = ""; TxtNewAdminPassword = "";
                MessageBox.Show("Password changed.");
            }
            else
                MessageBox.Show("Wrong password!");
        }

        private void NewData()
        {
            var response = MessageBox.Show("Are you sure?", "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes && dialogHelper.ShowNew())
            {
                Model.Instance.Users.Clear();
                Model.Instance.Products.Clear();
                OLModel.Helpers.AdminLog("Data was deleted.");
            }
        }

    }
}
