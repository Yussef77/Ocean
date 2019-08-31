namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Class BrokenRule.
    /// </summary>
    public class BrokenRule {
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public String ErrorMessage { get; }
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; }
        /// <summary>
        /// Gets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public RuleType RuleType { get; }
        /// <summary>
        /// Gets the name of the rule type.
        /// </summary>
        /// <value>The name of the rule type.</value>
        public String RuleTypeName { get; }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.BrokenRule"/> class.</summary>
        /// <param name="ruleTypeName">Name of the rule type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="ruleType">Type of the rule.</param>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when ruleTypeName is null, empty, or white space.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when errorMessage is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value ruleType is not defined.</exception>
        public BrokenRule(String ruleTypeName, String propertyName, String errorMessage, RuleType ruleType = RuleType.Attribute) {
            if (String.IsNullOrWhiteSpace(ruleTypeName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(ruleTypeName));
            }

            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            if (String.IsNullOrWhiteSpace(errorMessage)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(errorMessage));
            }

            if (!Enum.IsDefined(typeof(RuleType), ruleType)) {
                throw new InvalidEnumArgumentException(nameof(ruleType), (Int32)ruleType, typeof(RuleType));
            }

            this.RuleTypeName = ruleTypeName;
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.RuleType = ruleType;
        }
    }
}
