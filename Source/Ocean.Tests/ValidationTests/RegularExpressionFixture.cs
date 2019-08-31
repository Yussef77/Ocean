namespace Oceanware.Ocean.Tests.ValidationTests {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class RegularExpressionFixture : FixtureBase {
        readonly Customer _sut;

        public RegularExpressionFixture() {
            _sut = new Customer();
            _sut.ActiveRuleSet = ValidationConstants.Update;
        }

        [Theory]
        [InlineData("204036", "UK Bank Sort Code did not match the required ^(\\d){2}-(\\d){2}-(\\d){2}$ pattern.", ExpectedValidationResult.Fail)]
        [InlineData("444-58-54", "UK Bank Sort Code did not match the required ^(\\d){2}-(\\d){2}-(\\d){2}$ pattern.", ExpectedValidationResult.Fail)]
        [InlineData("45/45/85", "UK Bank Sort Code did not match the required ^(\\d){2}-(\\d){2}-(\\d){2}$ pattern.", ExpectedValidationResult.Fail)]
        [InlineData("20-40-36", "", ExpectedValidationResult.Pass)]
        public void WhenCustomPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.UKBankSortCode = targetValue;
            const String TargetPropertyName = nameof(_sut.UKBankSortCode);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("hello@gmail", "Email did not match the required email pattern.", ExpectedValidationResult.Fail)]
        [InlineData("hello@gmail.com", "", ExpectedValidationResult.Pass)]
        public void WhenEmailPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Email = targetValue;
            const String TargetPropertyName = nameof(_sut.Email);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("10.000", "Ip Address did not match the required IP address pattern.", ExpectedValidationResult.Fail)]
        [InlineData("10.0.0.1", "", ExpectedValidationResult.Pass)]
        public void WhenIPAddressPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.IpAddress = targetValue;
            const String TargetPropertyName = nameof(_sut.IpAddress);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("111-12-123455", "Ssn did not match the required SSN pattern.", ExpectedValidationResult.Fail)]
        [InlineData("123121234", "Ssn did not match the required SSN pattern.", ExpectedValidationResult.Fail)]
        [InlineData("123-12-1234", "", ExpectedValidationResult.Pass)]
        public void WhenSSNPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Ssn = targetValue;
            const String TargetPropertyName = nameof(_sut.Ssn);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("google", "Web Site did not match the required URL pattern.", ExpectedValidationResult.Fail)]
        [InlineData("https://google.com", "", ExpectedValidationResult.Pass)]
        [InlineData("https://google.com:8080", "", ExpectedValidationResult.Pass)]
        [InlineData("https://localhost:5001/", "Web Site did not match the required URL pattern.", ExpectedValidationResult.Fail)]
        public void WhenURLPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.WebSite = targetValue;
            const String TargetPropertyName = nameof(_sut.WebSite);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("google", "Local Home Page did not match the required URL pattern.", ExpectedValidationResult.Fail)]
        [InlineData("https://google.com", "", ExpectedValidationResult.Pass)]
        [InlineData("https://google.com:8080", "", ExpectedValidationResult.Pass)]
        [InlineData("https://localhost:5001/", "", ExpectedValidationResult.Pass)]
        public void WhenURLAndLocalHostPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.LocalHomePage = targetValue;
            const String TargetPropertyName = nameof(_sut.LocalHomePage);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("111/111.1111", "Phone did not match the required US phone number pattern.", ExpectedValidationResult.Fail)]
        [InlineData("444-58-54", "Phone did not match the required US phone number pattern.", ExpectedValidationResult.Fail)]
        [InlineData("45/45/85", "Phone did not match the required US phone number pattern.", ExpectedValidationResult.Fail)]
        [InlineData("123-123-1234", "", ExpectedValidationResult.Pass)]
        [InlineData("(123)-123-1234", "", ExpectedValidationResult.Pass)]
        [InlineData("(123) 123-1234", "", ExpectedValidationResult.Pass)]
        [InlineData("(123).123-1234", "", ExpectedValidationResult.Pass)]
        [InlineData("123.123.1234", "", ExpectedValidationResult.Pass)]
        public void WhenUSPhoneNumberPatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.Phone = targetValue;
            const String TargetPropertyName = nameof(_sut.Phone);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        [Theory]
        [InlineData("1234-1234", "Zip Code did not match the required zip code pattern.", ExpectedValidationResult.Fail)]
        [InlineData("1234", "Zip Code did not match the required zip code pattern.", ExpectedValidationResult.Fail)]
        [InlineData("12345", "", ExpectedValidationResult.Pass)]
        [InlineData("12345-1234", "", ExpectedValidationResult.Pass)]
        [InlineData("", "", ExpectedValidationResult.Pass)]
        [InlineData(null, "", ExpectedValidationResult.Pass)]
        public void WhenZipCodePatternEnsureCorrectTestsResults(String targetValue, String expectedMessage, ExpectedValidationResult expectedValidationResult) {
            // arrange
            _sut.ZipCode = targetValue;
            const String TargetPropertyName = nameof(_sut.ZipCode);
            String ExpectedMessage = expectedMessage;

            // act assert
            base.RunValidation(expectedValidationResult, TargetPropertyName, _sut, ExpectedMessage);
        }

        class Customer : IRuleSet {
            public String ActiveRuleSet { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.Email)]
            public String Email { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.IPAddress)]
            public String IpAddress { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.USPhoneNumber)]
            public String Phone { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.SSN)]
            public String Ssn { get; set; }

            [RegularExpressionValidator("^(\\d){2}-(\\d){2}-(\\d){2}$", FriendlyName = "UK Bank Sort Code")]
            public String UKBankSortCode { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.URL)]
            public String WebSite { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.URLIsWellFormed)]
            public String LocalHomePage { get; set; }

            [RegularExpressionValidator(RegularExpressionPatternType.USZipCode, RequiredEntry.No)]
            public String ZipCode { get; set; }

            public Customer() {
            }
        }
    }
}
