namespace Oceanware.Ocean {

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class ArgumentNullEmptyWhiteSpaceException.
    /// Derives from the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ArgumentNullEmptyWhiteSpaceException : Exception {

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public String ParameterName { get; } = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullEmptyWhiteSpaceException"/> class.
        /// </summary>
        public ArgumentNullEmptyWhiteSpaceException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullEmptyWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        public ArgumentNullEmptyWhiteSpaceException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullEmptyWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public ArgumentNullEmptyWhiteSpaceException(String message, Exception innerException) : base(message, innerException) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullEmptyWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when parameterName is null, empty, or white space.</exception>
        public ArgumentNullEmptyWhiteSpaceException(String parameterName, String message) : base(message) {
            if (String.IsNullOrWhiteSpace(parameterName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(parameterName));
            }
            this.ParameterName = parameterName;
        }

        /// <summary>
        /// Preferred constructor because a localized default message is created. Initializes a new instance of the <see cref="ArgumentNullEmptyWhiteSpaceException"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when parameterName is null, empty, or white space.</exception>
        public ArgumentNullEmptyWhiteSpaceException(String parameterName) : base(String.Format(Strings.StringIsNullEmptyOrWhiteSpaceFormat, parameterName)) {
            if (String.IsNullOrWhiteSpace(parameterName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(parameterName));
            }
            this.ParameterName = parameterName;
        }
    }
}
