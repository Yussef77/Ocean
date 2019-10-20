namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean;
    using Oceanware.Ocean.Extensions;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class BankRoutingNumberValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public BankRoutingNumberValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("0A3456789")]
        [InlineData("12345678B")]
        public void WhenAnyCharacterIsNotADigitTestShouldFail(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            String ExpectedMessage = String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersCharactersMustBeNumericFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("823456789")]
        [InlineData("223456789")]
        public void WhenFirstCharacterIsNotAZeroOrOneTestShouldFail(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            String ExpectedMessage = String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersFirstDigitMustBeZeorOrOneFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("0123456789")]
        public void WhenLengthIsNotNineTestShouldFail(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            String ExpectedMessage = String.Format(Strings.ValueIsNotAValidBankRoutingNumberAllBankRoutingNumbersAreNineDigitsInLengthFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenPropertyIsNotAStringTestShouldFail() {
            // arrange
            _sut.JBCBankRoutingNumber = 125555;
            const String TargetPropertyName = nameof(_sut.JBCBankRoutingNumber);

            // act assert
            Assert.Throws<InvalidOperationException>(() => _modelRulesInvoker.CheckAllValidationRulesForProperty(_sut, TargetPropertyName));
        }

        [Theory]
        [InlineData("013456789")]
        [InlineData("123456786")]
        public void WhenRoutingNumberIsInvalidTestShouldFail(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            String ExpectedMessage = String.Format(Strings.ValueIsNotAValidBankRoutingNumberFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("071000013")]
        [InlineData("065400137")]
        public void WhenRoutingNumberIsValidTestShouldPass(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            const String ExpectedMessage = "";
            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryNoTestShouldPass(String value) {
            // arrange
            _sut.USBankRoutingNumber = value;
            const String TargetPropertyName = nameof(_sut.USBankRoutingNumber);
            String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryYesInvalidTestShouldFail(String value) {
            // arrange
            _sut.NavyFederalRoutingNumber = value;
            const String TargetPropertyName = nameof(_sut.NavyFederalRoutingNumber);
            var targetPropertyNameFriendlyName = nameof(_sut.NavyFederalRoutingNumber).GetWords();
            String ExpectedMessage = $"{targetPropertyNameFriendlyName} was null, DBNull, or empty string but was required.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingDefaultConstructorEntryIsRequiredInvalidTestShouldFail(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            var targetPropertyNameFriendlyName = nameof(_sut.ChaseBankRoutingNumber).GetWords();
            String ExpectedMessage = $"{targetPropertyNameFriendlyName} was null, DBNull, or empty string but was required.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {

            public String ActiveRuleSet { get; set; }

            [BankRoutingNumberValidator()]
            public String ChaseBankRoutingNumber { get; set; } = String.Empty;

            [BankRoutingNumberValidator()]
            public Int32 JBCBankRoutingNumber { get; set; }

            [BankRoutingNumberValidator(RequiredEntry.Yes)]
            public String NavyFederalRoutingNumber { get; set; } = String.Empty;

            [BankRoutingNumberValidator(RequiredEntry.No)]
            public String USBankRoutingNumber { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
