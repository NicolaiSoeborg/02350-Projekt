using System;
using System.Windows;

namespace OLProgram.View
{
    /// <summary>
    /// Interaction logic for AdminLoginWindow.xaml
    /// </summary>
    public partial class AdminLoginWindow : Window
    {
        public AdminLoginWindow()
        {
            OLProgram.ViewModel.AdminVM._adminLoginWindow = this;
            InitializeComponent();
        }
    }
}
