namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class USStateAbbreviationValidatorAttribute. This class cannot be inherited. Used to validate a US state abbreviation.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class USStateAbbreviationValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="USStateAbbreviationValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public USStateAbbreviationValidatorAttribute(RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            base.RequiredEntry = requiredEntry;
        }

        /// <summary>Validates the string property. Error message is set in the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="InvalidOperationException">Thrown when method call is invalid for the object's current state. Bank routing number validation rule can only be applied to String properties.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when target is null.</exception>
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

            if (!(propertyInfo.PropertyType == typeof(String))) {
                throw new InvalidOperationException(Strings.BankRoutingNumberValidationRuleCanOnlyBeAppliedToStringProperties);
            }

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

            String targetStringValue = Convert.ToString(targetValue).Trim();

            if (StateAbbreviationNameTool.Instance.IsValid(targetStringValue)) {
                return true;
            }
            this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.StateAbbreviationIsNotValidFormat, displayName, targetStringValue), displayName, targetValue);
            return false;
        }
    }
}
