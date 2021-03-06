﻿using System;
using System.Windows.Input;
using ESystems.WebCamControl.Tools.ViewModel;

namespace ESystems.WebCamControl.Tools.View
{
    /// <summary>
    /// Relay command factory.
    /// </summary>
    public sealed class CommandFactory : ICommandFactory
    {
        /// <summary>
        /// Create command.
        /// </summary>
        /// <typeparam name="T">Concrete type. </typeparam>
        /// <param name="execute">Command action. </param>
        /// <returns>Instance of <see cref="ICommand"/>. </returns>
        public ICommand CreateCommand<T>(Action<T> execute)
        {
            return CreateCommand(obj => execute((T)obj));
        }

        public ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute)
        {
            return new RelayCommand(o => execute((T)o), o => canExecute((T)o));
        }

        /// <summary>
        /// Create command.
        /// </summary>
        /// <param name="execute">Command action. </param>
        /// <returns>Instance of <see cref="ICommand"/>. </returns>
        public ICommand CreateCommand(Action execute)
        {
            return new RelayCommand(o => execute());
        }

        /// <summary>
        /// Create command.
        /// </summary>
        /// <param name="execute">Command action. </param>
        /// <param name="canExecute">Command predicate. </param>
        /// <returns>Instance of <see cref="ICommand"/>. </returns>
        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(o => execute(), o => canExecute());
        }

        /// <summary>
        /// Create command.
        /// </summary>
        /// <param name="execute">Command action. </param>
        /// <returns>Instance of <see cref="ICommand"/>. </returns>
        private ICommand CreateCommand(Action<object> execute)
        {
            return new RelayCommand(execute);
        }
    }
}
