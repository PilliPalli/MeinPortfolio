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
                    return Task.FromResult($"Section '{section}' not found. Available sections: about, projects, contact, home");
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
        public override string Description => "List sections in home or show content of the current section";
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
                NavigationSection.Home => Task.FromResult("about_me   projects   contact_me"),

                NavigationSection.About => Task.FromResult(
                    "Name: Moritz Nicola Kreis\n" +
                    "Alter: 22 Jahre alt\n" +
                    "Abschluss: Staatlich geprüfter Wirtschaftsinformatiker\n" +
                    "Erfahrung: Berufseinsteiger\n" +
                    "Kenntnisse: C#, Blazor, Git, SQL\n" +
                    "Hobbys: Fitnessstudio, Freunde treffen"),
                
                NavigationSection.Projects => Task.FromResult(@"Meine Projekte:

                    - Portfolio
                      Beschreibung: Interaktive Portfolio-Website im Terminal-Stil mit Informationen über mich
                      Technologie: C#, Blazor WebAssembly

                    - Schwimmbad-Verwaltung
                      Beschreibung: Anwendung zur Mitgliederverwaltung eines fiktiven Schwimmbads mit MSSQL-Anbindung
                      Technologie: C#, WPF

                    - Garbage Collection Tool
                      Beschreibung: Tool zur automatischen Löschung temporärer Dateien mit Scheduler, Login-System und DB-Anbindung
                      Technologie: C#, WPF

                    - Code-Kommentierungstool
                      Beschreibung: Tool zur automatischen Kommentierung von Code mittels ChatGPT-API, einstellbar nach Detailgrad
                      Technologie: C#, WPF
                    "),

                NavigationSection.Contact => Task.FromResult("Email: moritz.nicola.kreis@gmail.com"),

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