namespace Oceanware.Ocean.Extensions {

    using System;

    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Parses a property name and returns a friendly name. If <c>inString</c> is empty, or white space, returns <c>String.Empty</c>.
        /// </summary>
        /// <param name="inString">The string to get words from.</param>
        /// <returns>String with words parsed from in string with a space added between upper case characters.</returns>
        public static String GetWords(this String inString) {
            if (String.IsNullOrWhiteSpace(inString)) {
                return String.Empty;
            }
            var sb = new System.Text.StringBuilder(256);
            Boolean foundUpper = false;

            foreach (char c in inString) {
                if (foundUpper) {
                    if (char.IsUpper(c)) {
                        sb.Append(Constants.WhiteSpace);
                        sb.Append(c);
                    } else if (char.IsLetterOrDigit(c)) {
                        sb.Append(c);
                    }
                } else if (char.IsUpper(c)) {
                    foundUpper = true;
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
