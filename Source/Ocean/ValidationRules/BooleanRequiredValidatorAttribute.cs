namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Reflection;

    /// <summary>
    /// Class BooleanRequiredValidatorAttribute is applied to Boolean properties and when applied, requires that the value be true.
    /// Used in website to required users to accept terms, etc.
    /// Derives from the <see cref="BaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BooleanRequiredValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanRequiredValidatorAttribute"/> class.
        /// </summary>
        public BooleanRequiredValidatorAttribute() {
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

            if (!(propertyInfo.PropertyType == typeof(Boolean) || propertyInfo.PropertyType == typeof(Boolean?))) {
                throw new InvalidOperationException(Strings.BankRoutingNumberValidationRuleCanOnlyBeAppliedToStringProperties);
            }

            var targetValue = propertyInfo.GetValue(target, null);
            if (targetValue == null) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsRequiredToBeCheckedFormat, displayName), displayName, targetValue);
                return false;
            }

            var resolvedValue = (Boolean)targetValue;
            if (resolvedValue) {
                return true;
            } else {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsRequiredToBeCheckedFormat, displayName), displayName, targetValue);
                return false;
            }
        }
    }
}
