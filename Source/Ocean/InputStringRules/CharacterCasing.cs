namespace Oceanware.Ocean.InputStringRules {

    /// <summary>
    /// Specifies the input String character casing
    /// </summary>
    public enum CharacterCasing {

        /// <summary>
        /// No character casing applied
        /// </summary>
        None,

        /// <summary>
        /// All characters converted to lower case
        /// </summary>
        LowerCase,

        /// <summary>
        /// Formats phone number entries (###) ###-### extension text, no case changes applied
        /// </summary>
        PhoneNoProperName,

        /// <summary>
        /// Formats phone number entries (###) ###-### Extension Text, proper name casing applied
        /// </summary>
        PhoneProperName,

        /// <summary>
        /// Formats phone number entries (###) ###-### EXTENSION TEXT, upper casing applied
        /// </summary>
        PhoneUpper,

        /// <summary>
        /// Formats phone number entries ###-###-### extension text, no case changes applied
        /// </summary>
        PhoneWithDashesNoProperName,

        /// <summary>
        /// Formats phone number entries ###-###-### Extension Text, proper name casing applied
        /// </summary>
        PhoneWithDashesProperName,

        /// <summary>
        /// Formats phone number entries ###-###-### EXTENSION TEXT, upper casing applied
        /// </summary>
        PhoneWithDashesUpper,

        /// <summary>
        /// Formats phone number entries ###.###.### extension text, no case changes applied
        /// </summary>
        PhoneWithDotsNoProperName,

        /// <summary>
        /// Formats phone number entries ###.###.### Extension Text, proper name casing applied
        /// </summary>
        PhoneWithDotsProperName,

        /// <summary>
        /// Formats phone number entries ###.###.### EXTENSION TEXT, upper casing applied
        /// </summary>
        PhoneWithDotsUpper,

        /// <summary>
        /// Proper name casing applied
        /// </summary>
        ProperName,

        /// <summary>
        /// All characters converted to upper case
        /// </summary>
        UpperCase
    }
}
