namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class CreditCardNumberValidatorAttribute. This class cannot be inherited. Used to validate string properties that represent a credit card number.
    /// Derives from the <see cref="Oceanware.OceanValidation.OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    /// <seealso cref="Oceanware.OceanValidation.OptionallyRequiredBaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CreditCardNumberValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardNumberValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public CreditCardNumberValidatorAttribute(RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            base.RequiredEntry = requiredEntry;
        }

        /// <summary>
        /// Validates the property, Deriving classes must set the <seealso cref="FinalErrorMessage" /> if the validation fails.
        /// </summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="InvalidOperationException">Thrown when method call is invalid for the object's current state. Credit card number validation rule can only be applied to String properties.</exception>
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
                throw new InvalidOperationException(Strings.CreditCardNumberValidationRuleCanOnlyBeAppliedToStringProperties);
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

            String cardNumber = Convert.ToString(targetValue).Trim();

            if (cardNumber.Any(c => !Char.IsDigit(c))) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.CreditCardNumberIsNotAValidCreditCardNumberOnlyNumericInputIsAllowedFormat, displayName, cardNumber), displayName, targetValue);
                return false;
            }

            Int32 lengthCreditCardNumber = cardNumber.Length;
            var cardArray = new Int32[lengthCreditCardNumber + 1];
            Int32 value;

            for (var count = lengthCreditCardNumber - 1; count >= 1; count -= 2) {
                value = Convert.ToInt32(cardNumber.Substring(count - 1, 1)) * 2;
                cardArray[count] = value;
            }

            value = 0;

            Int32 start = lengthCreditCardNumber % 2 == 0 ? 2 : 1;

            for (var count = start; count <= lengthCreditCardNumber; count += 2) {
                value += Convert.ToInt32(cardNumber.Substring(count - 1, 1));
                Int32 arrValue = cardArray[count - 1];

                if (arrValue < 10) {
                    value += arrValue;
                } else {
                    value = value + Convert.ToInt32(Convert.ToString(arrValue).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(arrValue).Substring(Convert.ToString(arrValue).Length - 1));
                }
            }

            if (value % 10 != 0) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.CreditCardNumberIsNotAValidCreditCardNumberFormat, displayName, cardNumber), displayName, targetValue);
                return false;
            }
            return true;
        }
    }
}
