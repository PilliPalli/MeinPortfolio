﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MeinPortfolio.Models;
using MeinPortfolio.Models.Commands;
using MeinPortfolio.Services;
using System.Net.Http;

namespace MeinPortfolio.Pages
{
    public partial class Home : IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private CommandService CommandService { get; set; }
        [Inject] private NavigationService NavigationService { get; set; }
        [Inject] private ThemeService ThemeService { get; set; }
        [Inject] private IHttpClientFactory HttpClientFactory { get; set; }

        private List<OutputLine> output = new();
        private string input = "";
        private ElementReference inputElement;
        private bool showContactForm = false;

        protected override async Task OnInitializedAsync()
        {
            // Register commands
            RegisterCommands();

            // Initialize theme
            await ThemeService.InitializeAsync();
            ThemeService.OnThemeChanged += HandleThemeChanged;

            // Subscribe to navigation events
            NavigationService.OnNavigate += HandleNavigate;

            // Add welcome message
            output.Add(new OutputLine("Welcome to your interactive Terminal Portfolio. Type 'help' for available commands."));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initTerminal");
                await FocusInputAsync();
            }
        }

        private void RegisterCommands()
        {
            // Help command
            CommandService.RegisterCommand(new HelpCommand(CommandService));

            // Navigation commands
            CommandService.RegisterCommand(new CdCommand(NavigationService));
            CommandService.RegisterCommand(new PwdCommand(NavigationService));
            CommandService.RegisterCommand(new LsCommand(NavigationService));
            CommandService.RegisterCommand(new BackCommand(NavigationService));
            CommandService.RegisterCommand(new HomeCommand(NavigationService));

            // System commands
            CommandService.RegisterCommand(new ClearCommand());
            CommandService.RegisterCommand(new EchoCommand());
            CommandService.RegisterCommand(new DateCommand());
            CommandService.RegisterCommand(new WhoamiCommand());

            // Theme commands
            CommandService.RegisterCommand(new ThemeCommand(ThemeService));

            // Easter egg commands
           
            CommandService.RegisterCommand(new HireMeCommand());
            CommandService.RegisterCommand(new AsciiArtCommand());

            // Resume command
            CommandService.RegisterCommand(new ResumeCommand(JSRuntime));

            // GitHub command
            CommandService.RegisterCommand(new GitHubCommand(HttpClientFactory.CreateClient()));

            
        }



        private async Task HandleKeyPress(KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "Enter":
                    await ExecuteCommandAsync();
                    break;
                case "ArrowUp":
                    input = CommandService.GetPreviousCommand();
                    StateHasChanged();
                    break;
                case "ArrowDown":
                    input = CommandService.GetNextCommand();
                    StateHasChanged();
                    break;
                case "Tab":
                    input = GetAutoCompleteSuggestion(input);
                    StateHasChanged();
                    break;
            }
        }


        private List<string> commandList = new List<string>
{
    "help",
    "ls",
    "home",
    "matrix",
    "clear",
    "whoami",
    "cd",
    "about_me",
    "skills",
    "github"
};

        private string GetAutoCompleteSuggestion(string currentInput)
        {
            if (string.IsNullOrWhiteSpace(currentInput))
                return currentInput;

            var suggestion = commandList.FirstOrDefault(cmd => cmd.StartsWith(currentInput, StringComparison.OrdinalIgnoreCase));

            return suggestion ?? currentInput;
        }



        private async Task ExecuteCommandAsync()
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            var commandInput = input.Trim();
            output.Add(new OutputLine($"> {commandInput}", isCommand: true));

            var result = await CommandService.ExecuteCommandAsync(commandInput);

            if (result.Output == "clear")
            {
                output.Clear();
                output.Add(new OutputLine("Welcome to your interactive Terminal Portfolio. Type 'help' for available commands."));
                input = "";
                StateHasChanged();
                return;
            }


            if (!string.IsNullOrEmpty(result.Output))
            {
                if (result.IsHtml)
                {
                    output.Add(new OutputLine(result.Output, isHtml: true));
                }
                else if (result.IsAnimation)
                {
                    
                    await JSRuntime.InvokeVoidAsync("startMatrix");
                }
                else
                {
                    foreach (var line in result.Output.Split('\n'))
                    {
                        output.Add(new OutputLine(line));
                    }
                }
            }

            input = "";
            await FocusInputAsync();
            StateHasChanged();

            // Scroll to bottom
            await JSRuntime.InvokeVoidAsync("eval", "document.getElementById('terminal-body').scrollTop = document.getElementById('terminal-body').scrollHeight");
        }


        private async Task FocusInputAsync()
        {
            await JSRuntime.InvokeVoidAsync("eval", "document.querySelector('.terminal-input input').focus()");
        }

        private void HandleThemeChanged(ThemeType theme)
        {
            StateHasChanged();
        }

        private void HandleNavigate(NavigationSection section)
        {
            StateHasChanged();
        }

        public void ShowContactForm(bool show)
        {
            showContactForm = show;
            StateHasChanged();
        }

        public void Dispose()
        {
            // Unsubscribe from events
            ThemeService.OnThemeChanged -= HandleThemeChanged;
            NavigationService.OnNavigate -= HandleNavigate;
        }
    }
}
