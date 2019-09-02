namespace Oceanware.Ocean.Blazor {

    using System;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Oceanware.Ocean.Blazor.Properties;
    using Oceanware.Ocean.Rules;

    /// <summary>
    /// Class OceanValidator.
    /// Derives from the <c>ComponentBase</c>. />
    /// </summary>
    public class OceanValidator : ComponentBase {
        const String StringTypeName = "String";
        readonly IModelRulesInvoker _modelRulesInvoker = new ModelRulesInvoker();

        /// <summary>Gets or sets the current edit context.</summary>
        /// <value>The current edit context.</value>
        [CascadingParameter]
        EditContext CurrentEditContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OceanValidator"/> class.
        /// </summary>
        public OceanValidator() {
        }

        /// <summary>Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree.</summary>
        /// <exception cref="InvalidOperationException">Thrown when no cascading parameter of type <c>EditContent</c> is supplied.</exception>
        protected override void OnInitialized() {
            if (this.CurrentEditContext == null) {
                throw new InvalidOperationException(String.Format(Resources.RequiresCascadingParameterFormat, nameof(OceanValidator), nameof(EditContext), nameof(OceanValidator), nameof(EditForm)));
            }

            var validationMessageStore = new ValidationMessageStore(this.CurrentEditContext);
            this.CurrentEditContext.OnValidationRequested += (sender, e) => ValidateModel((EditContext)sender, validationMessageStore);
            this.CurrentEditContext.OnFieldChanged += (sender, e) => ValidateField(this.CurrentEditContext, validationMessageStore, e.FieldIdentifier);
        }

        void ValidateField(EditContext editContext, ValidationMessageStore validationMessageStore, in FieldIdentifier fieldIdentifier) {
            if (editContext is null) {
                throw new ArgumentNullException(nameof(editContext));
            }

            if (validationMessageStore is null) {
                throw new ArgumentNullException(nameof(validationMessageStore));
            }

            var validationResult = _modelRulesInvoker.CheckAllValidationRulesForProperty(editContext.Model, fieldIdentifier.FieldName);

            validationMessageStore.Clear(fieldIdentifier);
            if (!validationResult.IsValid) {
                foreach (var kvp in validationResult.ValidationErrors) {
                    validationMessageStore.Add(editContext.Field(kvp.Key), kvp.Value.ErrorMessage);
                }
            }

            var propertyInfo = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName);
            if (propertyInfo.PropertyType.Name == StringTypeName) {
                if (propertyInfo.GetValue(fieldIdentifier.Model, null) is String propertyValue) {
                    var newValue = _modelRulesInvoker.FormatPropertyValueUsingCharacterCaseRule(editContext.Model, fieldIdentifier.FieldName, propertyValue);
                    propertyInfo.SetValue(fieldIdentifier.Model, newValue);
                }
            }
            editContext.NotifyValidationStateChanged();
        }

        void ValidateModel(EditContext editContext, ValidationMessageStore validationMessageStore) {
            if (editContext is null) {
                throw new ArgumentNullException(nameof(editContext));
            }

            if (validationMessageStore is null) {
                throw new ArgumentNullException(nameof(validationMessageStore));
            }

            var validationResult = _modelRulesInvoker.CheckAllValidationRules(editContext.Model);

            validationMessageStore.Clear();
            if (!validationResult.IsValid) {
                foreach (var kvp in validationResult.ValidationErrors) {
                    validationMessageStore.Add(editContext.Field(kvp.Key), kvp.Value.ErrorMessage);
                }
            }
            editContext.NotifyValidationStateChanged();
        }
    }
}
