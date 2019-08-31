namespace Oceanware.Ocean.Infrastructure {

    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Oceanware.Ocean.ValidationRules;

    ///<summary>
    ///<para>This class can generates random passwords, which do not include ambiguous characters, such as I, l, and 1.</para>
    ///<para>The generated password will be made of 7-bit ASCII symbols.</para>
    ///<para>Every four characters will include one lower case character, one upper case character, one number, and one special symbol (such as '%') in a random order.</para>
    ///<para>The password will always start with an alpha-numeric character.</para>
    ///</summary>
    public class RandomPassword {
        const Int32 Zero = 0;
        const Int32 DefaultMaximumPasswordLength = 16;
        const Int32 DefaultMinimumPasswordLength = 8;

        ///Define supported password characters divided into groups.
        ///You can add (or remove) characters to (from) these groups.
        const String PasswordCharactersLowerCase = "abcdefgijkmnopqrstwxyz";
        const String PasswordCharactersNumeric = "23456789";
        const String PasswordCharactersUpperCase = "ABCDEFGHJKLMNPQRSTWXYZ";
        public const String PasswordAllowedSpecialCharacters = "!@#$*&^%()-_+|";

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPassword"/> class.
        /// </summary>
        RandomPassword() {
        }

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>String containing a randomly generated password.</returns>
        /// <remarks>The length of the generated password will be random. It will be no shorter than the minimum default and no longer than maximum default.</remarks>
        public static String Generate() {
            return Generate(DefaultMinimumPasswordLength, DefaultMaximumPasswordLength, SpecialCharacter.Yes, DigitCharacter.Yes, UpperCaseCharacter.Yes, LowerCaseCharacter.Yes);
        }
        
        /// <summary>Generates a random password.</summary>
        /// <param name="length">The length.</param>
        /// <param name="specialCharacter">The special character.</param>
        /// <param name="digitCharacter">The digit character.</param>
        /// <param name="upperCaseCharacter">The upper case character.</param>
        /// <param name="lowerCaseCharacter">The lower case character.</param>
        /// <param name="passwordAllowedSpecialCharacters">The password allowed special characters.</param>
        /// <returns>String containing a randomly generated password.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length is not greater than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when all password characteristics are <c>No</c>.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when passwordAllowedSpecialCharacters is null, empty, or white space.</exception>
        public static String Generate(Int32 length, SpecialCharacter specialCharacter, DigitCharacter digitCharacter, UpperCaseCharacter upperCaseCharacter, LowerCaseCharacter lowerCaseCharacter, String passwordAllowedSpecialCharacters = PasswordAllowedSpecialCharacters) {
            if (length <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(length), Strings.MustBeGreaterThanZero);
            }
            if (specialCharacter == SpecialCharacter.No && digitCharacter == DigitCharacter.No && specialCharacter == SpecialCharacter.No && upperCaseCharacter == UpperCaseCharacter.No && lowerCaseCharacter == LowerCaseCharacter.No) {
                throw new InvalidOperationException(Strings.AtLeastOnePasswordCharacteristicMustBeYes);
            }
            if (String.IsNullOrWhiteSpace(passwordAllowedSpecialCharacters)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(passwordAllowedSpecialCharacters));
            }

            return Generate(length, length, specialCharacter, digitCharacter, upperCaseCharacter, lowerCaseCharacter, passwordAllowedSpecialCharacters);
        }

        /// <summary>Generates a random password.</summary>
        /// <param name="length">The length.</param>
        /// <param name="specialCharacter">The special character.</param>
        /// <param name="digitCharacter">The digit character.</param>
        /// <param name="upperCaseCharacter">The upper case character.</param>
        /// <param name="lowerCaseCharacter">The lower case character.</param>
        /// <param name="passwordAllowedSpecialCharacters">The password allowed special characters.</param>
        /// <returns>String containing a randomly generated password.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when minimumLength is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than minimum length.</exception>
        /// <exception cref="InvalidOperationException">Thrown when all password characteristics are <c>No</c>.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when passwordAllowedSpecialCharacters is null, empty, or white space.</exception>
        public static String Generate(Int32 minimumLength, Int32 maximumLength, SpecialCharacter specialCharacter, DigitCharacter digitCharacter, UpperCaseCharacter upperCaseCharacter, LowerCaseCharacter lowerCaseCharacter, String passwordAllowedSpecialCharacters = PasswordAllowedSpecialCharacters) {
            if (minimumLength <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(minimumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanOrEqualToMinimumLength);
            }
            if (specialCharacter == SpecialCharacter.No && digitCharacter == DigitCharacter.No && specialCharacter == SpecialCharacter.No && upperCaseCharacter == UpperCaseCharacter.No && lowerCaseCharacter == LowerCaseCharacter.No) {
                throw new InvalidOperationException(Strings.AtLeastOnePasswordCharacteristicMustBeYes);
            }

            var characterGroupsList = new List<Char[]>();
            if (specialCharacter == SpecialCharacter.Yes) {
                characterGroupsList.Add(passwordAllowedSpecialCharacters.ToCharArray());
            }
            if (digitCharacter == DigitCharacter.Yes) {
                characterGroupsList.Add(PasswordCharactersNumeric.ToCharArray());
            }
            if (upperCaseCharacter == UpperCaseCharacter.Yes) {
                characterGroupsList.Add(PasswordCharactersUpperCase.ToCharArray());
            }
            if (lowerCaseCharacter == LowerCaseCharacter.Yes) {
                characterGroupsList.Add(PasswordCharactersLowerCase.ToCharArray());
            }

            var characterGroups = characterGroupsList.ToArray();

            //Use this array to track the number of unused characters in each character group.
            var charsRemainingInGroup = new Int32[characterGroups.Length];

            //Initially, all characters in each group are not used.
            Int32 i;

            for (i = 0; i < charsRemainingInGroup.Length; i++) {
                charsRemainingInGroup[i] = characterGroups[i].Length;
            }

            //Use this array to track (iterate through) unused character groups.
            var remainingGroupsOrder = new Int32[characterGroups.Length];

            //Initially, all character groups are not used.
            for (i = 0; i < remainingGroupsOrder.Length; i++) {
                remainingGroupsOrder[i] = i;
            }

            var randomBytes = new Byte[4];
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(randomBytes);
            }

            //Convert 4 bytes into a 32-bit integer value.
            Int32 seed = (randomBytes[0] & 0X7F) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3];

            var random = new Random(seed);

            Char[] password = minimumLength < maximumLength ? new Char[random.Next(minimumLength - 1, maximumLength) + 1] : new Char[minimumLength];
            Int32 nextCharacterIndex;
            Int32 nextGroupIndex;
            Int32 nextRemainingGroupsOrderIndex;
            Int32 lastCharacterIndex;
            Int32 lastRemainingGroupsOrderIndex = remainingGroupsOrder.Length - 1;

            //Generate password characters one at a time.
            for (i = 0; i < password.Length; i++) {
                //If only one character group remained unprocessed, process it;
                //otherwise, pick a random character group from the unprocessed
                //group list. To allow a special character to appear in the
                //first position, increment the second parameter of the Next
                //function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                nextRemainingGroupsOrderIndex = lastRemainingGroupsOrderIndex == 0 ? 0 : random.Next(0, lastRemainingGroupsOrderIndex);

                //Get the actual index of the character group, from which we will
                //pick the next character.
                nextGroupIndex = remainingGroupsOrder[nextRemainingGroupsOrderIndex];

                //Get the index of the last unprocessed characters in this group.
                lastCharacterIndex = charsRemainingInGroup[nextGroupIndex] - 1;

                //If only one unprocessed character is left, pick it; otherwise,
                //get a random character from the unused character list.
                nextCharacterIndex = lastCharacterIndex == 0 ? 0 : random.Next(0, lastCharacterIndex + 1);

                //Add this character to the password.
                password[i] = characterGroups[nextGroupIndex][nextCharacterIndex];

                //If we processed the last character in this group, start over.
                if (lastCharacterIndex == 0) {
                    charsRemainingInGroup[nextGroupIndex] = characterGroups[nextGroupIndex].Length;

                    //There are more unprocessed characters left.
                } else {
                    //Swap processed character with the last unprocessed character
                    //so that we don't pick it until we process all characters in
                    //this group.
                    if (lastCharacterIndex != nextCharacterIndex) {
                        Char temp = characterGroups[nextGroupIndex][lastCharacterIndex];
                        characterGroups[nextGroupIndex][lastCharacterIndex] = characterGroups[nextGroupIndex][nextCharacterIndex];
                        characterGroups[nextGroupIndex][nextCharacterIndex] = temp;
                    }

                    //Decrement the number of unprocessed characters in
                    //this group.
                    charsRemainingInGroup[nextGroupIndex] = charsRemainingInGroup[nextGroupIndex] - 1;
                }

                //If we processed the last group, start all over.
                if (lastRemainingGroupsOrderIndex == 0) {
                    lastRemainingGroupsOrderIndex = remainingGroupsOrder.Length - 1;

                    //There are more unprocessed groups left.
                } else {
                    //Swap processed group with the last unprocessed group
                    //so that we don't pick it until we process all groups.
                    if (lastRemainingGroupsOrderIndex != nextRemainingGroupsOrderIndex) {
                        Int32 temp = remainingGroupsOrder[lastRemainingGroupsOrderIndex];
                        remainingGroupsOrder[lastRemainingGroupsOrderIndex] = remainingGroupsOrder[nextRemainingGroupsOrderIndex];
                        remainingGroupsOrder[nextRemainingGroupsOrderIndex] = temp;
                    }

                    //Decrement the number of unprocessed groups.
                    lastRemainingGroupsOrderIndex -= 1;
                }
            }

            //Convert password characters into a String and return the result.
            return new String(password);
        }

        /// <summary>Generates a random password.</summary>
        /// <param name="minimumLength">Minimum password length.</param>
        /// <param name="maximumLength">Maximum password length.</param>
        /// <returns>String containing a randomly generated password.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when minimumLength is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maximumLength is less than minimum length.</exception>
        public static String Generate(Int32 minimumLength, Int32 maximumLength) {
            if (minimumLength <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(minimumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanZero);
            }
            if (maximumLength < minimumLength) {
                throw new ArgumentOutOfRangeException(nameof(maximumLength), Strings.MustBeGreaterThanOrEqualToMinimumLength);
            }
            return Generate(minimumLength, maximumLength, SpecialCharacter.Yes, DigitCharacter.Yes, UpperCaseCharacter.Yes, LowerCaseCharacter.Yes);
        }

        /// <summary>Generates a random password.</summary>
        /// <param name="length">Desired password length.</param>
        /// <returns>String containing a randomly generated password.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length is not greater than zero.</exception>
        public static String Generate(Int32 length) {
            if (length <= Zero) {
                throw new ArgumentOutOfRangeException(nameof(length), Strings.MustBeGreaterThanZero);
            }
            return Generate(length, length, SpecialCharacter.Yes, DigitCharacter.Yes, UpperCaseCharacter.Yes, LowerCaseCharacter.Yes);
        }
    }
}
