namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents ValidationRulesManager, maintains rule methods for a business Object or business Object type.
    /// </summary>
    public class ValidationRulesManager {
        Dictionary<String, ValidationRulesList> _validationRulesList;

        /// <summary>
        /// Gets the has rules.
        /// </summary>
        /// <value>Returns <c>True</c> if this <c>ValidationRulesManager</c> has rules.</value>
        public Boolean HasRules { get { return this.RulesDictionary.Count > 0; } }

        /// <summary>
        /// Returns RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        public Dictionary<String, ValidationRulesList> RulesDictionary => _validationRulesList ?? (_validationRulesList = new Dictionary<String, ValidationRulesList>());

        /// <summary>Gets the rules checked.</summary>
        /// <value>The rules checked.</value>
        public Boolean RulesLoaded { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesManager"/> class.
        /// </summary>
        public ValidationRulesManager() { }

        /// <summary>Adds a rule to the list of rules to be enforced.</summary>
        /// <param name="rule">The rule.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">Thrown when rule is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public void AddRule(IValidationRule rule, String propertyName) {
            if (rule is null) {
                throw new ArgumentNullException(nameof(rule));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            IList<IValidationRule> list = GetRulesForProperty(propertyName).List;
            list.Add(rule);
        }

        /// <summary>Returns the list containing rules for a property. If no list exists one is created and returned.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see cref="ValidationRulesList"/> for the specified property name.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public ValidationRulesList GetRulesForProperty(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }
            if (this.RulesDictionary.ContainsKey(propertyName)) {
                return this.RulesDictionary[propertyName];
            }

            var validationRulesList = new ValidationRulesList();
            this.RulesDictionary.Add(propertyName, validationRulesList);
            return validationRulesList;
        }

        /// <summary>Sets the <c>RulesLoaded</c> to <c>True</c>.</summary>
        public void SetRulesLoaded() {
            this.RulesLoaded = true;
        }
    }
}
