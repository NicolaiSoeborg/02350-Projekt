using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLProgram.ViewModel
{
    /**
      * Alle ViewModels skal extende denne "BaseVM".
      * BaseVM extender "ViewModelBase" som foretager "MvvmLight" magi.
      * Skal data deles mellem ViewModels, så kan denne klasse bruges,
      * dog skal denne model ikke indeholde alt mulig crap, flyt selve logikken til ViewModelen.
    **/
    public class BaseVM : ViewModelBase
    {
        private static string _username = "";
        public string TxtUsername {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(); }
        }
        public string HelloTxtUsername
        {
            get { return String.Format("Hej {0}.", _username); }
        }

    }
}
