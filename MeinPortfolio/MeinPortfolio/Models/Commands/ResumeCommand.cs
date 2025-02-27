using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MeinPortfolio.Models.Commands
{
    public class ResumeCommand : BaseCommand
    {
        private readonly IJSRuntime _jsRuntime;

        public override string Name => "resume";
        public override string Description => "View or download resume";
        public override string Usage => "resume [download]";

        public ResumeCommand(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower() == "download")
            {
                await _jsRuntime.InvokeVoidAsync("downloadResume");
                return "Downloading resume as PDF...";
            }
            else
            {
                return @"
RESUME
======

PERSONAL INFORMATION
-------------------
Name: Dein Name
Email: deine-email@example.com
LinkedIn: linkedin.com/in/deinname
GitHub: github.com/deinname

SKILLS
------
- C# (5/5)
- Blazor (4/5)
- SQL & Dapper (4/5)
- ASP.NET Core (4/5)
- Git & CI/CD (4/5)

EXPERIENCE
----------
Software Developer | Company Name | 2020 - Present
- Developed and maintained web applications using C# and Blazor
- Implemented database solutions using SQL and Dapper
- Collaborated with team members using Git and CI/CD pipelines

Junior Developer | Previous Company | 2018 - 2020
- Assisted in developing desktop applications using WPF
- Learned and applied C# programming principles
- Participated in code reviews and team meetings

EDUCATION
---------
Bachelor of Computer Science | University Name | 2014 - 2018

PROJECTS
--------
- Schwimmbad-Mitgliedschaftsverwaltung (C#, SQLite, WPF)
- Garbage-Collection-Tool mit WPF
- Kundenverwaltung für Oldtimer-Vermietung (Blazor Server)

Use 'resume download' to download as PDF.
";
            }
        }
    }
}
