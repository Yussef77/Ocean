namespace Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class RangeValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public RangeValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("01/01/2021", "Hire Date must be less than or equal to 12/31/2020 12:00:00 AM.", ExpectedValidationResult.Fail)]
        [InlineData("01/01/1959", "Hire Date must be greater than or equal to 1/1/1960 12:00:00 AM.", ExpectedValidationResult.Fail)]
        [InlineData("01/01/1960", "", ExpectedValidationResult.Pass)]
        [InlineData("12/31/2020", "", ExpectedValidationResult.Pass)]
        [InlineData(null, "", ExpectedValidationResult.Pass)]
        public void ConvertToTypeDateInclusiveEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            if (targetValue == null) {
                _sut.HireDate = null;
            } else {
                _sut.HireDate = Convert.ToDateTime(targetValue);
            }
            const String TargetPropertyName = nameof(_sut.HireDate);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("2000.1", "Donation must be less than 1000.", ExpectedValidationResult.Fail)]
        [InlineData("1000", "Donation must be less than 1000.", ExpectedValidationResult.Fail)]
        [InlineData("0", "Donation must be greater than 0.", ExpectedValidationResult.Fail)]
        [InlineData("999", "", ExpectedValidationResult.Pass)]
        [InlineData("1", "", ExpectedValidationResult.Pass)]
        public void ConvertToTypeDecimalExclusiveEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Donation = Convert.ToDecimal(targetValue);
            const String TargetPropertyName = nameof(_sut.Donation);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("2000.1", "Cost must be less than or equal to 1000.", ExpectedValidationResult.Fail)]
        [InlineData("1000.1", "Cost must be less than or equal to 1000.", ExpectedValidationResult.Fail)]
        [InlineData("-0.1", "Cost must be greater than or equal to 0.", ExpectedValidationResult.Fail)]
        [InlineData("1000", "", ExpectedValidationResult.Pass)]
        [InlineData("0", "", ExpectedValidationResult.Pass)]
        public void ConvertToTypeDecimalInclusiveEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Cost = Convert.ToDecimal(targetValue);
            const String TargetPropertyName = nameof(_sut.Cost);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(4.99d, "New Stock must be greater than or equal to 5.", ExpectedValidationResult.Fail)]
        [InlineData(10000.1d, "New Stock must be less than or equal to 10000.", ExpectedValidationResult.Fail)]
        [InlineData(5d, "", ExpectedValidationResult.Pass)]
        [InlineData(10000d, "", ExpectedValidationResult.Pass)]
        public void DoubleValueEnsureCorrectTestsResults(Double targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.NewStock = targetValue;
            const String TargetPropertyName = nameof(_sut.NewStock);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(4.99f, "Donation Confirm must be greater than or equal to 5.", ExpectedValidationResult.Fail)]
        [InlineData(10000.1f, "Donation Confirm must be less than or equal to 10000.", ExpectedValidationResult.Fail)]
        [InlineData(5f, "", ExpectedValidationResult.Pass)]
        [InlineData(10000f, "", ExpectedValidationResult.Pass)]
        public void SingleValueEnsureCorrectTestsResults(Single targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.DonationConfirm = targetValue;
            const String TargetPropertyName = nameof(_sut.DonationConfirm);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [RangeValidator(RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Inclusive, "1000", ConvertToType.Decimal)]
            public Decimal Cost { get; set; }

            [RangeValidator(RangeBoundaryType.Exclusive, "0", RangeBoundaryType.Exclusive, "1000", ConvertToType.Decimal)]
            public Decimal Donation { get; set; }

            [RangeValidator(RangeBoundaryType.Inclusive, 5f, RangeBoundaryType.Inclusive, 10000f)]
            public Single DonationConfirm { get; set; }

            [RangeValidator(RangeBoundaryType.Inclusive, "01/01/1960", RangeBoundaryType.Inclusive, "12/31/2020", ConvertToType.Date, RequiredEntry.No)]
            public Nullable<DateTime> HireDate { get; set; }

            [RangeValidator(RangeBoundaryType.Inclusive, 5d, RangeBoundaryType.Inclusive, 10000d)]
            public Double NewStock { get; set; }

            public Customer() {
            }
        }
    }
}
