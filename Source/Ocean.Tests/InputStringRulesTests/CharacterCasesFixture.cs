namespace Ocean.Tests.InputStringRulesTests {

    using System;
    using System.Reflection;
    using Oceanware.Ocean.InputStringRules;
    using Xunit;

    public class CharacterCasesFixture : FixtureBase {
        readonly CustomerCC _sut;

        static CharacterCasesFixture() {
            Type type = typeof(CustomerCC);
            var mgr = SharedCharacterFormattingRules.GetManager(type);
            foreach (PropertyInfo prop in type.GetProperties()) {
                foreach (CharacterFormattingAttribute atr in prop.GetCustomAttributes(typeof(CharacterFormattingAttribute), false)) {
                    mgr.AddRule(prop.Name, atr.CharacterCasing, atr.RemoveSpace, atr.PhoneExtension);
                }
            }
        }

        public CharacterCasesFixture() {
            _sut = new CustomerCC();
        }

        [Fact]
        public void WhenCustomCharacterCaseRulesAreAddedTheyAreHonored() {
            // arrange
            var defaultRules = CharacterCasingChecks.GetChecks();
            defaultRules.Add(new CharacterCasingCheck("Abc", "ABC"));
            CharacterCasingChecks.SetGetChecksSource(() => defaultRules);
            const String TargetPropertyName = nameof(_sut.FirstName);
            const String TargetPropertyValue = "abc";
            const String ExpectedValue = "ABC";

            // act assert
            base.RunValidation(TargetPropertyValue, ExpectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("john   smith", "john   smith")]
        [InlineData("in", "in")]
        public void WhenNoneDoNotChangeValue(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.UserDefined);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("5555551212 akd  asd  ", "(555) 555-1212")]
        [InlineData("555.555.1212   EX 125", "(555) 555-1212")]
        public void WhenPhoneExtensionRemoveEnsureExtensionIsRemovedValue(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.CellFour);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("5555551212 abX", "(555) 555-1212  abX")]
        [InlineData("555.555.1212   deF gh", "(555) 555-1212  deF gh")]
        public void WhenPhoneNoPropertyNameFormatsCorrectly(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.CellTwo);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("5555551212", "(555) 555-1212")]
        [InlineData("555.555.1212", "(555) 555-1212")]
        public void WhenPhoneProperNameFormatsCorrectly(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.Cell);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("5555551212 abx", "(555) 555-1212  ABX")]
        [InlineData("555.555.1212 def gh", "(555) 555-1212  DEF GH")]
        public void WhenPhoneUpperFormatsCorrectly(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.CellThree);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("john   smith", "John Smith")]
        [InlineData("wpf  framework   4.0", "WPF Framework 4.0")]
        public void WhenProperNameAndRemoveMultipleSpacesEnsureCorrectValue(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.LastName);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("john smith", "John Smith")]
        [InlineData("wpf framework", "WPF Framework")]
        public void WhenProperNameEnsureValueIsProperName(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.FirstName);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("abc   124  zebra", "ABC124ZEBRA")]
        [InlineData("   ssddfs 1111 asd", "SSDDFS1111ASD")]
        public void WhenUpperAndRemoveAllSpacesEnsureUpperAndAllSpacesRemovedFromValue(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.Account);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        [Theory]
        [InlineData("john   smith", "JOHN   SMITH")]
        [InlineData("in", "IN")]
        public void WhenUpperCaseEnsureCorrectValue(String value, String expectedValue) {
            // arrange
            const String TargetPropertyName = nameof(_sut.State);

            // act assert
            base.RunValidation(value, expectedValue, TargetPropertyName, _sut);
        }

        class CustomerCC {

            [CharacterFormatting(CharacterCasing.UpperCase, RemoveSpace = RemoveSpace.AllSpaces)]
            public String Account { get; set; }

            [CharacterFormatting(CharacterCasing.PhoneProperName)]
            public String Cell { get; set; }

            [CharacterFormatting(CharacterCasing.PhoneProperName, PhoneExtension = PhoneExtension.Remove)]
            public String CellFour { get; set; }

            [CharacterFormatting(CharacterCasing.PhoneUpper)]
            public String CellThree { get; set; }

            [CharacterFormatting(CharacterCasing.PhoneNoProperName)]
            public String CellTwo { get; set; }

            [CharacterFormatting(CharacterCasing.ProperName)]
            public String FirstName { get; set; }

            [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
            public String LastName { get; set; }

            [CharacterFormatting(CharacterCasing.UpperCase)]
            public String State { get; set; }

            [CharacterFormatting(CharacterCasing.None)]
            public String UserDefined { get; set; }

            public CustomerCC() {
            }
        }
    }
}
