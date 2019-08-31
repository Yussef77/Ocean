namespace Oceanware.Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Oceanware.Ocean.ValidationRules.InstanceRules;
    using Xunit;

    public class ComparePropertyValidatorFixture : FixtureBase {
        readonly Customer _sut;

        public ComparePropertyValidatorFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Fact]
        public void InstanceFirstReviewLessThanEqualToHireDateAddDays30() {
            // arrange
            _sut.FirstReview = DateTime.Now.AddDays(28);
            _sut.HireDate = DateTime.Now;
            const String TargetPropertyName = nameof(_sut.FirstReview);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void InstanceFirstReviewLessThanEqualToHireDateAddDays30Fails() {
            // arrange
            _sut.FirstReview = DateTime.Now.AddDays(35);
            _sut.HireDate = DateTime.Now;
            const String TargetPropertyName = nameof(_sut.FirstReview);
            String ExpectedMessage = $"First Review must be less than or equal to {DateTime.Now.AddDays(30).ToShortDateString()}.";

            // act assert
            base.RunValidation(ExpectedValidationResult.Fail, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void InstanceRuleHireDateLessThanEqualToToday() {
            // arrange
            _sut.HireDate = DateTime.Now;
            const String TargetPropertyName = nameof(_sut.HireDate);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void InstanceRuleHireDateLessThanEqualToTodayFailsWithFutureDate() {
            // arrange
            _sut.HireDate = DateTime.Now.AddDays(1);
            const String TargetPropertyName = nameof(_sut.HireDate);
            String ExpectedMessage = $"Hire Date must be less than or equal to {DateTime.Today.Date.ToShortDateString()}.";

            // act assert
            base.RunValidation(ExpectedValidationResult.Fail, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenPropertiesAreEqualTestPasses() {
            // arrange
            _sut.Email = "012345678901234567890123456789";
            _sut.EmailConfirm = _sut.Email;
            const String TargetPropertyName = nameof(_sut.Email);
            const String ExpectedMessage = "";

            // act assert
            base.RunValidation(ExpectedValidationResult.Pass, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Fact]
        public void WhenPropertiesAreSupposedToBeEqualButAreNotTestFails() {
            // arrange
            _sut.Email = "012345678901234567890123456789";
            _sut.EmailConfirm = "0123";
            const String TargetPropertyName = nameof(_sut.Email);
            const String ExpectedMessage = "Email must be equal to Email Confirm.";

            // act assert
            base.RunValidation(TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(99, 100, "Donation must be greater than Donation Confirm.", ExpectedValidationResult.Fail)]
        [InlineData(101, 100, "", ExpectedValidationResult.Pass)]
        public void WhenTargetIsSupposedToBeGreatThanEnsureCorrectTestsResults(Int32 targetValue, Int32 otherPropertyValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Donation = targetValue;
            _sut.DonationConfirm = otherPropertyValue;
            const String TargetPropertyName = nameof(_sut.Donation);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(99, 100, "Required Stock must be greater than or equal to Required Stock Confirm.", ExpectedValidationResult.Fail)]
        [InlineData(101, 100, "", ExpectedValidationResult.Pass)]
        [InlineData(101, 101, "", ExpectedValidationResult.Pass)]
        [InlineData(null, 100, "Required Stock must be greater than or equal to Required Stock Confirm.", ExpectedValidationResult.Fail)]
        public void WhenTargetIsSupposedToBeGreatThanEqualEnsureCorrectTestsResults(Int32 targetValue, Int32 otherPropertyValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.RequiredStock = targetValue;
            _sut.RequiredStockConfirm = otherPropertyValue;
            const String TargetPropertyName = nameof(_sut.RequiredStock);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData(100, 99, "New Stock must be less than New Stock Confirm.", ExpectedValidationResult.Fail)]
        [InlineData(100, 101, "", ExpectedValidationResult.Pass)]
        public void WhenTargetIsSupposedToBeLessThanEnsureCorrectTestsResults(Int32 targetValue, Int32 otherPropertyValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.NewStock = targetValue;
            _sut.NewStockConfirm = otherPropertyValue;
            const String TargetPropertyName = nameof(_sut.NewStock);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("abc", "abc", "Password must not equal Password Confirm.", ExpectedValidationResult.Fail)]
        [InlineData("abc", "ABC", "", ExpectedValidationResult.Pass)]
        [InlineData(null, "ABC", "", ExpectedValidationResult.Pass)]
        public void WhenTargetIsSupposedToBeNotEqualEnsureCorrectTestsResults(String targetValue, String otherPropertyValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Password = targetValue;
            _sut.PasswordConfirm = otherPropertyValue;
            const String TargetPropertyName = nameof(_sut.Password);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet, ISupportInstanceValidationRules {

            [field: NonSerialized]
            ValidationRulesManager _instanceValidationRulesManager;

            public String ActiveRuleSet { get; set; }

            [ComparePropertyValidator(ComparisonType.GreaterThan, nameof(Customer.DonationConfirm))]
            public Int32 Donation { get; set; }

            public Int32 DonationConfirm { get; set; }

            [ComparePropertyValidator(ComparisonType.Equal, nameof(Customer.EmailConfirm))]
            public String Email { get; set; } = String.Empty;

            public String EmailConfirm { get; set; } = String.Empty;

            public DateTime FirstReview { get; set; }

            public DateTime HireDate { get; set; }

            public ValidationRulesManager InstanceValidationRulesManager {
                get {
                    return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
                }
            }

            [ComparePropertyValidator(ComparisonType.LessThan, nameof(Customer.NewStockConfirm))]
            public Int32 NewStock { get; set; }

            public Int32 NewStockConfirm { get; set; }
            public String ObjectRuleSet { get; set; }

            [ComparePropertyValidator(ComparisonType.LessThanEqual, nameof(Customer.OldStockConfirm))]
            public Int32 OldStock { get; set; }

            public Int32 OldStockConfirm { get; set; }

            [ComparePropertyValidator(ComparisonType.NotEqual, nameof(Customer.PasswordConfirm), RequiredEntry.No)]
            public String Password { get; set; }

            public String PasswordConfirm { get; set; }

            [ComparePropertyValidator(ComparisonType.GreaterThanEqual, nameof(Customer.RequiredStockConfirm), RequiredEntry.No)]
            public Int32 RequiredStock { get; set; }

            public Int32 RequiredStockConfirm { get; set; }

            public Customer() {
                this.AddInstanceBusinessValidationRules();
            }

            protected virtual void AddInstanceBusinessValidationRules() {
                this.InstanceValidationRulesManager.AddRule(new CompareDateInstanceValidationRule(() => DateTime.Today, ComparisonType.LessThanEqual), nameof(HireDate));

                this.InstanceValidationRulesManager.AddRule(new CompareDateInstanceValidationRule(() => this.HireDate.AddDays(30), ComparisonType.LessThanEqual), nameof(FirstReview));
            }
        }
    }
}
