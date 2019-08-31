namespace BlazorServerSideApp.Model {

    using System;
    using System.Reflection;
    using Oceanware.Ocean.Extensions;
    using Oceanware.Ocean.ValidationRules;

    /// <summary>Class NoJavaInStringInstanceValidationRule.
    /// <para>This is a demo of using a simple class to provide a dynamic validation rule.</para>
    /// <para>This demo rule looks for Java in the input and returns IsValid <c>false</c> if found.</para>
    /// Derives from the <see cref="T:Oceanware.OceanValidation.IValidationRule"/></summary>
    /// <seealso cref="Oceanware.OceanValidation.IValidationRule" />
    public class NoJavaInStringInstanceValidationRule : IValidationRule {
        public String FinalErrorMessage { get; private set; }

        public String RuleSet { get; set; }

        public RuleType RuleType => RuleType.Instance;

        public String RuleTypeName => this.GetType().Name;

        public Boolean IsValid(Object target, String propertyName) {
            if (target is null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (String.IsNullOrWhiteSpace(propertyName)) {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            var targetValue = Convert.ToString(propertyInfo.GetValue(target, null));

            if (String.IsNullOrWhiteSpace(targetValue)) {
                return true;
            }

            if (!targetValue.Contains("Java")) {
                return true;
            }
            this.FinalErrorMessage = $"{propertyName.GetWords()} cannot contain Java in the value";
            return false;
        }
    }
}
