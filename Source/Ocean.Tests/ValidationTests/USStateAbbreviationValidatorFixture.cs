namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class USStateAbbreviationValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public USStateAbbreviationValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("on", "State 'on' state abbreviation is not valid.", ExpectedValidationResult.Fail)]
        [InlineData(null, "State was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("AE", "", ExpectedValidationResult.Pass)]
        [InlineData("DC", "", ExpectedValidationResult.Pass)]
        [InlineData("in", "", ExpectedValidationResult.Pass)]
        [InlineData("IN", "", ExpectedValidationResult.Pass)]
        public void StateAbbreviationEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.State = targetValue;
            const String TargetPropertyName = nameof(_sut.State);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("on", "State Not Required 'on' state abbreviation is not valid.", ExpectedValidationResult.Fail)]
        [InlineData(null, "", ExpectedValidationResult.Pass)]
        [InlineData("", "", ExpectedValidationResult.Pass)]
        [InlineData("AE", "", ExpectedValidationResult.Pass)]
        [InlineData("DC", "", ExpectedValidationResult.Pass)]
        [InlineData("in", "", ExpectedValidationResult.Pass)]
        [InlineData("IN", "", ExpectedValidationResult.Pass)]
        public void WhenStateAbbreviationRequiredNoEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.StateNotRequired = targetValue;
            const String TargetPropertyName = nameof(_sut.StateNotRequired);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [USStateAbbreviationValidator()]
            public String State { get; set; } = String.Empty;

            [USStateAbbreviationValidator(RequiredEntry.No)]
            public String StateNotRequired { get; set; } = String.Empty;

            public Customer() {
            }
        }
    }
}
