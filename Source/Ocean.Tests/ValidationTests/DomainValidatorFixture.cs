namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.Extensions;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class DomainValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public DomainValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryNoTestShouldPass(String value) {
            // arrange
            _sut.RuleType = value;
            const String TargetPropertyName = nameof(_sut.RuleType);
            String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsingConstructorWithRequiredEntryYesInvalidTestShouldFail(String value) {
            // arrange
            _sut.Role = value;
            const String TargetPropertyName = nameof(_sut.Role);
            var targetPropertyNameFriendlyName = nameof(_sut.Role).GetWords();
            String ExpectedMessage = $"{targetPropertyNameFriendlyName} was null, DBNull, or empty string but was required.";
            
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("Top")]
        [InlineData("Left")]
        [InlineData("Bottom")]
        [InlineData("Right")]
        public void WhenValueIsInDomainTestShouldPass(String value) {
            // arrange
            _sut.Alignment = value;
            const String TargetPropertyName = nameof(_sut.Alignment);
            const String ExpectedMessage = "";
            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("Back", "Alignment Back did not match any of the acceptable values Top, Left, Right, Bottom.")]
        [InlineData("Below", "Alignment Below did not match any of the acceptable values Top, Left, Right, Bottom.")]
        public void WhenValueIsNotInDomainTestShouldFail(String value, String expectedMessage) {
            // arrange
            _sut.Alignment = value;
            const String TargetPropertyName = nameof(_sut.Alignment);
            String ExpectedMessage = expectedMessage;
            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [DomainValidator("Top", "Left", "Right", "Bottom")]
            public String Alignment { get; set; } = String.Empty;

            [DomainValidator(RequiredEntry.Yes, "Admin", "User", "Guest")]
            public String Role { get; set; } = String.Empty;

            [DomainValidator(RequiredEntry.No, "Attribute", "Shared", "Instance")]
            public String RuleType { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
