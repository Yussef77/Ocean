namespace Oceanware.Ocean.ValidationRules {

    /// <summary>
    /// Represents the values for the enum ResourceErrorMessageFormatStringArgument.
    /// </summary>
    public enum ResourceErrorMessageFormatStringArgument {
        /// <summary>
        /// No arguments
        /// </summary>
        None,
        /// <summary>
        /// Uses the property name
        /// </summary>
        PropertyName,
        /// <summary>
        /// Uses the value
        /// </summary>
        Value,
        /// <summary>
        /// Uses the property name and value in this order
        /// </summary>
        PropertyNameValue,
        /// <summary>
        /// Uses the value and property name in this order
        /// </summary>
        ValuePropertyName
    }
}
