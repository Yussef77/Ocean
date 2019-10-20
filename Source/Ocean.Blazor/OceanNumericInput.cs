namespace Oceanware.Ocean.Blazor {

    using System;
    using System.Globalization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.Rendering;
    using Oceanware.Ocean.Blazor.Properties;
    using Oceanware.Ocean.Extensions;

    /// <summary>
    /// An input component for editing numeric values.
    /// Supported numeric types are <see cref="Int16"/>, <see cref="Int32"/>, <see cref="Int64"/>, <see cref="Single"/>, <see cref="Double"/>, and <see cref="Decimal"/>.
    /// </summary>
    public class OceanNumericInput<TValue> : InputBase<TValue> {
        readonly Boolean _isWholeNumberOnly;

        /// <summary>
        /// <para>
        /// Gets or sets the browser input mode. After setting PLEASE test on mobile devices.
        /// </para>
        /// <para>
        /// When <c>Decimal</c> or <c>Numeric</c>, enables mobile keyboards to popup when the field is accessed on devices.
        /// </para>
        /// </summary>
        /// <value>The browser input mode.</value>
        [Parameter]
        public BrowserInputMode BrowserInputMode { get; set; }

        /// <summary>
        /// Gets or sets the format string.
        /// </summary>
        /// <value>The format string.</value>
        [Parameter]
        public String FormatString { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the number of decimal places.
        /// </summary>
        /// <value>The number of decimal places.</value>
        [Parameter]
        public Int32 NumberOfDecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the error message used when displaying an a parsing error. This message has a localized default message, developers can override the default message by setting this property in the .razor file.
        /// </summary>
        /// <value>The parsing error message.</value>
        [Parameter]
        public String ParsingErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the too many digits error message. This message has a localized default message, developers can override the default message by setting this property in the .razor file.
        /// </summary>
        /// <value>The too many digits error message.</value>
        [Parameter]
        public String TooManyDigitsErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the whole number error message used when displaying an a parsing error. This message has a localized default message, developers can override the default message by setting this property in the .razor file.
        /// </summary>
        /// <value>The whole number parsing error message.</value>
        [Parameter]
        public String WholeNumberParsingErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OceanNumericInput{TValue}"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when bound property type is not <see cref="Int16"/>, <see cref="Int32"/>, <see cref="Int64"/>, <see cref="Single"/>, <see cref="Double"/>, or <see cref="Decimal"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when property number of decimal places is less than zero.</exception>
        public OceanNumericInput() {
            var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
            _isWholeNumberOnly = targetType == typeof(Int32) || targetType == typeof(Int64) || targetType == typeof(Int16);
            if (_isWholeNumberOnly) {
                return;
            }

            if (targetType != typeof(Single) && targetType != typeof(Double) && targetType != typeof(Decimal)) {
                throw new InvalidOperationException(String.Format(Resources.TypeIsNotASupportedNumericTypeFormat, targetType));
            }

            if (NumberOfDecimalPlaces < 0) {
                throw new InvalidOperationException(String.Format(Resources.ValueMustBeEqualToOrGreaterThanZeroFormat, nameof(NumberOfDecimalPlaces), this.NumberOfDecimalPlaces));
            }
        }

        /// <summary>
        /// Renders the component to the supplied RenderTreeBuilder.
        /// </summary>
        /// <param name="builder">A RenderTreeBuilder that will receive the render output.</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<String>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            if (this.BrowserInputMode == BrowserInputMode.Decimal) {
                builder.AddAttribute(6, "inputmode", "decimal");
            } else if (this.BrowserInputMode == BrowserInputMode.Numeric) {
                builder.AddAttribute(6, "inputmode", "numeric");
            }

            builder.CloseElement();
        }

        /// <summary>
        /// Formats the value as a string. Derived classes can override this to determine the formating used for CurrentValueAsString.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A string representation of the value using the current culture when formatting the returned value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when values is an unsupported type.</exception>
        protected override String FormatValueAsString(TValue value) {
            switch (value) {
                case null:
                    return null;

                case Int16 @int16:
                    return this.FormatValue(@int16);

                case Int32 @int32:
                    return this.FormatValue(@int32);

                case Int64 @int64:
                    return this.FormatValue(@int64);

                case Single @single:
                    return this.FormatValue(@single);

                case Double @double:
                    return this.FormatValue(@double);

                case Decimal @decimal:
                    return this.FormatValue(@decimal);

                default:
                    throw new InvalidOperationException(String.Format(Resources.TypeIsNotASupportedNumericTypeFormat, value.GetType().FullName));
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered. If the error messages have not be set in the .razor UI, they are each set to a localized message.
        /// </summary>
        /// <param name="firstRender">Set to <c>true</c> if this is the first time has been invoked on this component instance; otherwise <c>false</c>.</param>
        /// <remarks>The see the OnAfterRender and OnAfterRenderAsync lifecycle methods are useful for performing interop, or interacting with values received from <c>@ref</c>.
        /// Use the <paramref name="firstRender" /> parameter to ensure that initialization work is only performed once.</remarks>
        protected override void OnAfterRender(Boolean firstRender) {
            if (firstRender) {
                if (!_isWholeNumberOnly && String.IsNullOrWhiteSpace(this.TooManyDigitsErrorMessage)) {
                    this.TooManyDigitsErrorMessage = String.Concat(Resources.TooManyDigitsErrorMessageFormat, " ", this.NumberOfDecimalPlaces, ".");
                }
                if (String.IsNullOrWhiteSpace(this.ParsingErrorMessage)) {
                    this.ParsingErrorMessage = Resources.ParsingErrorMessageFormat;
                }
                if (String.IsNullOrWhiteSpace(this.WholeNumberParsingErrorMessage)) {
                    this.WholeNumberParsingErrorMessage = Resources.WholeNumberParsingErrorMessageFormat;
                }
            }
            base.OnAfterRender(firstRender);
        }

        /// <summary>
        /// <para>
        /// Parses a string to create an instance of <typeparamref name="TValue" />. Derived classes can override this to change how CurrentValueAsString interprets incoming values.
        /// </para>
        /// <para>
        /// This method handles all the input use cases to around the decimal separator, group separator, and negative sign.
        /// </para>
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">An instance of <typeparamref name="TValue" />.</param>
        /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
        /// <returns>True if the value could be parsed; otherwise false.</returns>
        protected override Boolean TryParseValueFromString(String value, out TValue result, out String validationErrorMessage) {
            if (String.IsNullOrWhiteSpace(value)) {
                value = "0";
            } else {
                var numberOfCharactersPastDecimalPoint = 0;
                var foundDecimalPoint = false;
                var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                var groupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
                var negativeSign = CultureInfo.CurrentCulture.NumberFormat.NegativeSign;

                foreach (var c in value) {
                    if (Char.IsDigit(c)) {
                        if (foundDecimalPoint) {
                            numberOfCharactersPastDecimalPoint++;
                        }
                        if (numberOfCharactersPastDecimalPoint > this.NumberOfDecimalPlaces) {
                            result = default;
                            validationErrorMessage = String.Format(TooManyDigitsErrorMessage, FieldIdentifier.FieldName.GetWords());
                            return false;
                        }
                        continue;
                    } else {
                        var cString = c.ToString();
                        if (cString == groupSeparator || cString == negativeSign) {
                            continue;
                        } else if (!_isWholeNumberOnly && cString == decimalSeparator) {
                            foundDecimalPoint = true;
                            continue;
                        } else if (_isWholeNumberOnly && cString == decimalSeparator) {
                            result = default;
                            validationErrorMessage = String.Format(WholeNumberParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                            return false;
                        }
                    }
                    result = default;
                    if (_isWholeNumberOnly) {
                        validationErrorMessage = String.Format(WholeNumberParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                    } else {
                        validationErrorMessage = String.Format(ParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                    }

                    return false;
                }
            }

            try {
                if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out result)) {
                    validationErrorMessage = null;
                    return true;
                }
                if (_isWholeNumberOnly) {
                    validationErrorMessage = String.Format(WholeNumberParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                } else {
                    validationErrorMessage = String.Format(ParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                }
                return false;
            } catch {
                validationErrorMessage = String.Format(Resources.UnableToParseInputIntoANumberFormat, FieldIdentifier.FieldName.GetWords());
                result = default;
                return false;
            }
        }

        String FormatValue(Int16 value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }

        String FormatValue(Int32 value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }

        String FormatValue(Int64 value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }

        String FormatValue(Decimal value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }

        String FormatValue(Double value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }

        String FormatValue(Single value) {
            var outValue = value.ToString(this.FormatString, CultureInfo.CurrentCulture);
            return outValue;
        }
    }
}
