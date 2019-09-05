namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.Tests.ValidationTests;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class MultipleOfValidatorFixture : Oceanware.Ocean.Tests.ValidationTests.FixtureBase {
        readonly Customer _sut;

        public MultipleOfValidatorFixture() {
            _sut = new Customer();
        }

        [Theory]
        [InlineData(20)]
        [InlineData(10)]
        [InlineData(1000)]
        public void WhenDivisibleByTenTestPass(Int32 value) {
            // arrange
            _sut.Count = value;
            const String TargetPropertyName = nameof(_sut.Count);
            String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        public void WhenDivisibleByTwoTestPass(Int32 value) {
            // arrange
            _sut.Age = value;
            const String TargetPropertyName = nameof(_sut.Age);
            String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(201)]
        [InlineData(101)]
        [InlineData(1001)]
        public void WhenNotDivisibleByTenTestFail(Int32 value) {
            // arrange
            _sut.Count = value;
            const String TargetPropertyName = nameof(_sut.Count);
            String ExpectedMessage = $"Count value {value} is not divisible by 10.";

            // act assert
            base.RunValidation(ExpectedValidationResult.Fail, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenValidatorAppliedToNonIntegerPropertyExceptionIsThrown() {
            // arrange
            _sut.Name = String.Empty;

            // act assert
            Assert.Throws<InvalidOperationException>(() => _modelRulesInvoker.CheckAllValidationRulesForProperty(_sut, nameof(Customer.Name)));
        }

        class Customer {

            [MultipleOfValidator(2)]
            public Int32 Age { get; set; }

            [MultipleOfValidator(10)]
            public Int32 Count { get; set; }

            [MultipleOfValidator(2)]
            public String Name { get; set; }

            public Customer() {
            }
        }
    }
}
