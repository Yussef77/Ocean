namespace Oceanware.Ocean.ValidationRules {

    /// <summary>Class OptionallyRequiredBaseValidatorAttribute.
    /// Derives from the <see cref="BaseValidatorAttribute"/></summary>
    public abstract class OptionallyRequiredBaseValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Gets or sets the required entry. Note: <c>RequiredEntry</c> only applies to string properties.
        /// </summary>
        /// <value>The required entry.</value>
        public RequiredEntry RequiredEntry { get; set; } = RequiredEntry.Yes;

        /// <summary>Initializes a new instance of the <see cref="OptionallyRequiredBaseValidatorAttribute"/> class.</summary>
        protected OptionallyRequiredBaseValidatorAttribute() {
        }
    }
}
