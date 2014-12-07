using Barter.Li.Win.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Barter.li.Win.ViewModel
{
    /// <summary>
    /// BaseViewModel
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
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
