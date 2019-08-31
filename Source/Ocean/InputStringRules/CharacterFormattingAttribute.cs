namespace Oceanware.Ocean.InputStringRules {

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents CharacterFormattingAttribute, when applied to a business entity class property, that property will have its case corrected according to the CharacterCasing assigned.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CharacterFormattingAttribute : Attribute {
        Boolean _isPhoneCasing;
        PhoneExtension _phoneExtension = PhoneExtension.Keep;

        /// <summary>
        /// Get or sets the CharacterCasing that will be applied to this property when the property is updated.
        /// </summary>
        public CharacterCasing CharacterCasing { get; }

        /// <summary>Gets or sets a value indicating whether to keep or remove the phone extension. Defaults to PhoneExtension.Keep.</summary>
        /// <value>The phone extension.</value>
        /// <exception cref="T:System.InvalidOperationException">Thrown when property is set an Character Case Rule is not a Phone Rule.</exception>
        public PhoneExtension PhoneExtension {
            get => _phoneExtension;
            set {
                if (!_isPhoneCasing) {
                    throw new InvalidOperationException($"{nameof(PhoneExtension)} only used on Phone Character Case Rules.");
                }
                _phoneExtension = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to remove multiple spaces. Defaults to RemoveSpace.None.
        /// </summary>
        public RemoveSpace RemoveSpace { get; set; } = RemoveSpace.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterFormattingAttribute"/> class.
        /// </summary>
        /// <param name="characterCasing">The character casing.</param>
        /// <exception cref="InvalidEnumArgumentException">characterCasing is not a member of CharacterCasing enum.</exception>
        /// <exception cref="InvalidEnumArgumentException">removeSpace is not a member of RemoveSpace enum.</exception>
        public CharacterFormattingAttribute(CharacterCasing characterCasing) {
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCasing)) {
                throw new InvalidEnumArgumentException(nameof(characterCasing), (Int32)characterCasing, typeof(CharacterCasing));
            }

            this.CharacterCasing = characterCasing;
            switch (this.CharacterCasing) {
                case CharacterCasing.PhoneNoProperName:
                case CharacterCasing.PhoneProperName:
                case CharacterCasing.PhoneUpper:
                case CharacterCasing.PhoneWithDashesNoProperName:
                case CharacterCasing.PhoneWithDashesProperName:
                case CharacterCasing.PhoneWithDashesUpper:
                case CharacterCasing.PhoneWithDotsNoProperName:
                case CharacterCasing.PhoneWithDotsProperName:
                case CharacterCasing.PhoneWithDotsUpper:
                    _isPhoneCasing = true;
                    break;
            }
        }
    }
}
