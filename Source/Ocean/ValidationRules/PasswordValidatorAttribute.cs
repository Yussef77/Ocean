namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class PasswordValidatorAttribute. This class cannot be inherited.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PasswordValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {
        const Int32 One = 1;
        const Int32 Zero = 0;

        /// <summary>
        /// Gets the allowed password special characters.
        /// </summary>
        /// <value>The allowed password special characters.</value>
        public String AllowedPasswordSpecialCharacters { get; }

        /// <summary>
        /// Gets the digit character.
        /// </summary>
        /// <value>The digit character.</value>
        public DigitCharacter DigitCharacter { get; }

        /// <summary>
        /// Gets the lower case character.
        /// </summary>
        /// <value>The lower case character.</value>
        public LowerCaseCharacter LowerCaseCharacter { get; }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; }

        /// <summary>
        /// Gets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; }

        /// <summary>
        /// Gets the special character.
        /// </summary>
        /// <value>The special character.</value>
        public SpecialCharacter SpecialCharacter { get; }

        /// <summary>
        /// Gets the upper case character.
        /// </summary>
        /// <value>The upper case character.</value>
        public UpperCaseCharacter UpperCaseCharacter { get; }

        /// <summary>Initializes a new instance of the <see cref="PasswordValidatorAttribute"/> class.</summary>
        /// <param name="lowerCaseCharacter">Is a lower case character required.</param>
        /// <param name="upperCaseCharacter">Is an upper case character required.</param>
        /// <param name="digitCharacter">Is a digit character required.</param>
        /// <param name="specialCharacter">Is a special character required.</param>
        /// <param name="allowedPasswordSpecialCharacters">The allowed password special characters. Change value to limit or increase the number of special characters.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when minimumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength less than or equal to Minimum Length.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerCaseCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperCaseCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value digitCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value specialCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when allowedPasswordSpecialCharacters is null, empty, or white space.</exception>
        public PasswordValidatorAttribute(Int32 minimumLength, Int32 maximumLength, LowerCaseCharacter lowerCaseCharacter, UpperCaseCharacter upperCaseCharacter, DigitCharacter digitCharacter, SpecialCharacter specialCharacter, String allowedPasswordSpecialCharacters = "!@#$*^%&()-_+|")
            : this(minimumLength, maximumLength, lowerCaseCharacter, upperCaseCharacter, digitCharacter, specialCharacter, RequiredEntry.Yes, allowedPasswordSpecialCharacters) {
        }

        /// <summary>Initializes a new instance of the <see cref="PasswordValidatorAttribute"/> class.</summary>
        /// <param name="lowerCaseCharacter">Is a lower case character required.</param>
        /// <param name="upperCaseCharacter">Is an upper case character required.</param>
        /// <param name="digitCharacter">Is a digit character required.</param>
        /// <param name="specialCharacter">Is a special character required.</param>
        /// <param name="requiredEntry">Is the entry required.</param>
        /// <param name="allowedPasswordSpecialCharacters">The allowed password special characters. Change value to limit or increase the number of special characters.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when minimumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength less than or equal to Minimum Length.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value lowerCaseCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value upperCaseCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value digitCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value specialCharacter is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when allowedPasswordSpecialCharacters is null, empty, or white space.</exception>
        public PasswordValidatorAttribute(Int32 minimumLength, Int32 maximumLength, LowerCaseCharacter lowerCaseCharacter, UpperCaseCharacter upperCaseCharacter, DigitCharacter digitCharacter, SpecialCharacter specialCharacter, RequiredEntry requiredEntry, String allowedPasswordSpecialCharacters = "!@#$*&^%()-_+|") {
            if (minimumLength < One) {
                throw new ArgumentOutOfRangeException(nameof(minimumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength < One) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanOrEqualToMinimumLength);
            }
            if (!Enum.IsDefined(typeof(LowerCaseCharacter), lowerCaseCharacter)) {
                throw new InvalidEnumArgumentException(nameof(lowerCaseCharacter), (Int32)lowerCaseCharacter, typeof(LowerCaseCharacter));
            }
            if (!Enum.IsDefined(typeof(UpperCaseCharacter), upperCaseCharacter)) {
                throw new InvalidEnumArgumentException(nameof(upperCaseCharacter), (Int32)upperCaseCharacter, typeof(UpperCaseCharacter));
            }
            if (!Enum.IsDefined(typeof(DigitCharacter), digitCharacter)) {
                throw new InvalidEnumArgumentException(nameof(digitCharacter), (Int32)digitCharacter, typeof(DigitCharacter));
            }
            if (!Enum.IsDefined(typeof(SpecialCharacter), specialCharacter)) {
                throw new InvalidEnumArgumentException(nameof(specialCharacter), (Int32)specialCharacter, typeof(SpecialCharacter));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (specialCharacter == SpecialCharacter.Yes && String.IsNullOrWhiteSpace(allowedPasswordSpecialCharacters)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(allowedPasswordSpecialCharacters));
            }

            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
            this.LowerCaseCharacter = lowerCaseCharacter;
            this.UpperCaseCharacter = upperCaseCharacter;
            this.DigitCharacter = digitCharacter;
            this.SpecialCharacter = specialCharacter;
            this.AllowedPasswordSpecialCharacters = allowedPasswordSpecialCharacters;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Validates the String property. Error message is set in the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
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
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.ValueWasNullOrDBNullOrEmptyStringButWasRequiredFormat, displayName), displayName, targetValue);
                    return false;
                }
            } else {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    return true;
                }
            }

            var sb = new StringBuilder();
            String password = Convert.ToString(targetValue).Trim();
            var mustContainPrepended = false;

            if (this.MinimumLength > Zero && password.Length < this.MinimumLength) {
                sb.Append(String.Format(Strings.PasswordMinimumLengthIsFormat, this.MinimumLength));
            }
            if (password.Length > this.MaximumLength) {
                sb.Append(String.Format(Strings.PasswordMaximumLengthIsFormat, this.MaximumLength));
            }
            if (this.LowerCaseCharacter == LowerCaseCharacter.Yes && !password.Any(Char.IsLower)) {
                sb.Append("must contain ");
                mustContainPrepended = true;
                sb.Append(Strings.PasswordAtLeaseOneLowerCaseCharacter);
            }
            if (this.UpperCaseCharacter == UpperCaseCharacter.Yes && !password.Any(Char.IsUpper)) {
                if (!mustContainPrepended) {
                    sb.Append("must contain ");
                    mustContainPrepended = true;
                }
                sb.Append(Strings.PasswordAtLeaseOneUpperCaseCharacter);
            }
            if (this.DigitCharacter == DigitCharacter.Yes && !password.Any(Char.IsDigit)) {
                if (!mustContainPrepended) {
                    sb.Append("must contain ");
                    mustContainPrepended = true;
                }
                sb.Append(Strings.PasswordAtLeaseOneDigitCharacter);
            }
            if (this.SpecialCharacter == SpecialCharacter.Yes) {
                var specialCharacters = new HashSet<Char>(this.AllowedPasswordSpecialCharacters.ToCharArray());
                if (!password.Any(specialCharacters.Contains)) {
                    if (!mustContainPrepended) {
                        sb.Append("must contain ");
                        mustContainPrepended = true;
                    }
                    sb.Append(String.Format(Strings.PasswordAtLeaseOneSpecialCharacterFormat, this.AllowedPasswordSpecialCharacters));
                }
            }
            if (sb.Length > Zero) {
                sb.Insert(Zero, $"{displayName} ");
                var errorMessage = sb.ToString().Trim();
                if (errorMessage.EndsWith(",")) {
                    errorMessage = errorMessage.Substring(0, errorMessage.Length - 1);
                }
                this.FinalErrorMessage = base.CreateFailedValidationMessage(errorMessage, displayName, targetValue);
                return false;
            }
            return true;
        }
    }
}
