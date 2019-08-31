namespace Oceanware.Ocean.ValidationRules {

    /// <summary>Represents the values for the enum RegularExpressionPatternType.</summary>
    public enum RegularExpressionPatternType {

        /// <summary>
        /// Custom user supplied pattern
        /// </summary>
        Custom,

        /// <summary>
        /// Email pattern
        /// </summary>
        Email,

        /// <summary>
        /// IP address pattern
        /// </summary>
        IPAddress,

        /// <summary>
        /// Social security number pattern ###-##-####
        /// </summary>
        SSN,

        /// <summary>
        /// Url pattern, does not allow Urls like http://localhost:5000
        /// </summary>
        URL,

        /// <summary>
        /// Url is well formed.  URL is valid is the Url passes either the standard URL validation or Uri.IsWellFormedUriString for UriKind.Absolute
        /// </summary>
        URLIsWellFormed,

        /// <summary>
        /// US phone number pattern (###) ###-#### ###-###-#### ###.###.####
        /// </summary>
        USPhoneNumber,

        /// <summary>
        /// US zip code pattern ##### #####-####
        /// </summary>
        USZipCode
    }
}
