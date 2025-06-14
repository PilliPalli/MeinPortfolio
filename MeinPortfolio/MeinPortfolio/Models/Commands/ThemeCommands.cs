using System.Threading.Tasks;
using MeinPortfolio.Services;

namespace MeinPortfolio.Models.Commands
{
    public class ThemeCommand : BaseCommand
    {
        private readonly ThemeService _themeService;

        public override string Name => "theme";
        public override string Description => "Change the terminal theme";
        public override string Usage => "theme [dark|light|hacker|retro|cyberpunk]";

        public ThemeCommand(ThemeService themeService)
        {
            _themeService = themeService;
        }

        public override async Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                return $"Current theme: {_themeService.CurrentTheme}\nAvailable themes: dark, light, hacker, retro, cyberpunk";
            }

            var themeName = args[0].ToLower();
            
            switch (themeName)
            {
                case "dark":
                    await _themeService.SetThemeAsync(ThemeType.Dark);
                    return "Theme changed to dark.";
                
                case "light":
                    await _themeService.SetThemeAsync(ThemeType.Light);
                    return "Theme changed to light.";
                
                case "hacker":
                    await _themeService.SetThemeAsync(ThemeType.Hacker);
                    return "Theme changed to hacker.";
                
                case "retro":
                    await _themeService.SetThemeAsync(ThemeType.Retro);
                    return "Theme changed to retro.";
                
                case "cyberpunk":
                    await _themeService.SetThemeAsync(ThemeType.Cyberpunk);
                    return "Theme changed to cyberpunk.";
                
                default:
                    return $"Unknown theme: {themeName}. Available themes: dark, light, hacker, retro, cyberpunk";
            }
        }
    }
}
