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

namespace OLProgram.View
{
    /// <summary>
    /// Interaction logic for AdminLoginWindow.xaml
    /// </summary>
    public partial class AdminLoginWindow : Window
    {
        private string TxtPassword { get; set; }

        public AdminLoginWindow()
        {
            InitializeComponent();
        }

        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test: " + TxtPassword);
            //MainWindow.Content = new AdminUC();
        }
    }
}
