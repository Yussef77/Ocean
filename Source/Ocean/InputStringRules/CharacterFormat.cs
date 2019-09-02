namespace Oceanware.Ocean.InputStringRules {
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents a CharacterFormat
    /// </summary>
    public class CharacterFormat {

        /// <summary>
        /// Gets the character casing.
        /// </summary>
        public CharacterCasing CharacterCasing { get; }

        /// <summary>
        /// Gets the remove multiple spaces.
        /// </summary>
        public RemoveSpace RemoveSpace { get; }

        /// <summary>Gets the value indicating whether to keep or remove the phone extension. Defaults to PhoneExtension.Keep.</summary>
        public PhoneExtension PhoneExtension { get; }

        /// <summary>Initializes a new instance of the <see cref="CharacterFormat"/> class.</summary>
        /// <param name="characterCasing">The character casing.</param>
        /// <param name="removeSpace">The remove multiple spaces.</param>
        /// <param name="phoneExtension">The phone extension.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value characterCasing is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value removeSpace is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value phoneExtension is not defined.</exception>
        public CharacterFormat(CharacterCasing characterCasing, RemoveSpace removeSpace, PhoneExtension phoneExtension) {
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCasing)) {
                throw new InvalidEnumArgumentException(nameof(characterCasing), (Int32)characterCasing, typeof(CharacterCasing));
            }
            if (!Enum.IsDefined(typeof(RemoveSpace), removeSpace)) {
                throw new InvalidEnumArgumentException(nameof(removeSpace), (Int32)removeSpace, typeof(RemoveSpace));
            }

            if (!Enum.IsDefined(typeof(PhoneExtension), phoneExtension)) {
                throw new InvalidEnumArgumentException(nameof(phoneExtension), (Int32)phoneExtension, typeof(PhoneExtension));
            }
            
            this.CharacterCasing = characterCasing;
            this.RemoveSpace = removeSpace;
            this.PhoneExtension = phoneExtension;
        }

    }
}
