namespace BlazorServerSideApp {
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Oceanware.Ocean.InputStringRules;

    public class Program {

        public static IHostBuilder CreateHostBuilder(String[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Main(String[] args) {
            LoadCaseCorrectionRules();
            CreateHostBuilder(args).Build().Run();
        }

        static void LoadCaseCorrectionRules() {
            var defaultRules = CharacterCasingChecks.GetChecks();
            defaultRules.Add(new CharacterCasingCheck("Us Bank", "US Bank"));
            CharacterCasingChecks.SetGetChecksSource(() => defaultRules);
        }
    }
}
