namespace Oceanware.Ocean.ValidationRules.InstanceRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;
    using Ocean.Extensions;

    /// <summary>Class CompareDateInstanceValidationRule.
    /// <para>Instance rule that allows validating a <c>DateTime</c> property value against a runtime <c>DateTime</c> value.</para>
    /// <para>An instance rule that has a full API experience that other validators have.</para>
    /// Derives from the <see cref="BaseValidatorAttribute"/></summary>
    public class CompareDateInstanceValidationRule : BaseValidatorAttribute {
        const Int32 Zero = 0;
        readonly ComparisonType _comparisonType;
        readonly Func<DateTime> _targetDateCallBack;

        /// <summary>Initializes a new instance of the <see cref="CompareDateInstanceValidationRule"/> class.</summary>
        /// <param name="targetDateCallBack">The target date.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value comparisonType is not defined.</exception>
        /// <exception cref="ArgumentNullException">Thrown when targetDateCallBack is null.</exception>
        public CompareDateInstanceValidationRule(Func<DateTime> targetDateCallBack, ComparisonType comparisonType) {
            if (!Enum.IsDefined(typeof(ComparisonType), comparisonType)) {
                throw new InvalidEnumArgumentException(nameof(comparisonType), (Int32)comparisonType, typeof(ComparisonType));
            }
            _targetDateCallBack = targetDateCallBack ?? throw new ArgumentNullException(nameof(targetDateCallBack));
            _comparisonType = comparisonType;
            base.RuleType = RuleType.Instance;
        }

        /// <summary>Validates the property, Deriving classes must set the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
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

            var propertyIsNullable = false;
            if (Nullable.GetUnderlyingType(target.GetType()) != null) {
                propertyIsNullable = true;
            }

            var displayName = base.ResolveDisplayName(propertyName, base.FriendlyName, base.ProperCasePropertyName);

            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            var targetValue = propertyInfo.GetValue(target, null);
            if (!propertyIsNullable && targetValue == null) {
                this.FinalErrorMessage = String.Format(Strings.ValueWasNullFormat, propertyName.GetWords());
                return false;
            } else if (propertyIsNullable && targetValue == null) {
                return true;
            }

            var targetDateTimeValue = Convert.ToDateTime(propertyInfo.GetValue(target, null));
            if (targetDateTimeValue == DateTime.MinValue) {
                this.FinalErrorMessage = String.Format(Strings.ValueMustBeGreaterThanDateTimeMinDateFormat, propertyName.GetWords());
                return false;
            }

            var targetDate = _targetDateCallBack();

            var result = targetDateTimeValue.Date.CompareTo(targetDate.Date);

            switch (_comparisonType) {
                case ComparisonType.Equal:
                    if (result == Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustBeEqualToTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThan:
                    if (result > Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustBeGreaterThanTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                case ComparisonType.GreaterThanEqual:
                    if (result >= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustBeGreaterThanOrEqualToTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                case ComparisonType.LessThan:
                    if (result < Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustBeLessThanTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                case ComparisonType.LessThanEqual:
                    if (result <= Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustBeLessThanOrEqualToTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                case ComparisonType.NotEqual:
                    if (result != Zero) {
                        return true;
                    }
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DateMustNotBeEqualTargetValueFormat, displayName, targetDate.Date.ToShortDateString()), displayName, targetValue);
                    return false;

                default:
                    throw new InvalidEnumValueException(typeof(ComparisonType), _comparisonType);
            }
        }
    }
}
