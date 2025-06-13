using MeinPortfolio.Services;
using MeinPortfolio;
using MeinPortfolio.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

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
        
        builder.Services.Configure<PortfolioConfig>(builder.Configuration.GetSection("Portfolio"));
        
        builder.Services.AddSingleton<CommandService>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddScoped<ThemeService>();
        builder.Services.AddScoped<AuthService>();

        await builder.Build().RunAsync();
    }
}