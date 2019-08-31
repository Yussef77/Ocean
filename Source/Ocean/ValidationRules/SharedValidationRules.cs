namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents SharedValidationRules, maintains a list of all the per-type <see cref="ValidationRulesManager"/> objects loaded in memory.
    /// </summary>
    public static class SharedValidationRules {
        const Int32 Zero = 0;
        static readonly Object LockObject = new Object();
        static readonly Dictionary<Type, ValidationRulesManager> ValidationRuleManagers = new Dictionary<Type, ValidationRulesManager>();

        /// <summary>
        /// Gets the <see cref="ValidationRulesManager"/> for the specified Object type, optionally creating a new instance of the ValidationRulesManager for the type if necessary.
        /// </summary>
        /// <param name="type">Type of business Object for which the rules apply.</param>
        /// <returns><see cref="ValidationRulesManager"/> for the specified type.</returns>
        public static ValidationRulesManager GetManager(Type type) {
            lock (ValidationRuleManagers) {
                if (!ValidationRuleManagers.TryGetValue(type, out ValidationRulesManager manager)) {
                    manager = new ValidationRulesManager();
                    ValidationRuleManagers.Add(type, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a set of rules have been created for a given <see cref="Type" />.
        /// </summary>
        /// <param name="type">
        /// Type of business Object for which the rules apply.
        /// </param>
        /// <returns><see langword="true" /> if rules exist for the type.</returns>
        public static Boolean RulesExistFor(Type type) {
            lock (LockObject) {
                if (!ValidationRuleManagers.ContainsKey(type)) {
                    return false;
                }
                return ValidationRuleManagers[type].RulesDictionary.Count > Zero;
            }
        }
    }
}
