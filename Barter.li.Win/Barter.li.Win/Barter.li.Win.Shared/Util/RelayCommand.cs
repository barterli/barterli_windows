//----------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// RelayCommand for Command Binding
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Delegate which can allow command to execute
        /// Command will not executed if the UIElement is Disabled
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Action to be perform when command raise by clicking on Framework Element
        /// </summary>
        private Action<object> executeAction;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="executeAction">Action to be perform when command executes</param>
        public RelayCommand(Action<object> executeAction)
            : this(executeAction, null)
        {
        }      

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="executeAction">Action to be perform when command executes</param>
        /// <param name="canExecute">delegate to tell that command can be execute or not</param>
        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }

            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }
        
        /// <summary>
        /// Event which will be triggered when FrameworkElement (e.g Button) Enable or Disable
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Method who decide that Click/Tap event on Element can perform action or not based on Element's state
        /// </summary>
        /// <param name="parameter">object to pass for command execution</param>
        /// <returns>Can Command execute the Action or not</returns>
        public bool CanExecute(object parameter)
        {
            bool result = true;
            Func<object, bool> canExecuteHandler = this.canExecute;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler(parameter);
            }

            return result;
        }      

        /// <summary>
        /// Method to be executed when FrameworkElement changed from Enable to Disable state or ViceVersa 
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Method which executes the Command
        /// </summary>
        /// <param name="parameter">object for action</param>
        public void Execute(object parameter)
        {
            this.executeAction(parameter);
        }
    }
}