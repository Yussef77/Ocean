namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class StringLengthValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public StringLengthValidatorFixture() {
            _sut = new Customer();
        }

        [Fact]
        public void WhenAllowNullStringIsYesAndValueIsNullValidationPasses() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineOne = null;
            const String TargetPropertyName = nameof(_sut.AddressLineOne);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenMaximumLengthValidationFailsErrorMessageUsesFriendlyName() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AlternateLastName = "012345678901234567890123456789";
            const String TargetPropertyName = nameof(_sut.AlternateLastName);
            const String ExpectedMessage = "Alias Last Name is longer than 25.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenMinimumLengthValidationFailsErrorMessageUsesFriendlyName() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AlternateLastName = "1234";
            const String TargetPropertyName = nameof(_sut.AlternateLastName);
            const String ExpectedMessage = "Alias Last Name minimum length is 5.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("", ValidationConstants.Update, "Last Name minimum length is 5.", ExpectedValidationResult.Fail)]
        [InlineData("", ValidationConstants.Delete, "", ExpectedValidationResult.Pass)]
        [InlineData("12345", ValidationConstants.Update, "", ExpectedValidationResult.Pass)]
        [InlineData("012345678901234567890123456789", ValidationConstants.Update, "Last Name is longer than 25.", ExpectedValidationResult.Fail)]
        [InlineData("012345678901234567890", ValidationConstants.Update, "", ExpectedValidationResult.Pass)]
        public void WhenRuleSetIsProvidedEnsureCorrectTestsAreRun(String value, String ruleSet, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.ActiveRuleSet = ruleSet;
            _sut.LastName = value;
            const String TargetPropertyName = nameof(_sut.LastName);

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, expectedMessage);
        }

        [Fact]
        public void WhenValidationFailsAndErrorMessageResourceIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineTwo = null;
            const String TargetPropertyName = nameof(_sut.AddressLineTwo);
            const String ExpectedMessage = "Test resource value.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsAndOverrideErrorMessageIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.Review = "123456";
            const String TargetPropertyName = nameof(_sut.Review);
            const String ExpectedMessage = "Override error message.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [StringLengthValidator(5, 25, AllowNullString.Yes)]
            public String AddressLineOne { get; set; }

            [StringLengthValidator(5, AllowNullString.No, ErrorMessageResourceName = "TestResourceKey", ErrorMessageResourceType = typeof(Properties.Resources))]
            public String AddressLineTwo { get; set; } = String.Empty;

            [StringLengthValidator(5, 25, FriendlyName = "Alias Last Name")]
            public String AlternateLastName { get; set; } = String.Empty;

            [StringLengthValidator(5, 25, RuleSet = ValidationConstants.Update)]
            public String LastName { get; set; } = String.Empty;

            [StringLengthValidator(15, AllowNullString.Yes, ProperCasePropertyName = ProperCasePropertyName.No)]
            public String MiddleName { get; set; } = String.Empty;

            [StringLengthValidator(5, AllowNullString.No, OverrideErrorMessage = "Override error message.")]
            public String Review { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
