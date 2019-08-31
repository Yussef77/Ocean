namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>Class StringLengthValidatorAttribute. This class cannot be inherited. Used to validate a strings length.
    /// Derives from the <see cref="T:Oceanware.OceanValidation.BaseValidatorAttribute"/></summary>
    /// <seealso cref="Oceanware.OceanValidation.BaseValidatorAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class StringLengthValidatorAttribute : BaseValidatorAttribute {
        const Int32 MinusOne = -1;
        const Int32 One = 1;
        const Int32 Zero = 0;

        /// <summary>
        /// Gets the allow null string. Default value is No.
        /// </summary>
        /// <value>The allow null string.</value>
        public AllowNullString AllowNullString { get; } = AllowNullString.No;

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public Int32 MaximumLength { get; }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>The minimum length.</value>
        public Int32 MinimumLength { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class. <seealso cref="AllowNullString" /> defaults to <c>AllowNullString.No</c>.
        /// </summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength less than or equal to Minimum Length.</exception>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength)
            : this(minimumLength, maximumLength, AllowNullString.No) { }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.OceanValidation.StringLengthValidatorAttribute"/> class.</summary>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">Allow a null string, yes or no.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength less than or equal to Minimum Length.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value allowNullString is not defined.</exception>
        public StringLengthValidatorAttribute(Int32 minimumLength, Int32 maximumLength, AllowNullString allowNullString) {
            if (maximumLength < One) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanOrEqualToMinimumLength);
            }
            if (!Enum.IsDefined(typeof(AllowNullString), allowNullString)) {
                throw new InvalidEnumArgumentException(nameof(allowNullString), (Int32)allowNullString, typeof(AllowNullString));
            }

            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
            this.AllowNullString = allowNullString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="allowNullString">Allow a null string, yes or no.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        public StringLengthValidatorAttribute(Int32 maximumLength, AllowNullString allowNullString)
            : this(MinusOne, maximumLength, allowNullString) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLengthValidatorAttribute"/> class. <seealso cref="AllowNullString" /> defaults to <c>AllowNullString.No</c>.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than one.</exception>
        public StringLengthValidatorAttribute(Int32 maximumLength)
            : this(MinusOne, maximumLength, AllowNullString.No) { }

        /// <summary>Validates the string property. Note: all string values are trimmed of white space. Error message is set in the <seealso cref="BaseValidatorAttribute.FinalErrorMessage"/> property.</summary>
        /// <param name="target">The target instance to validate.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <c>true</c> if the target property is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when propertyName is null or whitespace or empty string.</exception>
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

            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            var targetValue = propertyInfo.GetValue(target, null);
            var targetStringValue = Convert.ToString(targetValue).Trim();

            if (this.AllowNullString == AllowNullString.Yes && (targetValue == null || Convert.IsDBNull(targetValue))) {
                return true;
            }

            var displayName = base.ResolveDisplayName(propertyName, this.FriendlyName, this.ProperCasePropertyName);

            if (this.AllowNullString == AllowNullString.No && (targetValue == null || Convert.IsDBNull(targetValue))) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.NullValueIsNotAllowedFormat, displayName), displayName, targetValue);
                return false;
            }

            if (this.MinimumLength > Zero && targetStringValue.Length < this.MinimumLength) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.MinimumLengthIsFormat, displayName, this.MinimumLength), displayName, targetValue);
                return false;
            }

            if (targetStringValue.Length > this.MaximumLength) {
                this.FinalErrorMessage = base.CreateFailedValidationMessage(String.Format(Strings.IsLongerThanFormat, displayName, this.MaximumLength), displayName, targetValue);
                return false;
            }

            return true;
        }
    }
}
