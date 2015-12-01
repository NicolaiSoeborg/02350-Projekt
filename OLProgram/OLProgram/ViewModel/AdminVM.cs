using OLModel;
using OLProgram.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace OLProgram.ViewModel
{
    public class AdminVM : BaseVM
    {
        public string TxtAdminPassword { get; set; }
        public static Window _adminLoginWindow = null;
        public static string billPath { get; set; }

        // Global commands for Admins
        public RelayCommand CloseApplicationCommand { get; }
        public RelayCommand AdminLoginCommand { get; } // Benyttes gennem AdminLoginWindow "Login" knappen

        // Admin Commands for Users
        public RelayCommand<User> DeleteSelectedUserCommand { get; }
        public RelayCommand<User> SaveCurrentUserInformationCommand { get; }
        public RelayCommand AddNewUserCommand { get; }

        //Admin Commands for Products
        public RelayCommand AddProductToGlobalCommand { get; }
        public RelayCommand<Product> DeleteSelectedProductCommand { get; }
        public RelayCommand AddNewProductCommand { get; }

        // Admin commands for Load, Save and New
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand LoadDataCommand { get; }
        public RelayCommand NewDataCommand { get; }
        public RelayCommand GenerateBillCommand { get; }
        
        // ICommand as we want to block "(Re)Save" bill untill a GenerateBill has occured
        public System.Windows.Input.ICommand ReSaveBillCommand { get; } 

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

            // Admin Commands for UsersVM
            DeleteSelectedUserCommand = new RelayCommand<User> (DeleteSelectedUser);
            AddNewUserCommand = new RelayCommand(AddNewUser);
            //SaveCurrentUserInformationCommand = new RelayCommand<User, String>(saveCurrentUserInformation);

            // Admin Commands for ProductsVM
            DeleteSelectedProductCommand = new RelayCommand<Product>(DeleteSelectedProduct);
            AddNewProductCommand = new RelayCommand(AddNewProduct);

            // Admin commands for Load, Save and New
            SaveDataCommand = new RelayCommand(SaveCurrentData);
            LoadDataCommand = new RelayCommand(LoadExistingData);
            NewDataCommand = new RelayCommand(NewData);
            GenerateBillCommand = new RelayCommand(GenerateBill); // TODO
            ReSaveBillCommand = new Command.ReSaveBillCommand();    
        }

        private void GenerateBill()
        {
            string path = dialogHelper.ShowSaveBill();
            if (path != null)
            {
                billPath = path;
                ReSaveBillCommand.Execute(null);
            }
            RaisePropertyChanged(() => ReSaveBillCommand);
        }

        private void DeleteSelectedProduct(Product selectedProduct)
        {
            if (selectedProduct != null)
            {
                var response = MessageBox.Show("Do you really want to delete Product " + selectedProduct.ProductName, "Deleting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                   
                Log.Add(getTimeStamp(DateTime.Now) + " - Product " + selectedProduct.ProductName + " was deleted. ");
                Products.Remove(selectedProduct);
            }
        }

        private void AddNewProduct()
        {
            Products.Add(new Product("New product"));
            Log.Add(getTimeStamp(DateTime.Now) + " - New Product added to Products");
        }

        private void AddNewUser()
        {
            Users.Add(new User("New user"));
            Log.Add(getTimeStamp(DateTime.Now) + " - New User added to Users");
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
            if (_adminLoginWindow != null && inputPassword != null)
            {
                string adminpw = Properties.Settings.Default.adminpwd;
                if (inputPassword.Equals(adminpw))
                {
                    // Login korrekt!
                    TxtAdminPassword = ""; // Clear saved password
                    MainWindow.Content = new View.AdminUC();
                    _adminLoginWindow.Close();
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
                    Log.Add(getTimeStamp(DateTime.Now) + " - User " + selectedUser.ToString() + " was deleted. ");
                    Users.Remove(selectedUser);
                }
            }
        }

        private async void LoadExistingData()
        {
            string path = dialogHelper.ShowOpen();
            if (path != null)
            {
                // Load Users and Products from xml file
                Data DataToLoad = await SerializerXML.Instance.AsyncDeserializeFromFile(path);

                // Clear current useres and add the loaded users
                Users.Clear();
                DataToLoad.Users.ForEach(x => Users.Add(x));
                Products.Clear();
                DataToLoad.Products.ForEach(x => Products.Add(x));

                // Do the Logs
                LogForUsers.Clear();
                DataToLoad.UserLog.ForEach(x => LogForUsers.Add(x));

                DataToLoad.AdminLog.ForEach(x => Log.Add(x));

                // Load each users dictionary 
                int DictionaryCounter = 0;
                int Counter = 0;
                int currentIndex = 0;
                foreach (User user in Users)
                {
                    Counter = 0;
                    foreach(String temp in DataToLoad.ProductKeys)
                    {
                        if (DataToLoad.ProductsForEachUser[currentIndex] != 0)
                        {
                            user.ProductsBought.Add(DataToLoad.ProductKeys[DictionaryCounter], DataToLoad.AmountBought[DictionaryCounter]);
                            DictionaryCounter++;
                            Counter++;

                            if (DataToLoad.ProductsForEachUser[currentIndex] == Counter)
                            {
                                currentIndex++;
                                break;
                            }
                        }
                        else
                        {
                            currentIndex++;
                            break;
                        }
                  }
             }
                    Log.Add(getTimeStamp(DateTime.Now) + " - Existing data loaded");

            }
        }

        private void SaveCurrentData()
        {
            string path = dialogHelper.ShowSave();
            if (path != null)
            {
                List<String> tempProductKeys = new List<string>();
                List<int> tempAmountBought = new List<int>();
                List<int> tempProductsForEachUser = new List<int>();
                int Counter;
                foreach(User user in Users)
                {
                    Counter = 0;
                    
                    foreach(var item in user.ProductsBought)
                    {
                        tempProductKeys.Add(item.Key);
                        tempAmountBought.Add(item.Value);
                        Counter++;
                    }
                    tempProductsForEachUser.Add(Counter);
                }

                Data DataToSave = new Data() { Users = Users.ToList(),
                                                Products = Products.ToList(), AmountBought = tempAmountBought,
                                                ProductKeys = tempProductKeys, ProductsForEachUser = tempProductsForEachUser,
                                                AdminLog = Log.ToList(), UserLog = LogForUsers.ToList()};
                SerializerXML.Instance.AsyncSerializeToFile(DataToSave, path);
            }
        }

        private void NewData()
        {
            if (dialogHelper.ShowNew())
            {
                Users.Clear();
                Products.Clear();
                LogForUsers.Clear();
                Log.Add(getTimeStamp(DateTime.Now) + " - Data was deleted");
                OLModel.User.UserIDCounter = 2000;
                OLModel.Product.ProductIdCounter = 0;
            }
        }

    }
}