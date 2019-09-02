namespace Oceanware.Ocean.InputStringRules {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Represents CharacterCasingRulesManager, maintains character casing rules for a business Object.
    /// </summary>
    public class CharacterFormattingRulesManager {
        Dictionary<String, CharacterFormat> _characterCasingRulesList;

        /// <summary>
        /// Gets the has rules.
        /// </summary>
        /// <value>Returns <c>True</c> if this <c>CharacterFormattingRulesManager</c> has rules.</value>
        public Boolean HasRules { get { return this.RulesDictionary.Count > 0; } }

        /// <summary>
        /// Gets RulesDictionary that contains all defined rules for this Object.
        /// </summary>
        public Dictionary<String, CharacterFormat> RulesDictionary => _characterCasingRulesList ?? (_characterCasingRulesList = new Dictionary<String, CharacterFormat>());

        /// <summary>
        /// Gets the rules loaded.
        /// </summary>
        /// <value>The rules loaded.</value>
        public Boolean RulesLoaded { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterFormattingRulesManager"/> class.
        /// </summary>
        public CharacterFormattingRulesManager() {
        }

        /// <summary>
        /// Adds a CharacterCasing Formatting rule to the list of rules to be executed when the property is changed.
        /// </summary>
        /// <param name="characterFormattingAttribute">The character formatting attribute.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="ArgumentNullException">Thrown when characterFormattingAttribute is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public void AddRule(CharacterFormattingAttribute characterFormattingAttribute, String propertyName) {
            if (characterFormattingAttribute is null) {
                throw new ArgumentNullException(nameof(characterFormattingAttribute));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            this.RulesDictionary.Add(propertyName, new CharacterFormat(characterFormattingAttribute.CharacterCasing, characterFormattingAttribute.RemoveSpace, characterFormattingAttribute.PhoneExtension));
        }

        /// <summary>Adds a CharacterCasing Formatting rule to the list of rules to be executed when the property is changed.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="characterCasing">The desired character casing formatting.</param>
        /// <param name="removeSpace">The remove multiple space.</param>
        /// <param name="phoneExtension">The phone extension.</param>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value characterCasing is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value removeSpace is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value phoneExtension is not defined.</exception>
        public void AddRule(String propertyName, CharacterCasing characterCasing, RemoveSpace removeSpace, PhoneExtension phoneExtension) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCasing)) {
                throw new InvalidEnumArgumentException(nameof(characterCasing), (Int32)characterCasing, typeof(CharacterCasing));
            }
            if (!Enum.IsDefined(typeof(RemoveSpace), removeSpace)) {
                throw new InvalidEnumArgumentException(nameof(removeSpace), (Int32)removeSpace, typeof(RemoveSpace));
            }
            if (!Enum.IsDefined(typeof(PhoneExtension), phoneExtension)) {
                throw new InvalidEnumArgumentException(nameof(phoneExtension), (Int32)phoneExtension, typeof(PhoneExtension));
            }

            this.RulesDictionary.Add(propertyName, new CharacterFormat(characterCasing, removeSpace, phoneExtension));
        }

        /// <summary>
        /// Returns the CharacterCasing rule for a the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see cref="CharacterCasing"/> from the rules dictionary for the property name.</returns>
        /// <exception cref="ArgumentException">propertyName cannot be null or whitespace.</exception>
        public CharacterFormat GetRuleForProperty(String propertyName) {
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(propertyName));
            }
            return this.RulesDictionary.ContainsKey(propertyName) ? this.RulesDictionary[propertyName] : null;
        }

        /// <summary>Sets the <c>RulesLoaded</c> to <c>True</c>.</summary>
        public void SetRulesLoaded() {
            this.RulesLoaded = true;
        }
    }
}
