namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class BaseValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public BaseValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("123456", ValidationConstants.Update, "City is longer than 5.", ExpectedValidationResult.Fail)]
        [InlineData("", ValidationConstants.Delete, "", ExpectedValidationResult.Pass)]
        [InlineData("12345", ValidationConstants.Update, "", ExpectedValidationResult.Pass)]
        public void WhenRuleSetIsProvidedEnsureCorrectTestsAreRun(String value, String ruleSet, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.ActiveRuleSet = ruleSet;
            _sut.City = value;
            const String TargetPropertyName = nameof(_sut.City);

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, expectedMessage);
        }

        [Fact]
        public void WhenValidationFailsAndErrorMessageResourceIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineOne = null;
            const String TargetPropertyName = nameof(_sut.AddressLineOne);
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

        [Fact]
        public void WhenValidationFailsAndResourceErrorMessageFormatStringArgument_PropertyNameIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineThree = null;
            const String TargetPropertyName = nameof(_sut.AddressLineThree);
            const String ExpectedMessage = "Address Line Three error.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsAndResourceErrorMessageFormatStringArgument_PropertyNameValueIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineTwo = null;
            const String TargetPropertyName = nameof(_sut.AddressLineTwo);
            const String ExpectedMessage = "Address Line Two error null.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsAndResourceErrorMessageFormatStringArgument_ValuePropertyNameIsProvidedUseForErrorMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AddressLineFive = null;
            const String TargetPropertyName = nameof(_sut.AddressLineFive);
            const String ExpectedMessage = "null error Address Line Five.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsErrorMessageUsesAdditionalMessage() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.LastName = "012345678901234567890123456789";
            const String TargetPropertyName = nameof(_sut.LastName);
            const String ExpectedMessage = "Additional message text. Last Name is longer than 5.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsErrorMessageUsesFriendlyName() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.AlternateLastName = "012345678901234567890123456789";
            const String TargetPropertyName = nameof(_sut.AlternateLastName);
            const String ExpectedMessage = "Alias Last Name is longer than 5.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidationFailsErrorMessageUsesPropertyNameWithoutProperCasing() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Update;
            _sut.MiddleName = "012345678901234567890123456789";
            const String TargetPropertyName = nameof(_sut.MiddleName);
            const String ExpectedMessage = "MiddleName is longer than 5.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {

            public String ActiveRuleSet { get; set; }

            [StringLengthValidator(5, ErrorMessageResourceName = "ValueMessagePropertyNameFormat", ErrorMessageResourceType = typeof(Properties.Resources), ResourceErrorMessageFormatStringArgument = ResourceErrorMessageFormatStringArgument.ValuePropertyName)]
            public String AddressLineFive { get; set; } = String.Empty;

            [StringLengthValidator(5, ErrorMessageResourceName = "ValueMessageFormat", ErrorMessageResourceType = typeof(Properties.Resources), ResourceErrorMessageFormatStringArgument = ResourceErrorMessageFormatStringArgument.Value)]
            public String AddressLineFour { get; set; } = String.Empty;

            [StringLengthValidator(5, ErrorMessageResourceName = "TestResourceKey", ErrorMessageResourceType = typeof(Properties.Resources))]
            public String AddressLineOne { get; set; }

            [StringLengthValidator(5, ErrorMessageResourceName = "PropertyNameMessageFormat", ErrorMessageResourceType = typeof(Properties.Resources), ResourceErrorMessageFormatStringArgument = ResourceErrorMessageFormatStringArgument.PropertyName)]
            public String AddressLineThree { get; set; } = String.Empty;

            [StringLengthValidator(5, ErrorMessageResourceName = "PropertyNameMessageValueFormat", ErrorMessageResourceType = typeof(Properties.Resources), ResourceErrorMessageFormatStringArgument = ResourceErrorMessageFormatStringArgument.PropertyNameValue)]
            public String AddressLineTwo { get; set; } = String.Empty;

            [StringLengthValidator(5, FriendlyName = "Alias Last Name")]
            public String AlternateLastName { get; set; } = String.Empty;

            [StringLengthValidator(5, RuleSet = ValidationConstants.InsertUpdate)]
            public String City { get; set; } = String.Empty;

            [StringLengthValidator(5, AdditionalMessage = "Additional message text.")]
            public String LastName { get; set; } = String.Empty;

            [StringLengthValidator(5, ProperCasePropertyName = ProperCasePropertyName.No)]
            public String MiddleName { get; set; } = String.Empty;

            [StringLengthValidator(5, OverrideErrorMessage = "Override error message.")]
            public String Review { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
