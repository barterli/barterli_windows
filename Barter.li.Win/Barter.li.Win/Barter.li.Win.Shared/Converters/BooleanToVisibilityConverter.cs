//----------------------------------------------------------------------------------------------
// <copyright file="BooleanToVisibilityConverter.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    ///     Converter for converting boolean value in to Windows UI Visibility property
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value"> The target data being passed to the source.</param>
        /// <param name="targetType">The type of the target property, specified by a helper structure that wraps the type name.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the source object</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool booleanValue = false;
            if (value is bool)
            {
                booleanValue = (bool)value;
            }
            else if (value is bool?)
            {
                bool? tmp = (bool?)value;
                booleanValue = tmp.HasValue ? tmp.Value : false;
            }

            return booleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        ///     Modifies the target data before passing it to the source object. This method is called only in TwoWay bindings.
        /// </summary>
        /// <param name="value"> The target data being passed to the source.</param>
        /// <param name="targetType">The type of the target property, specified by a helper structure that wraps the type name.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the source object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }
            else
            {
                return false;
            }
        }
    }
}
