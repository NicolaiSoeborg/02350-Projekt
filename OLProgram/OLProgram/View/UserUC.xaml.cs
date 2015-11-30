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
using OLProgram.ViewModel;

namespace OLProgram.View
{
    /// <summary>
    /// Interaction logic for UserUC.xaml
    /// </summary>
    public partial class UserUC : UserControl
    {
        public UserUC()
        {
            InitializeComponent();
        }

        private void KeyUpEventHandler(object sender, KeyEventArgs e)
        {
            int keyToSend = -1;

            if (e.Key == Key.D0)
            {
                keyToSend = 0;
            }
            else if (e.Key == Key.D1)
            {
                keyToSend = 1;
            }
            else if(e.Key == Key.D2)
            {
                keyToSend = 2;
            }
            else if(e.Key == Key.D3)
            {
                keyToSend = 3;
            }
            else if(e.Key == Key.D4)
            {
                keyToSend = 4;
            }
            else if(e.Key == Key.D5)
            {
                keyToSend = 5;
            }
            else if(e.Key == Key.D6)
            {
                keyToSend = 6;
            }
            else if(e.Key == Key.D7)
            {
                keyToSend = 7;
            }
            else if(e.Key == Key.D8)
            {
                keyToSend = 8;
            }
            else if(e.Key == Key.D9)
            {
                keyToSend = 9;
            }else if(e.Key == Key.Enter)
            {
                BasketVM.enterInput();
            }
            else
            {
                
            }
            if(keyToSend != -1)
            {
                BasketVM.writeInput(keyToSend);
            }
            


        }
    }
}
