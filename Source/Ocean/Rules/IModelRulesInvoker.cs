namespace Oceanware.Ocean.Rules {

    using System;
    using Oceanware.Ocean.ValidationRules;

    /// <summary>
    /// Interface IModelRulesInvoker has methods to validate a class that uses <c>Oceanware.Ocean.ValidationRules</c> validators.
    /// </summary>
    public interface IModelRulesInvoker {

        /// <summary>
        /// <summary>Checks all shared and instance validation rules.</summary>
        /// </summary>
        /// <typeparam name="T">Entity class type to validate.</typeparam>
        /// <param name="target">The target instance of entity class to validate.</param>
        /// <returns>ValidationResult that encapsulates the result of the validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        ValidationResult CheckAllValidationRules<T>(T target) where T : class;

        /// <summary>
        /// Checks all shared and instance validation rules for the property.
        /// </summary>
        /// <typeparam name="T">Entity class type to validate.</typeparam>
        /// <param name="target">The target instance class to validate.</param>
        /// <param name="propertyName">The property name on the target instance class to validate.</param>
        /// <returns>ValidationResult that encapsulates the result of the validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when target is null.</exception>
        /// <exception cref="T:Oceanware.OceanValidation.ArgumentNullEmptyWhiteSpaceException">Thrown when propertyName is null, empty, or white space.</exception>
        ValidationResult CheckAllValidationRulesForProperty<T>(T target, String propertyName) where T : class;

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
        String FormatPropertyValueUsingCharacterCaseRule<T>(T target, String propertyName, String propertyValue) where T : class;
    }
}
