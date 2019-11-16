namespace BlazorClientSideApp {

    using System;
    using Microsoft.AspNetCore.Blazor.Hosting;

    public class Program {

        public static IWebAssemblyHostBuilder CreateHostBuilder(String[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();

        public static void Main(String[] args) {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
