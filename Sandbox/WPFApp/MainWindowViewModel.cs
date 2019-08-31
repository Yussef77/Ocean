namespace WPFApp {

    using System;
    using System.Collections.Generic;
    using Oceanware.Ocean.Infrastructure;
    using Oceanware.Ocean.ValidationRules;

    public class MainWindowViewModel : ObservableObject {
        public Customer Customer { get; private set; }

        public IEnumerable<String> RuleSets { get; }

        public MainWindowViewModel() {
            // TODO Developers, you can make a decision to:
            //   run CheckAllRules on the object and bind to the UI with all invalid fields showing immediately.
            //   run CheckAllRules only on objects that are being hydrated from the database, and bind to the UI with all invalid fields showing immediately.
            //   not run CheckAllRules on new objects.
            //   this is application specific, do what is correct for your app.
            //
            //   I HIGHLY recommend that the service that returns the object either a new object or object hydrated from the database runs the rules.  
            //     In a real app you would not create an entity instance in the view model.

            var customer = new Customer();
            customer.CheckAllRules();  // see above options.
            this.RuleSets = new List<String> { ValidationConstants.Insert, ValidationConstants.Update, AppValidationConstants.InReview };
            customer.ActiveRuleSet = ValidationConstants.Insert;
            this.Customer = customer;

        }
    }
}
