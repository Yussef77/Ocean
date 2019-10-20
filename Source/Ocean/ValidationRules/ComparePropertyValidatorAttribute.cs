namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>Class ComparePropertyValidatorAttribute. This class cannot be inherited. Used to compare the value of another property based on the <seealso cref="ComparisonType"/>.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute"/></summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class ComparePropertyValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {
        const Int32 Zero = 0;

        /// <summary>
        /// Gets the name of the compare to property.
        /// </summary>
        /// <value>The name of the compare to property.</value>
        public String CompareToPropertyName { get; }

        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>The type of the comparison.</value>
        public ComparisonType ComparisonType { get; }

        /// <summary>Initializes a new instance of the <see cref="ComparePropertyValidatorAttribute"/> class.</summary>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="compareToPropertyName">Name of the compare to property.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when compareToPropertyName is null, empty, or white space.</exception>
        public ComparePropertyValidatorAttribute(ComparisonType comparisonType, String compareToPropertyName, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }

            if (String.IsNullOrWhiteSpace(compareToPropertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(compareToPropertyName));
            }

            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.ComparisonType = comparisonType;
            this.CompareToPropertyName = compareToPropertyName;
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

            var otherPropertyInfo = target.GetType().GetProperty(this.CompareToPropertyName);
            var otherPropertyValue = otherPropertyInfo.GetValue(target, null);
            if (otherPropertyValue == null || Convert.IsDBNull(otherPropertyValue)) {
                return true;
            }

            var otherPropertyDisplayName = base.ResolveDisplayName(otherPropertyInfo.Name, String.Empty, this.ProperCasePropertyName);

            var iTargetProperty = (IComparable)targetValue;
            var iOtherProperty = (IComparable)otherPropertyValue;
            Int32 result = iTargetProperty.CompareTo(iOtherProperty);

            switch (this.ComparisonType) {
                case ComparisonType.Equal:
                    if (result == Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustBeEqualToOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThan:
                    if (result > Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustBeGreaterThanOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThanEqual:
                    if (result >= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustBeGreaterThanOrEqualToOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                case ComparisonType.LessThan:
                    if (result < Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustBeLessThanOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                case ComparisonType.LessThanEqual:
                    if (result <= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustBeLessThanOrEqualToOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                case ComparisonType.NotEqual:
                    if (result != Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.PropertyMustNotBeEqualToOtherPropertyFormat, displayName, otherPropertyDisplayName), displayName, targetValue);
                    return false;

                default:
                    throw new InvalidEnumValueException(typeof(ComparisonType), this.ComparisonType);
            }
        }
    }
}
