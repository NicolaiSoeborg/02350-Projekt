using GalaSoft.MvvmLight.CommandWpf;
using OLProgram.Command;
using OLModel;
using System;
using System.Windows.Input;
using System.Windows;

namespace OLProgram.ViewModel
{
    public class BasketVM : BaseVM
    {
        public static Basket Basket { get; set; }
        public string HelloTxtUsername { get { return loggedInUser == null ? "NoUserLoggedIn" : String.Format("Velkommen {0}!", loggedInUser.Name); } }
        public static String inputForBasket { get; set; }
        public static inputHandler inputBasket { get; set; }

        public ICommand AddProductToBasketCommand { get; }
        public ICommand DecreaseBasketItemCommand { get; }
        public ICommand DeleteBasketItemCommand { get; }
        public ICommand ClearBasketCommand { get; }
        public RelayCommand CheckOutCommand { get; }
        public RelayCommand<String> EnterCommand { get; }
        public RelayCommand<int> writeInputCommand { get; }
        public RelayCommand homeCommand { get; }

        public BasketVM()
        {
            if (Basket == null) Basket = new Basket();
            if (inputBasket == null) inputBasket = new inputHandler();
            inputBasket.inputGetSetter = "";

            // Commands to access from UI:
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DecreaseBasketItemCommand = new RelayCommand<Product>(DecreaseBasketItem);
            DeleteBasketItemCommand = new RelayCommand<Product>(DeleteBasketItem);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            CheckOutCommand = new RelayCommand(CheckOutBasket);
            EnterCommand = new RelayCommand<string>(enterInput);
            writeInputCommand = new RelayCommand<int>(writeInput);
            homeCommand = new RelayCommand(homeButton);

        }

        public void writeInput(int input)
        {
            inputBasket.inputGetSetter += input.ToString();
            RaisePropertyChanged("inputGetSetter");
        }

        public void enterInput(String input)
        {            
            if (input != null)
            inputBasket.inputGetSetter = string.Empty;
            RaisePropertyChanged("inputGetSetter");
            {

                foreach(Product product in Products)
                {
                    if (input.Equals(product.ProductId))
                    {
                        undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, 1));
                        return;
                    }
                }
                int userID;
                
                if(int.TryParse(input, out userID))
                {
                    foreach (User user in Users)
                    {
                        if (user.UserID == userID)
                        {
                            CheckOutBasket();
                            return;
                        }
                    }
                }
            }
        }

        private void CheckOutBasket()
        {

            foreach (BasketItem basketItem in Basket.BasketItems) {

                Users[Users.IndexOf(loggedInUser)].BuyProducts(basketItem.ProductId, basketItem.Count);
                Log.Add(getTimeStamp(DateTime.Now) + " - " + loggedInUser.ToString() + " Bought " + basketItem.Count + " of productID " + basketItem.ProductId);
                LogForUsers.Add(getTimeStamp(DateTime.Now) + " - " + loggedInUser.ToString() + " Bought " + basketItem.Count + " of productID " + basketItem.ProductId);

                // Virkelig ineffektivt
                foreach (Product product in Products)
                {
                    if (product.ProductId == basketItem.ProductId)
                    {
                        product.Stock -= basketItem.Count;
                        product.Bought += basketItem.Count;
                    }
                }
            }

            loggedInUser = null;
            new ClearBasketCommand(Basket).Execute();
            undoRedoController.ClearUndoRedoStacks();
            MainWindow.Content = new View.LoginUC();
        }

        private void homeButton()
        {
            loggedInUser = null;
            new ClearBasketCommand(Basket).Execute();
            undoRedoController.ClearUndoRedoStacks();
            MainWindow.Content = new View.LoginUC();
        }

        private void AddProductToBasket(Product product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, 1));
        }

        private void DecreaseBasketItem(Product product)
        {
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, -1));
        }

        private void DeleteBasketItem(Product product)
        {
            int count = Basket.getCount(product);
            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, -count));
        }

        private void ClearBasket()
        {
            undoRedoController.AddAndExecute(new ClearBasketCommand(Basket));
        }
    }

}