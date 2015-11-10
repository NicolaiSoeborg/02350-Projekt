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
using System.Windows.Shapes;

namespace OLProgram.View
{
    /// <summary>
    /// Interaction logic for AdminScreen.xaml
    /// </summary>
    public partial class AdminScreen : UserControl
    {
        public AdminScreen()
        {
            InitializeComponent();
        }


        private void MenuItemClick(object o, RoutedEventArgs e)
        {

            MessageBox.Show("Hello!");
            //var exitEvent = e.OriginalSource as MenuItem;

            //if (exitEvent != null && exitEvent.Name == "MenuItem_Click_Exit")
            //{
            //    var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
            //                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            //}
        }
    }
}
