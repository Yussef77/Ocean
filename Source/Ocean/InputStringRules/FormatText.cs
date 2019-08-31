namespace Oceanware.Ocean.InputStringRules {

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents FormatText, provides text formatting
    /// </summary>
    public static class FormatText {

        /// <summary>
        /// Initializes the <see cref="FormatText"/> class.
        /// </summary>
        static FormatText() {
        }

        /// <summary>Corrects the text character casing and optionally format phone fields similar to Microsoft Outlook.</summary>
        /// <param name="inputString">String to be case corrected and optionally formatted.</param>
        /// <param name="characterCase">Character case and format.</param>
        /// <param name="phoneExtension">The phone extension.</param>
        /// <returns>String case corrected and optionally formatted.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value characterCase is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value phoneExtension is not defined.</exception>
        public static String ApplyCharacterCasing(String inputString, CharacterCasing characterCase, PhoneExtension phoneExtension) {
            if (!Enum.IsDefined(typeof(CharacterCasing), characterCase)) {
                throw new InvalidEnumArgumentException(nameof(characterCase), (Int32)characterCase, typeof(CharacterCasing));
            }
            if (!Enum.IsDefined(typeof(PhoneExtension), phoneExtension)) {
                throw new InvalidEnumArgumentException(nameof(phoneExtension), (Int32)phoneExtension, typeof(PhoneExtension));
            }

            inputString = inputString.Trim();

            if (inputString.Length == 0) {
                return inputString;
            }

            Int32 intX;

            switch (characterCase) {
                case CharacterCasing.None:
                    return inputString;

                case CharacterCasing.LowerCase:
                    return inputString.ToLower();

                case CharacterCasing.UpperCase:
                    return inputString.ToUpper();

                case CharacterCasing.PhoneWithDashesNoProperName:
                case CharacterCasing.PhoneWithDotsNoProperName:
                case CharacterCasing.PhoneNoProperName:
                    return FormatOutLookPhone(inputString, characterCase, phoneExtension);

                case CharacterCasing.PhoneWithDashesUpper:
                case CharacterCasing.PhoneWithDotsUpper:
                case CharacterCasing.PhoneUpper:
                    return FormatOutLookPhone(inputString, characterCase, phoneExtension).ToUpper();
            }

            inputString = inputString.ToLower();

            String previous = " ";
            String previousTwo = "  ";
            String previousThree = "   ";
            String charString;

            for (intX = 0; intX < inputString.Length; intX++) {
                charString = inputString.Substring(intX, 1);

                if (Char.IsLetter(Convert.ToChar(charString)) && charString != charString.ToUpper()) {
                    if (previous == " " || previous == "." || previous == "-" || previous == "/" || previousThree == " O'" || previousTwo == "Mc") {
                        inputString = inputString.Remove(intX, 1);
                        inputString = inputString.Insert(intX, charString.ToUpper());
                        previous = charString.ToUpper();
                    } else {
                        previous = charString;
                    }
                } else {
                    previous = charString;
                }

                previousTwo = previousTwo.Substring(1, 1) + previous;
                previousThree = previousThree.Substring(1, 1) + previousThree.Substring(2, 1) + previous;
            }

            intX = inputString.IndexOf("'", StringComparison.Ordinal);

            if (intX == 1) {
                String insertString = inputString.Substring(2, 1).ToUpper();
                inputString = inputString.Remove(2, 1);
                inputString = inputString.Insert(2, insertString);
            }

            try {
                intX = inputString.IndexOf("'", 3, StringComparison.Ordinal);

                if (intX > 3 && inputString.Substring(intX - 2, 1) == " ") {
                    String insertString = inputString.Substring(intX + 1, 1).ToUpper();
                    inputString = inputString.Remove(intX + 1, 1);
                    inputString = inputString.Insert(intX + 1, insertString);
                }
            } catch {
            }

            //never remove this code
            inputString += " ";

            foreach (CharacterCasingCheck check in CharacterCasingChecks.GetChecks()) {
                if (inputString.Contains(check.LookFor)) {
                    Int32 foundIndex = inputString.IndexOf(check.LookFor, StringComparison.Ordinal);

                    if (foundIndex > -1) {
                        inputString = inputString.Remove(foundIndex, check.LookFor.Length);
                        inputString = inputString.Insert(foundIndex, check.ReplaceWith);
                    }
                }
            }

            if (characterCase == CharacterCasing.PhoneProperName || characterCase == CharacterCasing.PhoneWithDashesProperName || characterCase == CharacterCasing.PhoneWithDotsProperName) {
                inputString = FormatOutLookPhone(inputString, characterCase, phoneExtension);
            }

            return inputString.Trim();
        }

        static String FormatOutLookPhone(String inputString, CharacterCasing characterCase, PhoneExtension phoneExtension) {
            if (inputString.Trim().Length == 0) {
                return inputString;
            }

            String tempCasted = inputString + " ";

            try {
                String temp = tempCasted;
                Int32 intX = temp.IndexOf(" ", 8, StringComparison.Ordinal);

                if (intX > 0) {
                    temp = inputString.Substring(0, intX);
                    temp = temp.Replace("(", "");
                    temp = temp.Replace(")", "");
                    temp = temp.Replace(" ", "");
                    temp = temp.Replace("-", "");
                    temp = temp.Replace(".", "");

                    var characterCaseName = Enum.GetName(typeof(CharacterCasing), characterCase);
                    String extension = null;

                    if (Int64.TryParse(temp, out var lngTemp) && temp.Length == 10) {
                        if (phoneExtension == PhoneExtension.Keep) {
                            extension = tempCasted.Substring(intX).Trim();
                        }
                        if (characterCaseName.Contains("Dots")) {
                            tempCasted = lngTemp.ToString("###.###.####");
                        } else if (characterCaseName.Contains("Dashes")) {
                            tempCasted = lngTemp.ToString("###-###-####");
                        } else {
                            tempCasted = lngTemp.ToString("(###) ###-####");
                        }

                        if (phoneExtension == PhoneExtension.Keep && !String.IsNullOrWhiteSpace(extension)) {
                            tempCasted = tempCasted + "  " + extension;
                        }
                    }
                }
            } catch {
            }

            return tempCasted;
        }
    }
}
