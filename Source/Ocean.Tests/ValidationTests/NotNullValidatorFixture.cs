namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class NotNullValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public NotNullValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Fact]
        public void WhenChaseBankRoutingNumberIsNullTestShouldFail() {
            // arrange
            _sut.ChaseBankRoutingNumber = null;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            const String ExpectedMessage = "Chase Bank Routing Number is null but is required to be non-null.";
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("071000013")]
        [InlineData("065400137")]
        public void WhenChaseBankRoutingNumberIsValidTestShouldPass(String value) {
            // arrange
            _sut.ChaseBankRoutingNumber = value;
            const String TargetPropertyName = nameof(_sut.ChaseBankRoutingNumber);
            const String ExpectedMessage = "";
            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenJBCBankRoutingNumberIsNullTestShouldFail() {
            // arrange
            _sut.JBCBankRoutingNumber = null;
            const String TargetPropertyName = nameof(_sut.JBCBankRoutingNumber);
            const String ExpectedMessage = "JBC Bank Routing Number is null but is required to be non-null.";
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenNavyFederalRoutingNumberIsNullTestShouldFail() {
            // arrange
            _sut.NavyFederalRoutingNumber = null;
            const String TargetPropertyName = nameof(_sut.NavyFederalRoutingNumber);
            const String ExpectedMessage = "Navy Federal Routing Number is null but is required to be non-null.";
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenRoutingNumberIsDBNullTestShouldFail() {
            // arrange
            _sut.ChaseCreditCardNumber = DBNull.Value;
            const String TargetPropertyName = nameof(_sut.ChaseCreditCardNumber);
            const String ExpectedMessage = "Chase Credit Card Number is DBNull but is required to be non-null.";
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [NotNullValidator()]
            public String ChaseBankRoutingNumber { get; set; }

            [NotNullValidator()]
            public Object ChaseCreditCardNumber { get; set; }

            [NotNullValidator(FriendlyName = "JBC Bank Routing Number")]
            public Int32? JBCBankRoutingNumber { get; set; }

            [NotNullValidator()]
            public Nullable<Int32> NavyFederalRoutingNumber { get; set; }

            public Customer() {
            }
        }
    }
}
