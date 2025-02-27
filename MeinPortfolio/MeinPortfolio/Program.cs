using MeinPortfolio.Services;
using MeinPortfolio;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MeinPortfolio;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddHttpClient();

        // Register services
        builder.Services.AddSingleton<CommandService>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddScoped<ThemeService>();

        await builder.Build().RunAsync();
    }
}
