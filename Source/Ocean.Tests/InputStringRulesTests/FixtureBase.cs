namespace Ocean.Tests.InputStringRulesTests {

    using System;
    using Oceanware.Ocean.Rules;
    using Xunit;

    public abstract class FixtureBase {
        readonly protected IModelRulesInvoker _modelRulesInvoker;

        protected FixtureBase() {
            _modelRulesInvoker = new ModelRulesInvoker();
        }

        public void RunValidation(String targetPropertyValue, String expectedValue, String targetPropertyName, Object sut) {
            // act
            var resultValue = _modelRulesInvoker.FormatPropertyValueUsingCharacterCaseRule(sut, targetPropertyName, targetPropertyValue);

            // assert
            Assert.True(resultValue == expectedValue, $"Expected that {resultValue} would equal {expectedValue}.");
        }
    }
}
