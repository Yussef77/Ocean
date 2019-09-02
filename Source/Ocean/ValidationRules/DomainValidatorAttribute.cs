namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class DomainValidatorAttribute. This class cannot be inherited. Used to validate the property value is contained in the domain.
    /// Derives from the <see cref="OptionallyRequiredBaseValidatorAttribute" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DomainValidatorAttribute : OptionallyRequiredBaseValidatorAttribute {
        const Int32 Zero = 0;

        /// <summary>
        /// Gets or sets the data, AKA the Domain.
        /// </summary>
        /// <value>The data.</value>
        public String[] Data { get; }

        /// <summary>Initializes a new instance of the <see cref="DomainValidatorAttribute"/> class.</summary>
        /// <param name="requiredEntry">The required entry.</param>
        /// <param name="data">The data is a param array of strings separated by a comma that represent each valid value in the domain.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value requiredEntry is not defined.</exception>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when domain does not have at least one value.</exception>
        /// <example>
        /// If valid values are Top, Bottom, Right and Left then the attribute signature would be:  "Top", "Bottom", "Right", "Left"
        ///     <code>
        ///         [DomainValidator(RequiredEntry.Yes, "Top", "Bottom", "Right", "Left")]
        ///     </code>
        ///     <code>
        ///         [DomainValidator(RequiredEntry.No, "Top", "Bottom", "Right", "Left")]
        ///     </code>
        ///     <code>
        ///         [DomainValidator("Top", "Bottom", "Right", "Left")]
        ///     </code>
        /// </example>
        public DomainValidatorAttribute(RequiredEntry requiredEntry, params String[] data) {
            if (!Enum.IsDefined(typeof(RequiredEntry), requiredEntry)) {
                throw new InvalidEnumArgumentException(nameof(requiredEntry), (Int32)requiredEntry, typeof(RequiredEntry));
            }
            if (data is null) {
                throw new ArgumentNullException(nameof(data));
            }
            if (data.Length == Zero) {
                throw new InvalidOperationException($"{nameof(data)} is empty, must have at least one value.");
            }

            this.RequiredEntry = requiredEntry;
            this.Data = new String[data.Length];
            data.CopyTo(this.Data, Zero);
        }

        /// <summary>Initializes a new instance of the <see cref="DomainValidatorAttribute"/> class.</summary>
        /// <param name="data">The data is a param array of strings separated by a comma that represent each valid value in the domain.</param>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when domain does not have at least one value.</exception>
        /// <example>
        /// If valid values are Top, Bottom, Right and Left then the attribute signature would be:  "Top", "Bottom", "Right", "Left"
        ///     <code>
        ///         [DomainValidator(RequiredEntry.Yes, "Top", "Bottom", "Right", "Left")]
        ///     </code>
        ///     <code>
        ///         [DomainValidator(RequiredEntry.No, "Top", "Bottom", "Right", "Left")]
        ///     </code>
        ///     <code>
        ///         [DomainValidator("Top", "Bottom", "Right", "Left")]
        ///     </code>
        /// </example>
        public DomainValidatorAttribute(params String[] data) : this(RequiredEntry.Yes, data) {
        }

        /// <summary>
        /// Validates the property, Error message is set in the <seealso cref="BaseValidatorAttribute.FinalErrorMessage" /> if the validation fails.
        /// </summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public override Boolean IsValid(Object target, String propertyName) {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            this.FinalErrorMessage = String.Empty;

            if (base.SkipValidation(target, this.RuleSet)) {
                return true;
            }

            var displayName = base.ResolveDisplayName(propertyName, this.FriendlyName, this.ProperCasePropertyName);

            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            var targetValue = propertyInfo.GetValue(target, null);

            if (this.RequiredEntry == RequiredEntry.Yes) {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    this.FinalErrorMessage = base.CreateFailedValidationMessage(Strings.ValueWasNullOrDBNullOrEmptyStringButWasRequired, displayName, targetValue);
                    return false;
                }
            } else {
                if (targetValue == null || String.IsNullOrWhiteSpace(Convert.ToString(targetValue).Trim()) || Convert.IsDBNull(targetValue)) {
                    return true;
                }
            }

            String targetStringValue = Convert.ToString(targetValue);

            if (this.Data.Any(s => String.Compare(s, targetStringValue, StringComparison.OrdinalIgnoreCase) == Zero)) {
                return true;
            }

            var validValues = String.Join(", ", this.Data);
            this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.DomainValidationValueDidNotMatchAnyAcceptableValueFormat, displayName, targetStringValue, validValues), displayName, targetValue);
            return false;
        }
    }
}
