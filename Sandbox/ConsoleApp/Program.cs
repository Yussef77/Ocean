/// <summary>
/// The purpose of the demo is to show how services such as WebAPI, or data services that are using Entity Framework attributes as well as Ocean Validation, can easily validate a class or class property using the ModelValidator.
/// </summary>
namespace ConsoleApp {

    using System;
    using Oceanware.Ocean.ValidationRules;
    using Oceanware.Ocean.Rules;
    using Oceanware.Ocean.ValidationRules.InstanceRules;

    class Customer : IRuleSet, ISupportInstanceValidationRules {

        [field: NonSerialized]
        ValidationRulesManager _instanceValidationRulesManager;

        public String ActiveRuleSet { get; set; }

        [StringLengthValidator(5, 64, RuleSet = ValidationConstants.InsertUpdate)]
        public String AddressLineOne { get; set; } = String.Empty;

        public DateTime Birthday { get; set; } = DateTime.Today;

        [StringLengthValidator(20, 100, AllowNullString.No)]
        public String Evaluation { get; set; } = String.Empty;

        [StringLengthValidator(2, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String FirstName { get; set; } = String.Empty;

        public ValidationRulesManager InstanceValidationRulesManager {
            get {
                return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
            }
        }

        [StringLengthValidator(5, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String LastName { get; set; } = String.Empty;

        [StringLengthValidator(15, AllowNullString.No)]
        public String MiddleName { get; set; } = String.Empty;

        [StringLengthValidator(8, 20, RuleSet = ValidationConstants.InsertUpdate)]
        public String Password { get; set; } = String.Empty;

        [ComparePropertyValidator(ComparisonType.Equal, nameof(Customer.Password))]
        public String PasswordConfirm { get; set; } = String.Empty;

        public Customer() {
            this.AddInstanceBusinessValidationRules();
        }

        protected virtual void AddInstanceBusinessValidationRules() {
            this.InstanceValidationRulesManager.AddRule(new CompareDateInstanceValidationRule(() => DateTime.Now, ComparisonType.LessThanEqual), nameof(Birthday));
        }
    }

    class CustomerService {

        public void Validate() {
            Console.WriteLine("Validate Class --------------------------");
            var customer = new Customer();
            customer.ActiveRuleSet = ValidationConstants.Update;

            var modelRulesInvoker = new ModelRulesInvoker();
            var validationResult = modelRulesInvoker.CheckAllValidationRules(customer);

            if (!validationResult.IsValid) {
                foreach (var item in validationResult.ValidationErrors) {
                    Console.WriteLine($"{item.Value.PropertyName} - {item.Value.ErrorMessage}");
                }
            }
            Console.WriteLine(String.Empty);
            Console.WriteLine("Validate Class Property --------------------------");
            customer.Password = "password!!";
            customer.PasswordConfirm = "p";
            validationResult = modelRulesInvoker.CheckAllValidationRulesForProperty(customer, nameof(Customer.PasswordConfirm));
            if (!validationResult.IsValid) {
                foreach (var item in validationResult.ValidationErrors) {
                    Console.WriteLine($"{item.Value.PropertyName} - {item.Value.ErrorMessage}");
                }
            }
        }
    }

    class Program {

        static void Main() {
            var cs = new CustomerService();
            cs.Validate();
        }
    }
}
