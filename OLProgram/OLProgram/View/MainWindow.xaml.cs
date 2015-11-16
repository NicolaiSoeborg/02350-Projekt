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
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(ChangeUC_Click));
        }

        private void ChangeUC_Click(object o, RoutedEventArgs e)
        {
            var ButtonEvent = e.OriginalSource as Button;
            if (ButtonEvent != null)
                switch (ButtonEvent.Name)
                {
                    case "LoginButton":
                        // TODO: Check login (men hvordan? MainWindow extender ikke BaseVM)
                        DoChangeUC(e, new UserUC());  break;
                    case "CheckOutButton":
                        DoChangeUC(e, new LoginUC()); break;
                    case "AdminButton":
                        DoChangeUC(e, new AdminUC()); break;
                }
        }

        private void DoChangeUC(RoutedEventArgs evt, object newUserControl)
        {
            this.Content = newUserControl;
            evt.Handled = true; // Stop event from doing any more modifications
        }
    }
}