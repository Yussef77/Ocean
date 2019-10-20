namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Linq;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BooleanRequiredValidatorAttribute : BaseValidatorAttribute {

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

            String bankRoutingNumber = Convert.ToString(targetValue).Trim();
            Int32 bankRoutingNumberLength = bankRoutingNumber.Length;
            Int32 bankRoutingNumberCalculationValue = 0;

            if (bankRoutingNumberLength != 9) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersAreNineDigitsInLengthFormat, displayName, bankRoutingNumber), displayName, targetValue);
                return false;
            }

            if (Int32.Parse(bankRoutingNumber.Substring(0, 1)) > 1) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersFirstDigitMustBeZeorOrOneFormat, displayName, bankRoutingNumber), displayName, targetValue);
                return false;
            }

            if (bankRoutingNumber.Any(c => !Char.IsDigit(c))) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersCharactersMustBeNumericFormat, displayName, bankRoutingNumber), displayName, targetValue);
                return false;
            }

            for (Int32 intX = 0; intX <= 8; intX += 3) {
                bankRoutingNumberCalculationValue += Int32.Parse(bankRoutingNumber.Substring(intX, 1)) * 3;
                bankRoutingNumberCalculationValue += Int32.Parse(bankRoutingNumber.Substring(intX + 1, 1)) * 7;
                bankRoutingNumberCalculationValue += Int32.Parse(bankRoutingNumber.Substring(intX + 2, 1));
            }

            if (bankRoutingNumberCalculationValue % 10 != 0) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueIsNotAValidBankRoutingNumberFormat, displayName, bankRoutingNumber), displayName, targetValue);
                return false;
            }
            return true;
        }
    }
}
