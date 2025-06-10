using System.Threading.Tasks;
using MeinPortfolio.Services;

namespace MeinPortfolio.Models.Commands
{
    public class CdCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public override string Name => "cd";
        public override string Description => "Change to a different section";
        public override string Usage => "cd [section]";

        public CdCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                return Task.FromResult($"Current location: {_navigationService.GetSectionPath()}\nUse 'cd [section]' to navigate to a different section.");
            }

            var section = args[0].ToLower();
            
            switch (section)
            {
                case "about":
                case "about_me":
                    _navigationService.NavigateTo(NavigationSection.About);
                    return Task.FromResult($"Navigated to About Me section.");
                
                case "projects":
                    _navigationService.NavigateTo(NavigationSection.Projects);
                    return Task.FromResult($"Navigated to Projects section.");
                
                case "contact":
                case "contact_me":
                    _navigationService.NavigateTo(NavigationSection.Contact);
                    return Task.FromResult($"Navigated to Contact section.");
                
                case "skills":
                    _navigationService.NavigateTo(NavigationSection.Skills);
                    return Task.FromResult($"Navigated to Skills section.");
                
                case "~":
                case "home":
                    _navigationService.GoHome();
                    return Task.FromResult($"Navigated to Home section.");
                
                case "..":
                    if (_navigationService.CanGoBack)
                    {
                        _navigationService.GoBack();
                        return Task.FromResult($"Navigated back to {_navigationService.GetSectionPath()}");
                    }
                    else
                    {
                        return Task.FromResult("Already at the top level.");
                    }
                
                default:
                    return Task.FromResult($"Section '{section}' not found. Available sections: about, projects, contact, skills, home");
            }
        }
    }

    public class PwdCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public override string Name => "pwd";
        public override string Description => "Print current section path";
        public override string Usage => "pwd";

        public PwdCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            return Task.FromResult(_navigationService.GetSectionPath());
        }
    }

    public class LsCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public override string Name => "ls";
        public override string Description => "List available commands or sections";
        public override string Usage => "ls";

        public LsCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            var section = _navigationService.CurrentSection;
            
            return section switch
            {
                NavigationSection.Home => Task.FromResult("about_me   projects   skills   contact_me"),
                NavigationSection.About => Task.FromResult("Name: Dein Name\nBeruf: Softwareentwickler\nSkills: C#, Blazor, Dapper, SQL\nErfahrung: 3 Jahre Softwareentwicklung"),
                NavigationSection.Projects => Task.FromResult(@"My Projects:

- Portfolio
  Description: Interactive terminal-based portfolio website
  Language: C#
  Stars: 5
  URL: https://github.com/deinname/Portfolio

- WPF-MembershipManager
  Description: Membership management system for swimming pools
  Language: C#
  Stars: 3
  URL: https://github.com/deinname/WPF-MembershipManager

- GarbageCollectionTool
  Description: Tool for managing garbage collection schedules
  Language: C#
  Stars: 2
  URL: https://github.com/deinname/GarbageCollectionTool

Developer Profile: Dein Name
Bio: Software Developer specializing in C# and Blazor
Location: Germany
Public Repositories: 10"),
                NavigationSection.Contact => Task.FromResult("Email: deine-email@example.com\nLinkedIn: linkedin.com/in/deinname\nGitHub: github.com/deinname"),
                NavigationSection.Skills => Task.FromResult("- C# (5/5)\n- Blazor (4/5)\n- SQL & Dapper (4/5)\n- ASP.NET Core (4/5)\n- Git & CI/CD (4/5)"),
                _ => Task.FromResult("Unknown section")
            };
        }
    }

    public class BackCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public override string Name => "back";
        public override string Description => "Navigate back to the previous section";
        public override string Usage => "back";

        public BackCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
                return Task.FromResult($"Navigated back to {_navigationService.GetSectionPath()}");
            }
            else
            {
                return Task.FromResult("No previous section to navigate back to.");
            }
        }
    }

    public class HomeCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public override string Name => "home";
        public override string Description => "Navigate to the home section";
        public override string Usage => "home";

        public HomeCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            _navigationService.GoHome();
            return Task.FromResult("Navigated to Home section.");
        }
    }
}