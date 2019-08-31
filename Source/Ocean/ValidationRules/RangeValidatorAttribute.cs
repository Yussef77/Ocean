namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class RangeValidatorAttribute. This class cannot be inherited. Used to compare the target value to a lower and upper boundary to see if the value is within the specified range.
    /// Derives from the <see cref="Oceanware.OceanValidation.OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    /// <seealso cref="Oceanware.OceanValidation.OptionallyRequiredBaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RangeValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {
        const Int32 Zero = 0;

        /// <summary>
        /// Gets the type of the lower range boundary.
        /// </summary>
        /// <value>The type of the lower range boundary.</value>
        public RangeBoundaryType LowerRangeBoundaryType { get; }

        /// <summary>
        /// Gets the lower value.
        /// </summary>
        /// <value>The lower value.</value>
        public IComparable LowerValue { get; }

        /// <summary>
        /// Gets the type of the upper range boundary.
        /// </summary>
        /// <value>The type of the upper range boundary.</value>
        public RangeBoundaryType UpperRangeBoundaryType { get; }

        /// <summary>
        /// Gets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public IComparable UpperValue { get; }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Double lowerValue, RangeBoundaryType upperRangeBoundaryType, Double upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int32 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int32 upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int64 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int64 upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Int16 lowerValue, RangeBoundaryType upperRangeBoundaryType, Int16 upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, Single lowerValue, RangeBoundaryType upperRangeBoundaryType, Single upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.LowerValue = lowerValue;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.UpperValue = upperValue;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="ArgumentNullException">Thrown when lowerValue is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when upperValue is null.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.LowerValue = lowerValue ?? throw new ArgumentNullException(nameof(lowerValue));
            this.UpperValue = upperValue ?? throw new ArgumentNullException(nameof(upperValue));
            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.RangeValidatorAttribute"/> class.</summary>
        /// <param name="lowerRangeBoundaryType">Type of the lower range boundary.</param>
        /// <param name="lowerValue">The lower value.</param>
        /// <param name="upperRangeBoundaryType">Type of the upper range boundary.</param>
        /// <param name="upperValue">The upper value.</param>
        /// <param name="convertToType">Type of the convert to.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperRangeBoundaryType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value convertToType is not defined.</exception>
        /// <exception cref="ArgumentNullException">Thrown when lowerValue is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when upperValue is null.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.InvalidEnumValueException">Thrown when enum value has not been programmed.</exception>
        public RangeValidatorAttribute(RangeBoundaryType lowerRangeBoundaryType, String lowerValue, RangeBoundaryType upperRangeBoundaryType, String upperValue, ConvertToType convertToType, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RangeBoundaryType), lowerRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(lowerRangeBoundaryType), (Int32)lowerRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (lowerValue is null) {
                throw new ArgumentNullException(nameof(lowerValue));
            }
            if (!Enum.IsDefined(typeof(RangeBoundaryType), upperRangeBoundaryType)) {
                throw new InvalidEnumArgumentException(nameof(upperRangeBoundaryType), (Int32)upperRangeBoundaryType, typeof(RangeBoundaryType));
            }
            if (upperValue is null) {
                throw new ArgumentNullException(nameof(upperValue));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (!Enum.IsDefined(typeof(ConvertToType), convertToType)) {
                throw new InvalidEnumArgumentException(nameof(convertToType), (Int32)convertToType, typeof(ConvertToType));
            }

            switch (convertToType) {
                case ConvertToType.Date:
                    this.LowerValue = Convert.ToDateTime(lowerValue);
                    this.UpperValue = Convert.ToDateTime(upperValue);
                    break;

                case ConvertToType.Decimal:
                    this.LowerValue = Convert.ToDecimal(lowerValue);
                    this.UpperValue = Convert.ToDecimal(upperValue);
                    break;

                default:
                    throw new InvalidEnumValueException(typeof(ConvertToType), convertToType);
            }

            this.LowerRangeBoundaryType = lowerRangeBoundaryType;
            this.UpperRangeBoundaryType = upperRangeBoundaryType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Validates the string property. Error message is set in the <seealso cref="P:Oceanware.OceanValidation.BaseValidatorAttribute.FinalErrorMessage"/> property.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.InvalidEnumValueException">Thrown when enum value has not been programmed.</exception>
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
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(Strings.ValueWasNullOrDBNullOrEmptyStringButWasRequired, displayName, targetValue);
                    return false;
                }
            } else {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    return true;
                }
            }

            var iSource = (IComparable)targetValue;
            Int32 lowerResult = iSource.CompareTo(this.LowerValue);

            if (this.LowerRangeBoundaryType == RangeBoundaryType.Inclusive) {
                if (lowerResult < Zero) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.InRangeRuleMustBeGreaterThanOrEqualFormat, displayName, this.LowerValue), displayName, targetValue);
                    return false;
                }
            } else {
                if (lowerResult <= Zero) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.InRangeRuleMustBeGreaterThanFormat, displayName, this.LowerValue), displayName, targetValue);
                    return false;
                }
            }

            Int32 upperResult = iSource.CompareTo(this.UpperValue);

            if (this.UpperRangeBoundaryType == RangeBoundaryType.Inclusive) {
                if (upperResult > Zero) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.InRangeRuleMustBeLessThanOrEqualFormat, displayName, this.UpperValue), displayName, targetValue);
                    return false;
                }
            } else {
                if (upperResult >= Zero) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.InRangeRuleMustBeLessThanFormat, displayName, this.UpperValue), displayName, targetValue);
                    return false;
                }
            }
            return true;
        }
    }
}
