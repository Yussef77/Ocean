namespace BlazorServerSideApp.Model {

    using System;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.ValidationRules;
    using Oceanware.Ocean.ValidationRules.InstanceRules;

public class Customer : IRuleSet, ISupportInstanceValidationRules {

    [field: NonSerialized]
    ValidationRulesManager _instanceValidationRulesManager;

    public String ActiveRuleSet { get; set; }

    [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
    [StringLengthValidator(5, 64, RuleSet = ValidationConstants.InsertUpdate)]
    public String AddressLineOne { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 30, RuleSet = ValidationConstants.InsertUpdate)]
        public String BankName { get; set; } = String.Empty;

        public DateTime Birthday { get; set; } = DateTime.Today;

        [CharacterFormatting(CharacterCasing.None, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(20, 100, AllowNullString.No, RuleSet = AppValidationConstants.InReview)]
        public String Evaluation { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(2, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String FirstName { get; set; } = String.Empty;

        public ValidationRulesManager InstanceValidationRulesManager {
            get {
                return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
            }
        }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String LastName { get; set; } = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(15, AllowNullString.No)]
        public String MiddleName { get; set; } = String.Empty;

        [PasswordValidator(8, 20, LowerCaseCharacter.Yes, UpperCaseCharacter.Yes, DigitCharacter.Yes, SpecialCharacter.Yes)]
        public String Password { get; set; } = String.Empty;

        [ComparePropertyValidator(ComparisonType.Equal, nameof(Customer.Password))]
        public String PasswordConfirm { get; set; } = String.Empty;

        public Customer() {
            this.AddInstanceBusinessValidationRules();
        }

        protected virtual void AddInstanceBusinessValidationRules() {
            this.InstanceValidationRulesManager.AddRule(new NoJavaInStringInstanceValidationRule(), nameof(MiddleName));
            this.InstanceValidationRulesManager.AddRule(new CompareDateInstanceValidationRule(() => DateTime.Now, ComparisonType.LessThanEqual), nameof(Birthday));
        }
    }
}
