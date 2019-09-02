namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class RegularExpressionValidatorAttribute. This class cannot be inherited. Used to validate string properties ensuring they match the supplied regex pattern.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RegularExpressionValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {

        /// <summary>
        /// Gets the custom regular expression pattern.
        /// </summary>
        /// <value>The custom regular expression pattern.</value>
        public String CustomRegularExpressionPattern { get; }

        /// <summary>
        /// Gets the type of the regular expression pattern.
        /// </summary>
        /// <value>The type of the regular expression pattern.</value>
        public RegularExpressionPatternType RegularExpressionPatternType { get; }

        /// <summary>Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute"/> class.</summary>
        /// <param name="regularExpressionPatternType">Type of the regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value regularExpressionPatternType is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RegularExpressionValidatorAttribute(RegularExpressionPatternType regularExpressionPatternType, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!Enum.IsDefined(typeof(RegularExpressionPatternType), regularExpressionPatternType)) {
                throw new InvalidEnumArgumentException(nameof(regularExpressionPatternType), (Int32)regularExpressionPatternType, typeof(RegularExpressionPatternType));
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }

            this.RegularExpressionPatternType = regularExpressionPatternType;
            this.RequiredEntry = requiredEntry;
        }

        /// <summary>Initializes a new instance of the <see cref="RegularExpressionValidatorAttribute"/> class.</summary>
        /// <param name="customRegularExpressionPattern">The custom regular expression pattern.</param>
        /// <param name="requiredEntry">The required entry.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when customRegularExpressionPattern is an invalid regular expression pattern.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        public RegularExpressionValidatorAttribute(String customRegularExpressionPattern, RequiredEntry requiredEntry = RequiredEntry.Yes) {
            if (!IsRegularExpressionPatternValid(customRegularExpressionPattern)) {
                throw new ArgumentOutOfRangeException(nameof(customRegularExpressionPattern), Strings.RegularExpressionInvalidCustomRegularExpressionPattern);
            }
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            this.RegularExpressionPatternType = RegularExpressionPatternType.Custom;
            this.CustomRegularExpressionPattern = customRegularExpressionPattern;
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
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(Strings.ValueWasNullOrDBNullOrEmptyStringButWasRequired, displayName, targetValue);
                    return false;
                }
            } else {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    return true;
                }
            }

            var targetStringValue = Convert.ToString(targetValue);
            String pattern;
            String brokenRuleMessage;

            switch (this.RegularExpressionPatternType) {
                case RegularExpressionPatternType.Custom:
                    pattern = this.CustomRegularExpressionPattern;
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredPatternFormat, displayName, pattern);
                    break;

                case RegularExpressionPatternType.Email:
                    pattern = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredEmailPatternFormat, displayName);
                    break;

                case RegularExpressionPatternType.IPAddress:
                    pattern = "^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])$";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredIPAddressPatternFormat, displayName);
                    break;

                case RegularExpressionPatternType.SSN:
                    pattern = "^\\d{3}-\\d{2}-\\d{4}$";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredSSNPatternFormat, displayName);
                    break;

                case RegularExpressionPatternType.URLIsWellFormed:
                case RegularExpressionPatternType.URL:
                    pattern = "(?#WebOrIP)((?#protocol)((news|nntp|telnet|http|ftp|https|ftps|sftp):\\/\\/)?(?#subDomain)(([a-zA-Z0-9]+\\.*(?#domain)[a-zA-Z0-9\\-]+(?#TLD)(\\.[a-zA-Z]+){1,2})|(?#IPAddress)((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))+(?#Port)(:[1-9][0-9]*)?)+(?#Path)((\\/((?#dirOrFileName)[a-zA-Z0-9_\\-\\%\\~\\+]+)?)*)?(?#extension)(\\.([a-zA-Z0-9_]+))?(?#parameters)(\\?([a-zA-Z0-9_\\-]+\\=[a-z-A-Z0-9_\\-\\%\\~\\+]+)?(?#additionalParameters)(\\&([a-zA-Z0-9_\\-]+\\=[a-z-A-Z0-9_\\-\\%\\~\\+]+)?)*)?";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredURLPatternFormat, displayName);
                    break;

                case RegularExpressionPatternType.USPhoneNumber:
                    pattern = @"^\(?([0-9]{3})\)?[\s-.●]?([0-9]{3})[-.●]?([0-9]{4})$";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredUSPhoneNumberPatternFormat, displayName);
                    break;

                case RegularExpressionPatternType.USZipCode:
                    pattern = "^\\d{5}(-\\d{4})?$";
                    brokenRuleMessage = String.Format(Strings.RegularExpressionDidNotMatchTheRequiredZipCodePatternFormat, displayName);
                    break;

                default:
                    throw new InvalidEnumValueException(typeof(RegularExpressionPatternType), this.RegularExpressionPatternType);
            }

            if (Regex.IsMatch(targetStringValue, pattern, RegexOptions.IgnoreCase)) {
                return true;
            }
            if (this.RegularExpressionPatternType == RegularExpressionPatternType.URLIsWellFormed) {
                if (Uri.IsWellFormedUriString(targetStringValue, UriKind.Absolute)) {
                    return true;
                }
            }
            this.FinalErrorMessage = base.CreateFailedValidationMessage(brokenRuleMessage, displayName, targetValue);
            return false;
        }

        Boolean IsRegularExpressionPatternValid(String regularExpressionPattern) {
            try {
                new Regex(regularExpressionPattern);
                return true;
            } catch {
                return false;
            }
        }
    }
}
