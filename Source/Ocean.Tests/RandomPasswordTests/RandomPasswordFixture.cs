namespace Ocean.Tests.RandomPasswordTests {

    using System;
    using Oceanware.Ocean.Infrastructure;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class RandomPasswordFixture {
        const Int32 _maximumLength = 16;
        const Int32 _minimumLength = 8;
        const String _passwordAllowedSpecialCharacters = "!@#$*&^%()-_+|";
        const String _passwordCharactersLowerCase = "abcdefgijkmnopqrstwxyz";
        const String _passwordCharactersNumeric = "23456789";
        const String _passwordCharactersUpperCase = "ABCDEFGHJKLMNPQRSTWXYZ";

        [Fact]
        public void GeneratesDefaultPasswordCorrectly() {
            // arrange

            // act
            var result = RandomPassword.Generate();

            // assert
            Assert.True(result.Length >= _minimumLength, "Result length too short");
            Assert.True(result.Length <= _maximumLength, "Result length too long.");
            Assert.True(ContainsCharacter(result, _passwordAllowedSpecialCharacters), "Result does not contain any special characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersLowerCase), "Result does not contain any lower case characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersNumeric), "Result does not contain any numeric characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersUpperCase), "Result does not contain any upper case characters");
        }

        [Fact]
        public void GeneratesPasswordWithCorrectLengthWithOnlyLowerCaseCharacters() {
            // arrange
            const Int32 Length = 15;

            // act
            var result = RandomPassword.Generate(Length, SpecialCharacter.No, DigitCharacter.No, UpperCaseCharacter.No, LowerCaseCharacter.Yes);

            // assert
            Assert.True(result.Length == Length, $"Result length {result.Length} is not {Length}.");
            Assert.False(ContainsCharacter(result, _passwordAllowedSpecialCharacters), "Result contains special characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersLowerCase), "Result does not contain any lower case characters");
            Assert.False(ContainsCharacter(result, _passwordCharactersNumeric), "Result contains numeric characters");
            Assert.False(ContainsCharacter(result, _passwordCharactersUpperCase), "Result contains upper case characters");
        }

        [Fact]
        public void GeneratesPasswordWithCorrectMinimumAndMaximumLengthWithOnlyLowerCaseCharacters() {
            // arrange
            const Int32 MinimumLength = 4;
            const Int32 MaximumLength = 20;
            const String SpecialCharacters = "$&";

            // act
            var result = RandomPassword.Generate(MinimumLength, MaximumLength, SpecialCharacter.Yes, DigitCharacter.No, UpperCaseCharacter.No, LowerCaseCharacter.No, SpecialCharacters);

            // assert
            Assert.True(result.Length >= MinimumLength, "Result length too short");
            Assert.True(result.Length <= MaximumLength, "Result length too long.");
            Assert.True(ContainsCharacter(result, SpecialCharacters), "Result does contain special characters");
            Assert.False(ContainsCharacter(result, _passwordCharactersLowerCase), "Result contains lower case characters");
            Assert.False(ContainsCharacter(result, _passwordCharactersNumeric), "Result contains numeric characters");
            Assert.False(ContainsCharacter(result, _passwordCharactersUpperCase), "Result contains upper case characters");
        }

        [Fact]
        public void GeneratesPasswordWithSpecifiedLengthCorrectly() {
            // arrange
            const Int32 Length = 15;
            // act
            var result = RandomPassword.Generate(Length);

            // assert
            Assert.True(result.Length == Length, $"Result length {result.Length} is not {Length}.");
            Assert.True(ContainsCharacter(result, _passwordAllowedSpecialCharacters), "Result does not contain any special characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersLowerCase), "Result does not contain any lower case characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersNumeric), "Result does not contain any numeric characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersUpperCase), "Result does not contain any upper case characters");
        }

        [Fact]
        public void GeneratesPasswordWithSpecifiedMinAndMaxLengthCorrectly() {
            // arrange
            const Int32 MinimumLength = 4;
            const Int32 MaximumLength = 20;
            // act
            var result = RandomPassword.Generate(MinimumLength, MaximumLength);

            // assert
            Assert.True(result.Length >= MinimumLength, "Result length too short");
            Assert.True(result.Length <= MaximumLength, "Result length too long.");
            Assert.True(ContainsCharacter(result, _passwordAllowedSpecialCharacters), "Result does not contain any special characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersLowerCase), "Result does not contain any lower case characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersNumeric), "Result does not contain any numeric characters");
            Assert.True(ContainsCharacter(result, _passwordCharactersUpperCase), "Result does not contain any upper case characters");
        }

        Boolean ContainsCharacter(String target, String requiredCharactersSet) {
            foreach (var item in requiredCharactersSet.ToCharArray()) {
                if (target.Contains(item)) {
                    return true;
                }
            }
            return false;
        }
    }
}
