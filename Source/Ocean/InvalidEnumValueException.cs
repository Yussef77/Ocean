namespace Oceanware.Ocean {

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class InvalidEnumValueException.
    /// Derives from the <see cref="Exception" />
    /// </summary>
    public class InvalidEnumValueException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEnumValueException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        public InvalidEnumValueException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidEnumValueException"/> class.</summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The invalid enum value.</param>
        public InvalidEnumValueException(Type enumType, Object value) : base(String.Format(Strings.InvalidEnumValueExceptionMessageFormat, value, enumType.Name)) {
        }
    }
}
