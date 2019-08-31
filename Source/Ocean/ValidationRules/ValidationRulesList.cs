namespace Oceanware.Ocean.ValidationRules {

    using System.Collections.Generic;

    /// <summary>Class ValidationRulesList.</summary>
    public class ValidationRulesList {
        IList<IValidationRule> _list;

        /// <summary>
        /// Gets the list of <seealso cref="IValidationRule"./>.
        /// </summary>
        /// <value>The list.</value>
        public IList<IValidationRule> List => _list ?? (_list = new List<IValidationRule>());

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.ValidationRulesList"/> class.</summary>
        public ValidationRulesList() { }
    }
}
