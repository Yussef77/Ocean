namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Reflection;

    /// <summary>Validator to check if a property value is a multiple of the <c>MultipleOf</c> property.</summary>
    /// <seealso cref="OptionallyRequiredBaseValidatorAttribute"/>
    public class MultipleOfValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {

        /// <summary>Gets the multiple of value.  This is the value that will be used as the divisor to determine if the property value is a multiple of this value.</summary>
        /// <value>The multiple of.</value>
        public Int32 MultipleOf { get; }

        /// <summary>Constructor.</summary>
        /// <param name="multipleOf">The multiple of.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when multipleOf is not greater than one.</exception>
        public MultipleOfValidatorAttribute(Int32 multipleOf) {
            if (multipleOf <= 1) {
                throw new ArgumentOutOfRangeException(nameof(multipleOf), Strings.ValueMustBeGreaterThanOne);
            }

            this.MultipleOf = multipleOf;
        }

        /// <summary>Validates the property, Deriving classes must set the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
        /// <param name="target">      The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <seealso cref="M:Oceanware.Ocean.ValidationRules.BaseValidatorAttribute.IsValid(Object,String)"/>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is Null Empty White Space.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the decorated property is not an integer.</exception>
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

            if (!(propertyInfo.PropertyType == typeof(Int32))) {
                throw new InvalidOperationException(Strings.MultipleOfValidatorRequirsItBeAppliedToAnIntegerProperty);
            }

            var targetValue = Convert.ToInt32(propertyInfo.GetValue(target, null));

            if (targetValue % this.MultipleOf == 0) {
                return true;
            }

            this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsNotDivisibleByFormat, displayName, targetValue, this.MultipleOf), displayName, targetValue);
            return false;
        }
    }
}
