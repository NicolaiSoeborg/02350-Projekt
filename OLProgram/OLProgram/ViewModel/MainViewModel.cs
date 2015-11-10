using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLModel;
using System.Windows;

namespace OLProgram.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        /*public ICommand LoginCommand
        {
            get { return new Commands.LoginCommand(); }
        }*/
        
        public ICommand closeApplicationCommand { get; }



        public MainViewModel()
        {

            //closeApplicationCommand = new RelayCommand(closeApplication(0));



            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void closeApplication(int status)
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            //if (response = MessageBoxResult.No)
           // {
                
            //} else
           // {
           //     Environment.Exit(0);
           // }
        }

    }
}