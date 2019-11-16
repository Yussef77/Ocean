namespace BlazorAppShared.Model {

    using System;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.ValidationRules;

    public class Person {

        public String ActiveRuleSet { get; set; }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 64, RuleSet = ValidationConstants.InsertUpdate)]
        public String AddressLineOne { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(2, 64, RuleSet = ValidationConstants.InsertUpdate)]
        public String City { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(2, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String FirstName { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String LastName { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.PhoneWithDashesProperName)]
        [StringLengthValidator(25)]
        public String Phone { get; set; }

        [USStateAbbreviationValidator]
        public String State { get; set; } = String.Empty;

        [RegularExpressionValidator(RegularExpressionPatternType.USZipCode)]
        public String Zip { get; set; } = String.Empty;

        public Person() {
        }
    }
}
