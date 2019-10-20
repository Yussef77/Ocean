namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class BooleanRequiredValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public BooleanRequiredValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Fact]
        public void WhenValidatorDecoratesNonBooleanPropertyTestShouldFail() {
            // arrange
            _sut.Count = 125555;
            const String TargetPropertyName = nameof(_sut.Count);

            // act assert
            Assert.Throws<InvalidOperationException>(() => _modelRulesInvoker.CheckAllValidationRulesForProperty(_sut, TargetPropertyName));
        }

        [Fact]
        public void WhenValueIsFalseTestShouldFail() {
            // arrange
            _sut.AcceptTerms = false;
            const String TargetPropertyName = nameof(_sut.AcceptTerms);
            const String ExpectedMessage = "Accept Terms is required to be checked.";

            // act assert
            base.RunValidation(ExpectedValidationResult.Fail, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValueIsTrueTestShouldPass() {
            // arrange
            _sut.AcceptTerms = true;
            const String TargetPropertyName = nameof(_sut.AcceptTerms);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {

            [BooleanRequiredValidator]
            public Boolean AcceptTerms { get; set; }

            public String ActiveRuleSet { get; set; }

            [BooleanRequiredValidator]
            public Int32 Count { get; set; }

            public Customer() {
            }
        }
    }
}
