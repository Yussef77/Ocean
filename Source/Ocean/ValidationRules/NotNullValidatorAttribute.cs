namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Reflection;

    /// <summary>
    /// Class NotNullValidatorAttribute. This class cannot be inherited. Used to verify both Nullable and non-nullable properties are not null or DBNull.
    /// Derives from the <see cref="Oceanware.OceanValidation.BaseValidatorAttribute" />
    /// </summary>
    /// <seealso cref="Oceanware.OceanValidation.BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class NotNullValidatorAttribute : BaseValidatorAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidatorAttribute"/> class.
        /// </summary>
        public NotNullValidatorAttribute() {
        }

        /// <summary>
        /// Validates the property, Error message is set in the <seealso cref="FinalErrorMessage" /> if the validation fails.
        /// </summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <remarks>
        ///<para>Boxing and Unboxing</para>
        ///<para>When a nullable type is boxed, the common language runtime automatically boxes the underlying value of the Nullable Object, not the Nullable Object itself. That is, if the HasValue property is true, the contents of the Value property is boxed. If the HasValue property is false, a null reference (Nothing in Visual Basic) is boxed.</para>
        ///<para>When the underlying value of a nullable type is un-boxed, the common language runtime creates a new Nullable structure initialized to the underlying value.</para>
        /// </remarks>
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

            //this handles both Nullable and standard uninitialized values
            if (targetValue == null) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.NotNullIsNullFormat, displayName), displayName, targetValue);
                return false;
            }

            if (Convert.IsDBNull(targetValue)) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.NotNullIsDBNullFormat, displayName), displayName, targetValue);
                return false;
            }

            return true;
        }
    }
}
