namespace Oceanware.Ocean.ValidationRules {

    using System;

    /// <summary>Interface IRuleSet</summary>
    public interface IRuleSet {
        /// <summary>
        /// Gets or sets the active rule set.
        /// <para>Empty string validates all rules.</para>
        /// <para>String name of the rule set to validate.  Note, do set multiple rule sets in this string separated by a "|".</para>
        /// <para>Multiple rule set names only apply to properties.</para>
        /// <para>This library has a predefined set of constants that are used on the entity properties, <seealso cref="ValidationConstants"/>.</para>
        /// <para>Developers can also create and use application specific rule sets.</para>
        /// </summary>
        /// <value>The active rule set.</value>
        /// <exception cref="ArgumentException">Active rule set can not contain the pipe symbol '|'. This is normally caused when a validation constant is passed that is for multiple rule sets and not a single rule set that this property requires.</exception>
        String ActiveRuleSet { get; set; }
    }
}
