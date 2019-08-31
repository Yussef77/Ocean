namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Class BrokenValidationRules.
    /// </summary>
    public class BrokenValidationRules {
        const Int32 Zero = 0;
        readonly Dictionary<String, List<BrokenRule>> _entityBrokenRules = new Dictionary<String, List<BrokenRule>>();

        /// <summary>Gets the error count.</summary>
        /// <value>The error count.</value>
        public Int32 ErrorCount { get { return _entityBrokenRules.Count; } }

        /// <summary>Gets the has errors.</summary>
        /// <value>The has errors.</value>
        public Boolean HasErrors { get { return _entityBrokenRules.Count > 0; } }

        /// <summary>Adds the specified validation result to the broken rules.</summary>
        /// <param name="validationResult">The validation result.</param>
        /// <exception cref="ArgumentNullException">Thrown when validationResult is null.</exception>
        public void Add(ValidationResult validationResult) {
            if (validationResult is null) {
                throw new ArgumentNullException(nameof(validationResult));
            }

            if (validationResult.IsValid) {
                return;
            }
            foreach (var item in validationResult.ValidationErrors) {
                Add(item.Value.RuleTypeName, item.Value.PropertyName, item.Value.ErrorMessage, item.Value.RuleType);
            }
        }

        /// <summary>Adds the specified rule to the broken rules.</summary>
        /// <param name="ruleTypeName">Name of the rule type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="ruleType">Type of the rule.</param>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when ruleTypeName is null, empty, or white space.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when errorMessage is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value ruleType is not defined.</exception>
        public void Add(String ruleTypeName, String propertyName, String errorMessage, RuleType ruleType) {
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

            if (_entityBrokenRules.TryGetValue(propertyName, out List<BrokenRule> brokenRules)) {
                brokenRules.Add(new BrokenRule(ruleTypeName, propertyName, errorMessage));
            } else {
                _entityBrokenRules.Add(propertyName, new List<BrokenRule> { new BrokenRule(ruleTypeName, propertyName, errorMessage, ruleType) });
            }
        }

        /// <summary>
        /// Clears the broken rules.
        /// </summary>
        public void Clear() {
            _entityBrokenRules.Clear();
        }

        /// <summary>Gets the broken rules for a property.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>IEnumerable&lt;BrokenRule&gt;.</returns>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public IEnumerable<BrokenRule> GetBrokenRules(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            if (_entityBrokenRules.TryGetValue(propertyName, out List<BrokenRule> brokenRules)) {
                return brokenRules;
            }
            return new List<BrokenRule>();
        }

        /// <summary>
        /// Gets all broken rules.
        /// </summary>
        /// <returns>IEnumerable&lt;BrokenRule&gt;.</returns>
        public IEnumerable<BrokenRule> GetBrokenRules() {
            var brokenRules = new List<BrokenRule>();
            if (_entityBrokenRules.Count > Zero) {
                foreach (var item in _entityBrokenRules.Values) {
                    brokenRules.AddRange(item);
                }
            }
            return brokenRules;
        }

        /// <summary>
        /// Removes all broken rules for the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Boolean.</returns>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public Boolean Remove(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }
            if (_entityBrokenRules.ContainsKey(propertyName)) {
                return _entityBrokenRules.Remove(propertyName);
            }
            return false;
        }
    }
}
