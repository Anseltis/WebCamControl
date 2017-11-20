using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ESystems.WebCamControl.Tools.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        /// <seealso cref="INotifyPropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
            => this.OnPropertyChanged(PropertyChanged, propertyName);

        /// <summary>
        /// Notify property changed for property setter.
        /// </summary>
        /// <typeparam name="T">Property type. </typeparam>
        /// <param name="field">Behind field name. </param>
        /// <param name="value">New value. </param>
        /// <param name="propertyName">Changed property name. </param>
        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }
    }
}
