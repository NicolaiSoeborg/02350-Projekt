using OLModel;
using OLProgram.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows;
using System.Security.Cryptography;

namespace OLProgram.ViewModel
{
    public class AdminVM : BaseVM
    {
        public string TxtAdminPassword { get; set; }
        public static Window _adminLoginWindow = null;

        // Global commands for Admins
        public RelayCommand CloseApplicationCommand { get; }
        public RelayCommand AdminLoginCommand { get; }

        // Admin Commands for UsersVM
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

        // For load, Save and new 
        public DialogVM dialogVM { get; set; }

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

            dialogVM = new DialogVM();
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
            if(selectedUser != null)
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
            string path = dialogVM.ShowOpen();
            if (path != null)
            {
                // Load Users and Products from xml file
                Data DataToLoad = await SerializerXML.Instance.AsyncDeserializeFromFile(path);

                // Clear current useres and add the loaded users
                Users.Clear();
                DataToLoad.Users.ForEach(x => Users.Add(x));
                Products.Clear();
                DataToLoad.Products.ForEach(x => Products.Add(x));

                Log.Add(getTimeStamp(DateTime.Now) + " - Existing data loaded");
            }
        }

        private void SaveCurrentData()
        {
            string path = dialogVM.ShowSave();
            if (path != null)
            {
                Data DataToSave = new Data() { Users = Users.ToList(), Products = Products.ToList() };
                SerializerXML.Instance.AsyncSerializeToFile(DataToSave, path);
                
            }
        }

        private void NewData()
        {
            if (dialogVM.ShowNew())
            {
                Users.Clear();
                Products.Clear();
                Log.Add(getTimeStamp(DateTime.Now) + " - Data was deleted");
                OLModel.User.UserIDCounter = 2000;
                OLModel.Product.ProductIdCounter = 0;
            }
        }

    }
}