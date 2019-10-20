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
    /// Supported numeric types are <see cref="Int16"/>, <see cref="Int32"/>, <see cref="Int64"/>, <see cref="Single"/>, <see cref="Double"/>, <see cref="Decimal"/>.
    /// </summary>
    public class OceanNumericInput<TValue> : InputBase<TValue> {
        readonly Boolean _isWholeNumberOnly;

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
        /// Gets or sets the error message used when displaying an a parsing error.
        /// </summary>
        /// <value>The parsing error message.</value>
        [Parameter]
        public String ParsingErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the too many digits error message.  See OnAfterRender for default value.
        /// </summary>
        /// <value>The too many digits error message.</value>
        [Parameter]
        public String TooManyDigitsErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the whole number error message used when displaying an a parsing error.
        /// </summary>
        /// <value>The whole number parsing error message.</value>
        [Parameter]
        public String WholeNumberParsingErrorMessage { get; set; }

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

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<String>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.CloseElement();
        }

        /// <inheritdoc />
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
                    throw new InvalidOperationException($"Unsupported type {value.GetType()}");
            }
        }

        /// <inheritdoc />
        protected override void OnAfterRender(Boolean firstRender) {
            if (!_isWholeNumberOnly && String.IsNullOrWhiteSpace(this.TooManyDigitsErrorMessage)) {
                this.TooManyDigitsErrorMessage = String.Concat(Resources.TooManyDigitsErrorMessageFormat, this.NumberOfDecimalPlaces);
            }
            if (String.IsNullOrWhiteSpace(this.ParsingErrorMessage)) {
                this.ParsingErrorMessage = Resources.ParsingErrorMessageFormat;
            }
            if (String.IsNullOrWhiteSpace(this.WholeNumberParsingErrorMessage)) {
                this.WholeNumberParsingErrorMessage = Resources.WholeNumberParsingErrorMessageFormat;
            }
            base.OnAfterRender(firstRender);
        }

        /// <inheritdoc />
        protected override Boolean TryParseValueFromString(String value, out TValue result, out String validationErrorMessage) {
            var tooManyDigits = false;
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
                            tooManyDigits = true;
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

            if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out result)) {
                if (tooManyDigits) {
                    validationErrorMessage = String.Format(TooManyDigitsErrorMessage, FieldIdentifier.FieldName.GetWords());
                    return false;
                }
                validationErrorMessage = null;
                return true;
            } else {
                if (_isWholeNumberOnly) {
                    validationErrorMessage = String.Format(WholeNumberParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                } else {
                    validationErrorMessage = String.Format(ParsingErrorMessage, FieldIdentifier.FieldName.GetWords());
                }
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
