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
        public RelayCommand EnterCommand { get; }
        public RelayCommand<KeyEventArgs> writeInputCommand { get; }
        public RelayCommand homeCommand { get; }

        public BasketVM()
        {
            if (Basket == null) Basket = new Basket();
            if (inputBasket == null)
            {
                inputBasket = new inputHandler();
                inputBasket.inputGetSetter = "";
            }
            // Commands to access from UI:
            inputForBasket = "";
            AddProductToBasketCommand = new RelayCommand<Product>(AddProductToBasket);
            DecreaseBasketItemCommand = new RelayCommand<Product>(DecreaseBasketItem);
            DeleteBasketItemCommand = new RelayCommand<Product>(DeleteBasketItem);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            CheckOutCommand = new RelayCommand(CheckOutBasket);
            EnterCommand = new RelayCommand(enterInput);
            writeInputCommand = new RelayCommand<KeyEventArgs>(writeInput);
            homeCommand = new RelayCommand(homeButton);

        }

        private void writeInput(KeyEventArgs e)
        {


            if (e.Key == Key.D0)
            {
                inputForBasket += "0";
            }
            else if (e.Key == Key.D1)
            {
                inputForBasket += "1";
            }
            else if (e.Key == Key.D2)
            {
                inputForBasket += "2";
            }
            else if (e.Key == Key.D3)
            {
                inputForBasket += "3";
            }
            else if (e.Key == Key.D4)
            {
                inputForBasket += "4";
            }
            else if (e.Key == Key.D5)
            {
                inputForBasket += "5";
            }
            else if (e.Key == Key.D6)
            {
                inputForBasket += "6";
            }
            else if (e.Key == Key.D7)
            {
                inputForBasket += "7";
            }
            else if (e.Key == Key.D8)
            {
                inputForBasket += "8";
            }
            else if (e.Key == Key.D9)
            {
                inputForBasket += "9";
            }
            else if(e.Key == Key.Enter)
            {
                enterInput();
            }
        }

        // Kræver optimeringer
        public void enterInput()
        {            
            if (inputForBasket != null)
            { 
                if(inputForBasket.Length == 4)
                {
                    int userID;

                    if (int.TryParse(inputForBasket, out userID))
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
                } else
                {

                    foreach (Product product in Products)
                    {
                        if (inputForBasket.Equals(product.ProductId))
                        {
                            undoRedoController.AddAndExecute(new AddProductToBasketCommand(Basket, product, 1));
                            inputForBasket = string.Empty;
                            return;
                        }
                    }
                }
            }
            inputForBasket = string.Empty;

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