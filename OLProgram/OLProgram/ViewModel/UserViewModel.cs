using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        public ObservableCollection<String> users { get; set; }

        public ICommand LoginCommand { get; }
    }
}
