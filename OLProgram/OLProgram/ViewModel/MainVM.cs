using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using OLProgram.OLModel;
using OLProgram.Command;
using System.Collections.ObjectModel;
using OLModel;
using System.Windows;

namespace OLProgram.ViewModel
{

    public class MainVM : BaseVM
    {
        public ICommand AddProductToGlobalCommand { get; }

        public MainVM()
        {
            //AddProductToGlobalCommand = new RelayCommand(AddProductToGlobal);
        }
        
        //private void AddProductToGlobal()
        //{
        //    undoRedoController.AddAndExecute(new AddProductToBasketCommand(Products, new Product()));
        //}

    }
}