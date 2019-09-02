namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;
    using Ocean.Extensions;

    /// <summary>Class BaseValidatorAttribute.
    /// Derives from the <see cref="Attribute"/></summary>
    public abstract class BaseValidatorAttribute : Attribute, IValidationRule {

        /// <summary>
        /// Get and sets AdditionalMessage. Developers can specify an additional message that is displayed when the validation rule is broken.  The additional message is displayed first and then normal broken rule is appended to the custom message if provided or to the localized message.
        /// </summary>
        /// <value>The custom message.</value>
        public String AdditionalMessage { get; set; } = String.Empty;

        /// <summary>
        ///     Gets or sets the resource name (property name) to use as the key for lookups on the resource type.
        /// </summary>
        /// <value>
        ///     Use this property to set the name of the property within <c>ErrorMessageResourceType</c> that will provide a localized error message.  Use <c>ErrorMessage</c> for non-localized error messages.
        /// </value>
        public String ErrorMessageResourceName { get; set; } = String.Empty;

        /// <summary>
        ///     Gets or sets the resource type to use for error message lookups.
        /// </summary>
        /// <value>
        ///     Use this property only in conjunction with <see cref="BaseValidatorAttribute.ErrorMessageResourceName" />.  They are used together to retrieve localized error messages at runtime. If either is null or an empty string, this pair of properties will be ignored for message generation.
        ///     <para>
        ///         Use <see cref="BaseValidatorAttribute.OverrideErrorMessage" /> instead of this pair if error messages are not localized.
        ///     </para>
        /// </value>
        public Type ErrorMessageResourceType { get; set; }

        /// <summary>Gets or sets the final error message.</summary>
        /// <value>The final error message.</value>
        public String FinalErrorMessage { get; protected set; } = String.Empty;

        /// <summary>
        /// Gets and sets FriendlyName. When supplied, provides way to override the name displayed in failed validation messages. If not provided the property name will be reformatted by splitting the pascal case name, adding a white space between capitalized letters.
        /// </summary>
        /// <value>The name of the friendly name.</value>
        public String FriendlyName { get; set; } = String.Empty;

        /// <summary>
        /// Get and sets OverrideErrorMessage. Developers can specify an override error message that is displayed when the validation rule is broken.  If supplied, override message replaces the normal broken rule or custom message.
        /// </summary>
        /// <value>The override error message is intended to be used for non-localizable override error messages in lieu of either the default error message or the user <see cref="BaseValidatorAttribute.ErrorMessageResourceType"/> and <see cref="P:Oceanware.OceanValidation.BaseValidatorAttribute.ErrorMessageResourceName"/> for localizable error messages.</value>
        public String OverrideErrorMessage { get; set; } = String.Empty;

        /// <summary>Gets or sets the name of the proper case property. The default value is <c>ProperCasePropertyName.Yes</c>.</summary>
        /// <value>The name of the proper case property.</value>
        public ProperCasePropertyName ProperCasePropertyName { get; set; } = ProperCasePropertyName.Yes;

        /// <summary>Gets or sets the resource error message format String argument.</summary>
        /// <value>
        /// When not <c>None</c>, the value is used to determine the arguments specified, and the order specified when using <seealso cref="String.Format"/> to format the <seealso cref="BaseValidatorAttribute.ErrorMessageResourceName"/> value.
        /// </value>
        public ResourceErrorMessageFormatStringArgument ResourceErrorMessageFormatStringArgument { get; set; } = ResourceErrorMessageFormatStringArgument.None;

        /// <summary>
        /// Gets and sets RuleSet. If the RuleSet property is not specified then that rule will always be checked.  If the RuleSet is specified then that rule will only be validated when that RuleSet is active on the parent Object.  If a rule applies to more than one RuleSet, then enter each RuleSet name separated by the pipe symbol.  Example:  RuleSet:="Insert|Update|Delete"
        /// </summary>
        /// <value>The rule set.</value>
        public String RuleSet { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public RuleType RuleType { get; set; } = RuleType.Attribute;

        /// <summary>Gets the name of the rule type.</summary>
        /// <value>The name of the rule type.</value>
        public String RuleTypeName { get { return this.GetType().Name; } }

        /// <summary>Initializes a new instance of the <see cref="BaseValidatorAttribute"/> class.</summary>
        protected BaseValidatorAttribute() {
        }

        /// <summary>Validates the property, Deriving classes must set the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public abstract Boolean IsValid(Object target, String propertyName);

        /// <summary>
        ///   <para>If the user has set the ErrorMessage property this will be used as the message.</para>
        ///   <para>If both ErrorMessageResourceName and ErrorMessageResourceType are set the ErrorMessageString value will be used as the message.</para>
        /// </summary>
        /// <param name="defaultMessage">The error message provided by the validator.</param>
        /// <param name="displayName">Display name for the property.</param>
        /// <param name="targetValue">The target value.</param>
        /// <returns>A string error message.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when defaultMessage is null, empty, or white space.</exception>
        protected String CreateFailedValidationMessage(String defaultMessage, String displayName, Object targetValue) {
            if (String.IsNullOrWhiteSpace(defaultMessage)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(defaultMessage));
            }

            var finalMessage = defaultMessage;

            if (!String.IsNullOrWhiteSpace(this.OverrideErrorMessage)) {
                return this.OverrideErrorMessage;
            }

            var resourceErrorMessage = GetResourceErrorMessage(displayName, targetValue);
            if (!String.IsNullOrWhiteSpace(resourceErrorMessage)) {
                finalMessage = resourceErrorMessage;
            }

            if (!String.IsNullOrWhiteSpace(this.AdditionalMessage)) {
                finalMessage = $"{this.AdditionalMessage} {finalMessage}";
            }

            return finalMessage;
        }

        /// <summary>
        /// Resolves the display name.  If <c>friendlyName</c> is supplied, this will be used. If <c>properCasePropertyName</c> is no, the <c>propertyName</c> will be used; otherwise, the <c>propertyName</c> will be parsed, providing white space between upper case characters.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="friendlyName">Friendly name overrides the property name.</param>
        /// <param name="properCasePropertyName">ProperCasePropertyName determines if the <c>propertyName</c>gets parsed or not.</param>
        /// <returns>Display name based on the above arguments.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value properCasePropertyName is not defined.</exception>
        protected String ResolveDisplayName(String propertyName, String friendlyName, ProperCasePropertyName properCasePropertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }
            if (!Enum.IsDefined(typeof(ProperCasePropertyName), properCasePropertyName)) {
                throw new InvalidEnumArgumentException(nameof(properCasePropertyName), (Int32)properCasePropertyName, typeof(ProperCasePropertyName));
            }

            if (!String.IsNullOrWhiteSpace(friendlyName)) {
                return friendlyName;
            }
            if (properCasePropertyName == ProperCasePropertyName.No) {
                return propertyName;
            }
            return propertyName.GetWords();
        }

        /// <summary>Determines if the property validation should continue based on the current rule set for the Object and rule set for the property.</summary>
        /// <param name="target">The target Object the validation rules are being run against.</param>
        /// <param name="ruleSet">Rule set on the property.</param>
        /// <returns>
        ///   <c>True</c> if if the validation should be skipped; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        protected Boolean SkipValidation(Object target, String ruleSet) {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (!String.IsNullOrWhiteSpace(ruleSet)) {
                if (target is IRuleSet rs) {
                    if (!String.IsNullOrWhiteSpace(rs.ActiveRuleSet)) {
                        if (!ruleSet.Contains(rs.ActiveRuleSet)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>Gets the resource error message.</summary>
        /// <param name="displayName">Display name for the property.</param>
        /// <param name="targetValue">The target value.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when displayName is null, empty, or white space.</exception>
        /// <exception cref="InvalidOperationException">Thrown when method call is invalid for the object's current state.</exception>
        /// <exception cref="InvalidEnumValueException">Thrown when enum value has not been programmed.</exception>
        String GetResourceErrorMessage(String displayName, Object targetValue) {
            if (String.IsNullOrWhiteSpace(displayName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(displayName));
            }
            if (String.IsNullOrWhiteSpace(this.ErrorMessageResourceName) || this.ErrorMessageResourceType == null) {
                return String.Empty;
            }
            var property = this.ErrorMessageResourceType.GetTypeInfo().GetDeclaredProperty(this.ErrorMessageResourceName);
            if (property == null) {
                return String.Empty;
            }

            var propertyGetter = property.GetMethod;
            if (propertyGetter == null || (!propertyGetter.IsAssembly && !propertyGetter.IsPublic)) {
                return String.Empty;
            }

            if (property.PropertyType != typeof(String)) {
                throw new InvalidOperationException(String.Format(Strings.ValidationAttributeResourcePropertyNotStringTypeFormat, property.Name, property.PropertyType.Name));
            }

            var resourceErrorMessage = property.GetValue(null, null);
            if (resourceErrorMessage == null) {
                return String.Empty;
            }

            var resourceErrorMessageValue = (String)resourceErrorMessage;
            var resolvedErrorMessage = resourceErrorMessageValue;
            String targetStringValue;
            if (targetValue == null) {
                targetStringValue = "null";
            } else if (Convert.IsDBNull(targetValue)) {
                targetStringValue = "DBNull";
            } else {
                targetStringValue = targetValue.ToString();
            }
            if (String.IsNullOrWhiteSpace(targetStringValue)) {
                targetStringValue = "empty string";
            }

            switch (this.ResourceErrorMessageFormatStringArgument) {
                case ResourceErrorMessageFormatStringArgument.None:
                    break;

                case ResourceErrorMessageFormatStringArgument.PropertyName:
                    resolvedErrorMessage = String.Format(resourceErrorMessageValue, displayName);
                    break;

                case ResourceErrorMessageFormatStringArgument.Value:
                    resolvedErrorMessage = String.Format(resourceErrorMessageValue, targetStringValue);
                    break;

                case ResourceErrorMessageFormatStringArgument.PropertyNameValue:
                    resolvedErrorMessage = String.Format(resourceErrorMessageValue, displayName, targetStringValue);
                    break;

                case ResourceErrorMessageFormatStringArgument.ValuePropertyName:
                    resolvedErrorMessage = String.Format(resourceErrorMessageValue, targetStringValue, displayName);
                    break;

                default:
                    throw new InvalidEnumValueException(typeof(ResourceErrorMessageFormatStringArgument), this.ResourceErrorMessageFormatStringArgument);
            }

            return resolvedErrorMessage;
        }
    }
}
