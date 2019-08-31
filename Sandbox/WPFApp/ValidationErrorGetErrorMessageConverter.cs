namespace WPFApp {

    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Class ValidationErrorGetErrorMessageConverter.
    /// Derives from the <see cref="System.Windows.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(ValidationError), typeof(String))]
    public class ValidationErrorGetErrorMessageConverter : IValueConverter {

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) {
                return null;
            }

            var sb = new System.Text.StringBuilder(1024);

            foreach (ValidationError ve in (ReadOnlyObservableCollection<ValidationError>)value) {
                if (ve.Exception == null || ve.Exception.InnerException == null) {
                    if (ve.ErrorContent != null) {
                        sb.AppendLine(ve.ErrorContent.ToString());
                    }
                } else {
                    sb.AppendLine(ve.Exception.InnerException.Message);
                }
            }

            if (sb.Length > 2) {
                sb.Length -= 2;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">Always throws if called.</exception>
        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
