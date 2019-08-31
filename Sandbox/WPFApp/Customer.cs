namespace WPFApp {

    using System;
    using Oceanware.Ocean.BusinessObject;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.ValidationRules;
    using Oceanware.Ocean.ValidationRules.InstanceRules;

    public class Customer : BusinessEntityBase {
        String _addressLineOne = String.Empty;
        String _bankName = String.Empty;
        DateTime _birthday = DateTime.Today;
        String _evaluation = String.Empty;
        String _firstName = String.Empty;
        String _lastName = String.Empty;
        String _middleName = String.Empty;
        String _password = String.Empty;
        String _passwordConfirm = String.Empty;

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 64, RuleSet = ValidationConstants.InsertUpdate)]
        public String AddressLineOne {
            get { return _addressLineOne; }
            set { base.SetPropertyValue(ref _addressLineOne, value); }
        }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(5, 30, RuleSet = ValidationConstants.InsertUpdate)]
        public String BankName {
            get { return _bankName; }
            set { base.SetPropertyValue(ref _bankName, value); }
        }

        public DateTime Birthday {
            get { return _birthday; }
            set { base.SetPropertyValue(ref _birthday, value); }
        }

        [CharacterFormatting(CharacterCasing.None, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(20, 100, AllowNullString.No, RuleSet = AppValidationConstants.InReview)]
        public String Evaluation {
            get { return _evaluation; }
            set { base.SetPropertyValue(ref _evaluation, value); }
        }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(2, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String FirstName {
            get { return _firstName; }
            set { base.SetPropertyValue(ref _firstName, value); }
        }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(2, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String LastName {
            get { return _lastName; }
            set { base.SetPropertyValue(ref _lastName, value); }
        }

        [CharacterFormatting(CharacterCasing.ProperName, RemoveSpace = RemoveSpace.MultipleSpaces)]
        [StringLengthValidator(15, AllowNullString.No)]
        public String MiddleName {
            get { return _middleName; }
            set { base.SetPropertyValue(ref _middleName, value); }
        }

        [StringLengthValidator(8, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String Password {
            get { return _password; }
            set { base.SetPropertyValue(ref _password, value); }
        }

        [ComparePropertyValidator(ComparisonType.Equal, nameof(Customer.Password))]
        public String PasswordConfirm {
            get { return _passwordConfirm; }
            set { base.SetPropertyValue(ref _passwordConfirm, value); }
        }

        public Customer() {
        }

        protected override void AddInstanceBusinessValidationRules() {
            this.InstanceValidationRulesManager.AddRule(new CompareDateInstanceValidationRule(() => DateTime.Now, ComparisonType.LessThanEqual), nameof(Birthday));
        }
    }
}
