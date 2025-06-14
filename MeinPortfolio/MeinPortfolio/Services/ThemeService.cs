using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MeinPortfolio.Services
{
    public enum ThemeType
    {
        Dark,
        Light,
        Hacker,
        Retro,
        Cyberpunk
    }

    public class ThemeService
    {
        private readonly IJSRuntime _jsRuntime;
        private ThemeType _currentTheme = ThemeType.Dark;

        public event Action<ThemeType> OnThemeChanged;

        public ThemeType CurrentTheme => _currentTheme;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            var storedTheme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "terminal_theme");
            if (!string.IsNullOrEmpty(storedTheme) && Enum.TryParse<ThemeType>(storedTheme, out var theme))
            {
                _currentTheme = theme;
                await ApplyThemeAsync(_currentTheme);
            }
            else
            {
                await ApplyThemeAsync(ThemeType.Dark);
            }
        }

        public async Task SetThemeAsync(ThemeType theme)
        {
            if (_currentTheme != theme)
            {
                _currentTheme = theme;
                await ApplyThemeAsync(theme);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "terminal_theme", theme.ToString());
                OnThemeChanged?.Invoke(theme);
            }
        }

        private async Task ApplyThemeAsync(ThemeType theme)
        {
            string bg, text, border;

            switch (theme)
            {
                case ThemeType.Dark:
                    bg = "#121212"; text = "#33ff33"; border = "#33ff33";
                    break;
                case ThemeType.Light:
                    bg = "#f0f0f0"; text = "#121212"; border = "#121212";
                    break;
                case ThemeType.Hacker:
                    bg = "#000000"; text = "#00ff00"; border = "#00ff00";
                    break;
                case ThemeType.Retro:
                    bg = "#2b2b2b"; text = "#ff8c00"; border = "#ff8c00";
                    break;
                case ThemeType.Cyberpunk:
                    bg = "#272932"; text = "#CB1DCD"; border = "#37EBF3";
                    break;
                default:
                    return;
            }

            await _jsRuntime.InvokeVoidAsync("applyTheme", theme.ToString(), new { bg, text, border });
        }

    }
}
