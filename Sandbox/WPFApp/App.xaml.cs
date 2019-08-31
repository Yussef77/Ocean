namespace WPFApp {

    using System.Windows;
    using Oceanware.Ocean.InputStringRules;

    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            // TODO Developers - you can load your additional checks from a database, etc.
            //  In all of my apps, I allow the user to create new checks while running the app and then persist them to the database.
            //  When adding new checks at runtime, remember to add them to the below cache as well as the database. Optionally, add to the database and then refresh the cache from the database.
            var defaultRules = CharacterCasingChecks.GetChecks();
            defaultRules.Add(new CharacterCasingCheck("Us Bank", "US Bank"));
            CharacterCasingChecks.SetGetChecksSource(() => defaultRules);
        }
    }
}
