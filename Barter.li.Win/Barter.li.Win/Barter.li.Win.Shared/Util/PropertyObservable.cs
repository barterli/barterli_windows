//----------------------------------------------------------------------------------------------
// <copyright file="PropertyObservable.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.Util
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    
    /// <summary>
    /// Observable class to Notify property changed
    /// </summary>
    public class PropertyObservable : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     This method is called by the Set accessor of each property. 
        ///     The CallerMemberName attribute that is applied to the optional propertyName 
        ///     parameter causes the property name of the caller to be substituted as an argument. 
        /// </summary>
        /// <param name="propertyName">Name of the Property</param>
        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
