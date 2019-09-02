namespace Oceanware.Ocean.Infrastructure {

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Class ObservableObject.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged {

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Warns the developer if this Object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                return;
            }
            Debug.Assert(this.GetType().GetRuntimeProperty(propertyName) != null, "Invalid property name: " + propertyName);
        }

        /// <summary>
        /// Handles the <see cref="ObservableObject.PropertyChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="ArgumentNullException">args is null</exception>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            if (args == null) {
                throw new ArgumentNullException(nameof(args));
            }
            var handler = this.PropertyChanged;
            handler?.Invoke(this, args);
        }

        /// <summary>
        /// Verifies the property name exists, calls OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">Optional, name of the property.</param>
        protected void RaisePropertyChanged([CallerMemberName] String propertyName = null) {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
