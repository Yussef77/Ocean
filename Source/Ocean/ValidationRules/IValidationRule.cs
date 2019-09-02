namespace Oceanware.Ocean.ValidationRules {

    using System;

    /// <summary>Interface IValidationRule</summary>
    public interface IValidationRule {

        /// <summary>Gets the final error message.</summary>
        /// <value>The final error message.</value>
        String FinalErrorMessage { get; }

        /// <summary>
        /// Gets and sets RuleSet. If the RuleSet property is not specified then that rule will always be checked.  If the RuleSet is specified then that rule will only be validated when that RuleSet is active on the parent Object.  If a rule applies to more than one RuleSet, then enter each RuleSet name separated by the pipe symbol.  Example:  RuleSet:="Insert|Update|Delete"
        /// </summary>
        /// <value>The rule set.</value>
        String RuleSet { get; set; }

        /// <summary>Gets the type of the rule.</summary>
        /// <value>The type of the rule.</value>
        RuleType RuleType { get; }

        /// <summary>Gets the name of the rule type.</summary>
        /// <value>The name of the rule type.</value>
        String RuleTypeName { get; }

        /// <summary>Validates the property, Deriving classes must set the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> if the validation fails.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when propertyName is null or whitespace or empty string.</exception>
        Boolean IsValid(Object target, String propertyName);
    }
}
