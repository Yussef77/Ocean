namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;

    /// <summary>Class ValidationResult.</summary>
    public class ValidationResult {
        const Int32 Zero = 0;

        /// <summary>Returns ValidationResult in a success state.</summary>
        /// <value>ValidationResult that is valid.</value>
        public static ValidationResult Success { get { return new ValidationResult(); } }

        /// <summary>
        /// Gets the is valid.
        /// </summary>
        /// <value>The is valid.</value>
        public Boolean IsValid { get; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public IReadOnlyList<KeyValuePair<String, BrokenRule>> ValidationErrors { get; }

        /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
        /// <param name="validationErrors">The validation errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when validationErrors is null.</exception>
        public ValidationResult(IReadOnlyList<KeyValuePair<String, BrokenRule>> validationErrors) {
            if (validationErrors == null) {
                throw new ArgumentNullException(nameof(validationErrors));
            }
            this.ValidationErrors = validationErrors;
            this.IsValid = validationErrors.Count == Zero;
        }

        ValidationResult() {
            this.IsValid = true;
            this.ValidationErrors = new List<KeyValuePair<String, BrokenRule>>();
        }
    }
}
