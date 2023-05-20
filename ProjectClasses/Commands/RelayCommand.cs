﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolStoreProject
{
    internal class RelayCommand : ICommand
    {
        // Fields
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        //

        // Events
        public event EventHandler ?CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //

        // Constructors
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        //

        // Methods
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)

        {
            this.execute(parameter);
        }
        //
    }
}
