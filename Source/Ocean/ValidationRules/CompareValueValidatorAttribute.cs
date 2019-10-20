namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class CompareValueValidatorAttribute. This class cannot be inherited. Used to compare the value to the target or compare to value based on the <seealso cref="CompareValueValidatorAttribute.ComparisonType"/>.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class CompareValueValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {
        const Int32 Zero = 0;

        /// <summary>
        /// Gets the compare to value.
        /// </summary>
        /// <value>The compare to value.</value>
        public IComparable CompareToValue { get; }

        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Double compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int32 compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int64 compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Int16 compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, Single compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.ComparisonType = comparisonType;
            this.CompareToValue = compareToValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="CompareValueValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToValue">The compare to value.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value convertToType is not defined.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when compareToValue is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumValueException">Thrown when enum value has not been programmed.</exception>
        public CompareValueValidatorAttribute(ComparisonType comparisonType, String compareToValue, ConvertToType convertToType, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            if (String.IsNullOrWhiteSpace(compareToValue)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(compareToValue));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(ConvertToType), convertToType)) {
                throw new InvalidEnumArgumentException(nameof(convertToType), (Int32)convertToType, typeof(ConvertToType));
            }

            this.ComparisonType = comparisonType;

            switch (convertToType) {
                case ConvertToType.Date:
                    this.CompareToValue = Convert.ToDateTime(compareToValue);

                    break;

                case ConvertToType.Decimal:
                    this.CompareToValue = Convert.ToDecimal(compareToValue);

                    break;

                default:
                    throw new InvalidEnumValueException(typeof(ConvertToType), convertToType);
            }

            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Validates the string property. Error message is set in the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> property.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumValueException">Thrown when enum value has not been programmed.</exception>
        public override Boolean IsValid(Object target, String propertyName) {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            this.FinalErrorMessage = String.Empty;

            if (base.SkipValidation(target, this.RuleSet)) {
                return true;
            }

            var displayName = base.ResolveDisplayName(propertyName, this.FriendlyName, this.ProperCasePropertyName);

            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            var targetValue = propertyInfo.GetValue(target, null);

            if (this.RequiredEntry == RequiredEntry.Yes) {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueWasNullOrDBNullOrEmptyStringButWasRequiredFormat, displayName), displayName, targetValue);
                    return false;
                }
            } else {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    return true;
                }
            }

            var iTargetProperty = (IComparable)targetValue;
            Int32 result = iTargetProperty.CompareTo(this.CompareToValue);

            switch (this.ComparisonType) {
                case ComparisonType.Equal:
                    if (result == Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustBeEqualToTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThan:
                    if (result > Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustBeGreaterThanTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThanEqual:
                    if (result >= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustBeGreaterThanOrEqualToTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                case ComparisonType.LessThan:
                    if (result < Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustBeLessThanTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                case ComparisonType.LessThanEqual:
                    if (result <= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustBeLessThanOrEqualToTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                case ComparisonType.NotEqual:
                    if (result != Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueMustNotBeEqualToTargetValueFormat, displayName, this.CompareToValue), displayName, targetValue);
                    return false;

                default:
                    throw new InvalidEnumValueException(typeof(ComparisonType), this.ComparisonType);
            }
        }
    }
}
