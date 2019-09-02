namespace Oceanware.Ocean.Rules {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.ValidationRules;

    /// <summary>
    /// Class ModelRulesInvoker validates a class that uses Ocean validation rules.
    /// </summary>
    public class ModelRulesInvoker : IModelRulesInvoker {
        static readonly Object LockObject = new Object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelRulesInvoker"/> class.
        /// </summary>
        public ModelRulesInvoker() {
        }

        /// <summary>Checks all shared and instance validation rules.</summary>
        /// <typeparam name="T">Entity class type to validate.</typeparam>
        /// <param name="target">The target instance of entity class to validate.</param>
        /// <returns>ValidationResult that encapsulates the result of the validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        public ValidationResult CheckAllValidationRules<T>(T target) where T : class {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }

            var validationRulesManager = GetValidationRulesManagerForTarget(target);

            var validationErrors = new List<KeyValuePair<String, BrokenRule>>();

            if (validationRulesManager.HasRules) {
                foreach (var kvp in validationRulesManager.RulesDictionary) {
                    foreach (IValidationRule validationRule in kvp.Value.List) {
                        if (validationRule.IsValid(target, kvp.Key) == false) {
                            validationErrors.Add(new KeyValuePair<String, BrokenRule>(kvp.Key, new BrokenRule(validationRule.RuleTypeName, kvp.Key, validationRule.FinalErrorMessage, validationRule.RuleType)));
                        }
                    }
                }
            }

            if (target is ISupportInstanceValidationRules supportInstanceValidationRules) {
                if (supportInstanceValidationRules.InstanceValidationRulesManager.HasRules) {
                    foreach (var kvp in supportInstanceValidationRules.InstanceValidationRulesManager.RulesDictionary) {
                        foreach (IValidationRule validationRule in kvp.Value.List) {
                            if (validationRule.IsValid(target, kvp.Key) == false) {
                                validationErrors.Add(new KeyValuePair<String, BrokenRule>(kvp.Key, new BrokenRule(validationRule.RuleTypeName, kvp.Key, validationRule.FinalErrorMessage, RuleType.Instance)));
                            }
                        }
                    }
                }
            }

            if (validationErrors.Any()) {
                return new ValidationResult(validationErrors);
            }
            return ValidationResult.Success;
        }

        /// <summary>Checks all shared and instance validation rules for the property.</summary>
        /// <typeparam name="T">Entity class type to validate.</typeparam>
        /// <param name="target">The target instance class to validate.</param>
        /// <param name="propertyName">The property name on the target instance class to validate.</param>
        /// <returns>ValidationResult that encapsulates the result of the validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public ValidationResult CheckAllValidationRulesForProperty<T>(T target, String propertyName) where T : class {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }

            var validationRulesManager = GetValidationRulesManagerForTarget(target);

            var validationErrors = new List<KeyValuePair<String, BrokenRule>>();

            if (validationRulesManager.HasRules) {
                foreach (var kvp in validationRulesManager.RulesDictionary.Where(x => x.Key == propertyName)) {
                    foreach (IValidationRule validationRule in kvp.Value.List) {
                        if (validationRule.IsValid(target, kvp.Key) == false) {
                            validationErrors.Add(new KeyValuePair<String, BrokenRule>(kvp.Key, new BrokenRule(validationRule.RuleTypeName, kvp.Key, validationRule.FinalErrorMessage, validationRule.RuleType)));
                        }
                    }
                }
            }

            if (target is ISupportInstanceValidationRules supportInstanceValidationRules) {
                if (supportInstanceValidationRules.InstanceValidationRulesManager.HasRules) {
                    foreach (var kvp in supportInstanceValidationRules.InstanceValidationRulesManager.RulesDictionary.Where(x => x.Key == propertyName)) {
                        foreach (IValidationRule validationRule in kvp.Value.List) {
                            if (validationRule.IsValid(target, kvp.Key) == false) {
                                validationErrors.Add(new KeyValuePair<String, BrokenRule>(kvp.Key, new BrokenRule(validationRule.RuleTypeName, kvp.Key, validationRule.FinalErrorMessage, RuleType.Instance)));
                            }
                        }
                    }
                }
            }

            if (validationErrors.Any()) {
                return new ValidationResult(validationErrors);
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Using the character case rule, formats the property value.
        /// </summary>
        /// <typeparam name="T">Entity class type to validate.</typeparam>
        /// <param name="target">The target instance class to validate.</param>
        /// <param name="propertyName">The property name on the target instance class to validate.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns>Case corrected and formatted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        public String FormatPropertyValueUsingCharacterCaseRule<T>(T target, String propertyName, String propertyValue) where T : class {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }
            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(propertyName));
            }
            var resultValue = propertyValue;
            var characterFormattingRulesManager = GetCharacterFormattingRulesManagerForTarget(target);
            if (characterFormattingRulesManager.HasRules && characterFormattingRulesManager.GetRuleForProperty(propertyName) is CharacterFormat characterFormat) {
                resultValue = characterFormat.CharacterCasing != CharacterCasing.None ? FormatText.ApplyCharacterCasing(propertyValue, characterFormat.CharacterCasing, characterFormat.PhoneExtension) : propertyValue;
                if (!String.IsNullOrWhiteSpace(resultValue) && characterFormat.RemoveSpace == RemoveSpace.MultipleSpaces) {
                    resultValue = Regex.Replace(resultValue, @"\s+", " ");
                } else if (!String.IsNullOrWhiteSpace(resultValue) && characterFormat.RemoveSpace == RemoveSpace.AllSpaces) {
                    resultValue = resultValue.Replace(" ", String.Empty).Trim();
                }
            }

            return resultValue;
        }

        CharacterFormattingRulesManager GetCharacterFormattingRulesManagerForTarget<T>(T target) where T : class {
            CharacterFormattingRulesManager characterFormattingRulesManager = SharedCharacterFormattingRules.GetManager(this.GetType());

            if (!characterFormattingRulesManager.RulesLoaded) {
                lock (LockObject) {
                    foreach (PropertyInfo prop in target.GetType().GetProperties()) {
                        if (prop.GetCustomAttribute(typeof(CharacterFormattingAttribute), false) is CharacterFormattingAttribute characterFormattingAttribute) {
                            characterFormattingRulesManager.AddRule(characterFormattingAttribute, prop.Name);
                        }
                    }
                }
                characterFormattingRulesManager.SetRulesLoaded();
            }

            return characterFormattingRulesManager;
        }

        ValidationRulesManager GetValidationRulesManagerForTarget<T>(T target) where T : class {
            ValidationRulesManager validationRulesManager = SharedValidationRules.GetManager(target.GetType());

            if (!validationRulesManager.RulesLoaded) {
                lock (LockObject) {
                    foreach (PropertyInfo prop in target.GetType().GetProperties()) {
                        foreach (BaseValidatorAttribute atr in prop.GetCustomAttributes(typeof(BaseValidatorAttribute), false)) {
                            validationRulesManager.AddRule(atr, prop.Name);
                        }
                    }
                }
                validationRulesManager.SetRulesLoaded();
            }

            return validationRulesManager;
        }
    }
}
