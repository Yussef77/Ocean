namespace Oceanware.Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class CompareValueValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public CompareValueValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("01/01/1960", "Hire Date must be greater than 1/1/1960 12:00:00 AM.", ExpectedValidationResult.Fail)]
        [InlineData("01/02/1960", "", ExpectedValidationResult.Pass)]
        public void ConvertToTypeDateEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.HireDate = Convert.ToDateTime(targetValue);
            const String TargetPropertyName = nameof(_sut.HireDate);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("1000.1", "Cost must be less than 1000.", ExpectedValidationResult.Fail)]
        [InlineData("-0.1", "Cost must be greater than 0.", ExpectedValidationResult.Fail)]
        [InlineData("900.12544", "", ExpectedValidationResult.Pass)]
        public void ConvertToTypeDecimalEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Cost = Convert.ToDecimal(targetValue);
            const String TargetPropertyName = nameof(_sut.Cost);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(10000d, "New Stock must be less than or equal to 5000.", ExpectedValidationResult.Fail)]
        [InlineData(5000d, "", ExpectedValidationResult.Pass)]
        [InlineData(10d, "", ExpectedValidationResult.Pass)]
        public void DoubleValueEnsureCorrectTestsResults(Double targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.NewStock = targetValue;
            const String TargetPropertyName = nameof(_sut.NewStock);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(-1, "Donation must be greater than 0.", ExpectedValidationResult.Fail)]
        [InlineData(1, "", ExpectedValidationResult.Pass)]
        public void IntegerValueEnsureCorrectTestsResults(Int32 targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Donation = targetValue;
            const String TargetPropertyName = nameof(_sut.Donation);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(-1f, "Donation Confirm must be greater than or equal to 0.", ExpectedValidationResult.Fail)]
        [InlineData(0f, "", ExpectedValidationResult.Pass)]
        [InlineData(0.1f, "", ExpectedValidationResult.Pass)]
        public void SingleValueEnsureCorrectTestsResults(Single targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.DonationConfirm = targetValue;
            const String TargetPropertyName = nameof(_sut.DonationConfirm);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("x", "Email Confirm must be equal to test@gmail.com.", ExpectedValidationResult.Fail)]
        [InlineData("test@gmail.com", "", ExpectedValidationResult.Pass)]
        [InlineData("", "", ExpectedValidationResult.Pass)]
        [InlineData("   ", "", ExpectedValidationResult.Pass)]
        [InlineData(null, "", ExpectedValidationResult.Pass)]
        public void StringValueMustEqualEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.EmailConfirm = targetValue;
            const String TargetPropertyName = nameof(_sut.EmailConfirm);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("test@gmail.com", "Email must not equal test@gmail.com.", ExpectedValidationResult.Fail)]
        [InlineData(null, "Value was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("", "Value was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("  ", "Value was null, DBNull, or empty string but was required.", ExpectedValidationResult.Fail)]
        [InlineData("x", "", ExpectedValidationResult.Pass)]
        public void StringValueMustNotEqualEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Email = targetValue;
            const String TargetPropertyName = nameof(_sut.Email);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [CompareValueValidator(ComparisonType.LessThan, "1000", ConvertToType.Decimal)]
            [CompareValueValidator(ComparisonType.GreaterThan, "0", ConvertToType.Decimal)]
            public Decimal Cost { get; set; }

            [CompareValueValidator(ComparisonType.GreaterThan, 0)]
            public Int32 Donation { get; set; }

            [CompareValueValidator(ComparisonType.GreaterThanEqual, 0.0f)]
            public Single DonationConfirm { get; set; }

            [CompareValueValidator(ComparisonType.NotEqual, "test@gmail.com", RequiredEntry.Yes)]
            public String Email { get; set; } = String.Empty;

            [CompareValueValidator(ComparisonType.Equal, "test@gmail.com", RequiredEntry.No)]
            public String EmailConfirm { get; set; } = String.Empty;

            [CompareValueValidator(ComparisonType.GreaterThan, "01/01/1960", ConvertToType.Date)]
            public DateTime HireDate { get; set; }

            [CompareValueValidator(ComparisonType.LessThanEqual, 5000.0d)]
            public Double NewStock { get; set; }

            public Customer() {
            }
        }
    }
}
