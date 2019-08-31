namespace Oceanware.Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.Rules;
    using Xunit;

    public abstract class FixtureBase {
        readonly protected IModelRulesInvoker _modelRulesInvoker;

        protected FixtureBase() {
            _modelRulesInvoker = new ModelRulesInvoker();
        }

        public void RunValidation(String targetPropertyName, Object sut, String expectedMessage, Int32 expectedErrorsCount = 1) {
            // act
            var validationResult = _modelRulesInvoker.CheckAllValidationRulesForProperty(sut, targetPropertyName);

            // Assert
            Assert.False(validationResult.IsValid, "Expect validation to fail.");
            Assert.True(expectedErrorsCount == validationResult.ValidationErrors.Count, "Unexpected number of validation errors.");
            Assert.True(expectedMessage == validationResult.ValidationErrors[0].Value.ErrorMessage, "Incorrect error message");
            Assert.True(targetPropertyName == validationResult.ValidationErrors[0].Key, "Incorrect member name");
        }

        public void RunValidation(ExpectedValidationResult expectedValidationResult, String targetPropertyName, Object sut, String expectedMessage, Int32 expectedErrorsCount = 1) {
            // act
            var validationResult = _modelRulesInvoker.CheckAllValidationRulesForProperty(sut, targetPropertyName);

            // Assert
            if (expectedValidationResult == ExpectedValidationResult.Pass) {
                const Int32 ExpectedErrorsCount = 0;
                Assert.True(validationResult.IsValid, "Expect validation to pass.");
                Assert.True(ExpectedErrorsCount == validationResult.ValidationErrors.Count, "Unexpected number of validation errors.");
            } else {
                Assert.False(validationResult.IsValid, "Expect validation to fail.");
                Assert.True(expectedErrorsCount == validationResult.ValidationErrors.Count, "Unexpected number of validation errors.");
                Assert.True(expectedMessage == validationResult.ValidationErrors[0].Value.ErrorMessage, "Incorrect error message");
                Assert.True(targetPropertyName == validationResult.ValidationErrors[0].Key, "Incorrect member name");
            }
        }
    }
}
