namespace Oceanware.Ocean.ValidationRules {

    public interface ISupportInstanceValidationRules {

        /// <summary>
        /// Enables classes to expose instance validation rules through the <c>InstanceValidationRulesManager</c> property.
        /// </summary>
        /// <returns><see cref="ValidationRulesManager"/> for this instance.</returns>
        ValidationRulesManager InstanceValidationRulesManager { get; }
    }
}
