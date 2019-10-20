namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class PasswordValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public PasswordValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("abc", "", ExpectedValidationResult.Pass)]
        [InlineData("123", "", ExpectedValidationResult.Pass)]
        [InlineData("", "", ExpectedValidationResult.Pass)]
        [InlineData(null, "", ExpectedValidationResult.Pass)]
        [InlineData("12", "Password Five minimum length is 3", ExpectedValidationResult.Fail)]
        [InlineData("123456789012345678901", "Password Five is longer than 20", ExpectedValidationResult.Fail)]
        public void WhenValidatingExtremelyWeakPasswordEnsureCorrectTestsResults(String value, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.PasswordFive = value;
            const String TargetPropertyName = nameof(_sut.PasswordFive);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("abc5defZ!", "", ExpectedValidationResult.Pass)]
        [InlineData("123456$zA", "", ExpectedValidationResult.Pass)]
        [InlineData("123456U$", "Password Two must contain at least one lower case character", ExpectedValidationResult.Fail)]
        [InlineData("123456u$", "Password Two must contain at least one upper case character", ExpectedValidationResult.Fail)]
        [InlineData("abcZ&&&&!", "Password Two must contain at least one digit character", ExpectedValidationResult.Fail)]
        [InlineData("", "Password Two was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData(null, "Password Two was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("$2U4a", "Password Two minimum length is 6", ExpectedValidationResult.Fail)]
        [InlineData("123456789012345678901aZ&", "Password Two is longer than 20", ExpectedValidationResult.Fail)]
        public void WhenValidatingPasswordEnsureCorrectTestsResults(String value, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.PasswordTwo = value;
            const String TargetPropertyName = nameof(_sut.PasswordTwo);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("abc5defZ!", "", ExpectedValidationResult.Pass)]
        [InlineData("123456$zA", "", ExpectedValidationResult.Pass)]
        [InlineData("123456U$", "Password One must contain at least one lower case character", ExpectedValidationResult.Fail)]
        [InlineData("123456u$", "Password One must contain at least one upper case character", ExpectedValidationResult.Fail)]
        [InlineData("abcZ&&&&!", "Password One must contain at least one digit character", ExpectedValidationResult.Fail)]
        [InlineData("abcZ1111dadf", "Password One must contain at least one of these special characters !@#$*^%&()-_+|", ExpectedValidationResult.Fail)]
        [InlineData("", "Password One was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData(null, "Password One was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("$2U4a", "Password One minimum length is 8", ExpectedValidationResult.Fail)]
        [InlineData("123456789012345678901aZ&", "Password One is longer than 20", ExpectedValidationResult.Fail)]
        public void WhenValidatingStrongPasswordEnsureCorrectTestsResults(String value, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.PasswordOne = value;
            const String TargetPropertyName = nameof(_sut.PasswordOne);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("abcdef", "", ExpectedValidationResult.Pass)]
        [InlineData("123456z", "", ExpectedValidationResult.Pass)]
        [InlineData("123456U", "Password Four must contain at least one lower case character", ExpectedValidationResult.Fail)]
        [InlineData("", "Password Four was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData(null, "Password Four was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("12U4a", "Password Four minimum length is 6", ExpectedValidationResult.Fail)]
        [InlineData("123456789012345678901Za", "Password Four is longer than 20", ExpectedValidationResult.Fail)]
        public void WhenValidatingVeryWeakPasswordEnsureCorrectTestsResults(String value, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.PasswordFour = value;
            const String TargetPropertyName = nameof(_sut.PasswordFour);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("abcdefZ", "", ExpectedValidationResult.Pass)]
        [InlineData("123456zA", "", ExpectedValidationResult.Pass)]
        [InlineData("123456U", "Password Three must contain at least one lower case character", ExpectedValidationResult.Fail)]
        [InlineData("123456u", "Password Three must contain at least one upper case character", ExpectedValidationResult.Fail)]
        [InlineData("", "Password Three was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData(null, "Password Three was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("12U4a", "Password Three minimum length is 6", ExpectedValidationResult.Fail)]
        [InlineData("123456789012345678901aZ", "Password Three is longer than 20", ExpectedValidationResult.Fail)]
        public void WhenValidatingWeakPasswordEnsureCorrectTestsResults(String value, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.PasswordThree = value;
            const String TargetPropertyName = nameof(_sut.PasswordThree);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [PasswordValidator(3, 20, LowerCaseCharacter.No, UpperCaseCharacter.No, DigitCharacter.No, SpecialCharacter.No, RequiredEntry.No)]
            public String PasswordFive { get; set; }

            [PasswordValidator(6, 20, LowerCaseCharacter.Yes, UpperCaseCharacter.No, DigitCharacter.No, SpecialCharacter.No)]
            public String PasswordFour { get; set; }

            [PasswordValidator(8, 20, LowerCaseCharacter.Yes, UpperCaseCharacter.Yes, DigitCharacter.Yes, SpecialCharacter.Yes)]
            public String PasswordOne { get; set; }

            [PasswordValidator(6, 20, LowerCaseCharacter.Yes, UpperCaseCharacter.Yes, DigitCharacter.No, SpecialCharacter.No)]
            public String PasswordThree { get; set; }

            [PasswordValidator(6, 20, LowerCaseCharacter.Yes, UpperCaseCharacter.Yes, DigitCharacter.Yes, SpecialCharacter.No)]
            public String PasswordTwo { get; set; }

            public Customer() {
            }
        }
    }
}
