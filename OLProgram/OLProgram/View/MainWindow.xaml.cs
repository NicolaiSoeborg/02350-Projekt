using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OLModel;

namespace OLProgram.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(LoginClick));
        }

        private void LoginClick(object o, RoutedEventArgs e)
        {
            var ButtonEvent = e.OriginalSource as Button;
            if (ButtonEvent != null)
                switch (ButtonEvent.Name)
                {
                    case "LoginButton":
                        this.Content = new UserWindow();
                        e.Handled = true;
                        break;
                    case "CheckOutButton":
                        this.Content = new Login();
                        e.Handled = true;
                        break;
                    case "AdminButton":
                        this.Content = new AdminUC();
                        e.Handled = true;
                        break;
                }

        }
    }
}