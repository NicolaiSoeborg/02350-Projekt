﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace OLProgram.OLModel
{
    public abstract class NotifyBase
    {
        //public abstract class NotifyBase : INotifyPropertyChanged
        //{
        //public event PropertyChangedEventHandler PropertyChanged;


        //protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        //{
        //    var propertyName = (propertyExpression?.Body as MemberExpression)?.Member?.Name;
        //    NotifyPropertyChanged(propertyName);
        //}

        //protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    if (propertyName != null && PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
