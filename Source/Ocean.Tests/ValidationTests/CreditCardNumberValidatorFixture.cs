namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean;
    using Oceanware.Ocean.Extensions;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class CreditCardNumberValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public CreditCardNumberValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("0A3456789")]
        [InlineData("12345678B")]
        public void WhenAnyCreditCardCharacterIsNotADigitTestShouldFail(String value) {
            // arrange
            _sut.ChaseCreditCardNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseCreditCardNumber);
            String ExpectedMessage = String.Format(Strings.CreditCardNumberIsNotAValidCreditCardNumberOnlyNumericInputIsAllowedFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("013456789")]
        [InlineData("123456786")]
        public void WhenCreditCardNumberIsInvalidTestShouldFail(String value) {
            // arrange
            _sut.ChaseCreditCardNumber = value;
            String TargetPropertyValue = value;
            const String TargetPropertyName = nameof(_sut.ChaseCreditCardNumber);
            String ExpectedMessage = String.Format(Strings.CreditCardNumberIsNotAValidCreditCardNumberFormat, TargetPropertyName.GetWords(), TargetPropertyValue);

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("3566002020360505")]
        [InlineData("5555555555554444")]
        [InlineData("5105105105105100")]
        [InlineData("4111111111111111")]
        [InlineData("4012888888881881")]
        [InlineData("4222222222222")]
        [InlineData("6011000990139424")]
        [InlineData("6011111111111117")]
        public void WhenCreditCardNumberIsValidTestShouldPass(String value) {
            // arrange
            _sut.ChaseCreditCardNumber = value;
            const String TargetPropertyName = nameof(_sut.ChaseCreditCardNumber);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenPropertyIsNotAStringTestShouldFail() {
            // arrange
            _sut.JBCCreditCardNumber = 125555;
            const String TargetPropertyName = nameof(_sut.JBCCreditCardNumber);

            // act assert
            Assert.Throws<InvalidOperationException>(() => _modelRulesInvoker.CheckAllValidationRulesForProperty(_sut, TargetPropertyName));

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryNoTestShouldPass(String value) {
            // arrange
            _sut.USBankCreditCardNumber = value;
            const String TargetPropertyName = nameof(_sut.USBankCreditCardNumber);
            String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryYesInvalidTestShouldFail(String value) {
            // arrange
            _sut.NavyFederalCreditCardNumber = value;
            const String TargetPropertyName = nameof(_sut.NavyFederalCreditCardNumber);
            var targetPropertyNameFriendlyName = nameof(_sut.NavyFederalCreditCardNumber).GetWords();
            String ExpectedMessage = $"{targetPropertyNameFriendlyName} was null, DBNull, or empty string but was required.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingDefaultConstructorEntryIsRequiredInvalidTestShouldFail(String value) {
            // arrange
            _sut.ChaseCreditCardNumber = value;
            const String TargetPropertyName = nameof(_sut.ChaseCreditCardNumber);
            var targetPropertyNameFriendlyName = nameof(_sut.ChaseCreditCardNumber).GetWords();
            String ExpectedMessage = $"{targetPropertyNameFriendlyName} was null, DBNull, or empty string but was required.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [CreditCardNumberValidator()]
            public String ChaseCreditCardNumber { get; set; } = String.Empty;

            [CreditCardNumberValidator()]
            public Int32 JBCCreditCardNumber { get; set; }

            [CreditCardNumberValidator(RequiredEntry.Yes)]
            public String NavyFederalCreditCardNumber { get; set; } = String.Empty;

            [CreditCardNumberValidator(RequiredEntry.No)]
            public String USBankCreditCardNumber { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
