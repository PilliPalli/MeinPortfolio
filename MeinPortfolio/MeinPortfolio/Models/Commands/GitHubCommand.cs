using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MeinPortfolio.Models.Commands
{
    public class GitHubCommand : BaseCommand
    {
        private readonly HttpClient _httpClient;
        private const string GitHubUsername = "pillipalli";

        public override string Name => "github";
        public override string Description => "Display GitHub repositories and information";
        public override string Usage => "github [repos|profile]";

        public GitHubCommand(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                return "Available GitHub commands: repos, profile";
            }

            var command = args[0].ToLower();
            
            try
            {
                switch (command)
                {
                    case "repos":
                        return await GetRepositoriesAsync();
                    
                    case "profile":
                        return await GetProfileAsync();
                    
                    default:
                        return $"Unknown GitHub command: {command}. Available commands: repos, profile";
                }
            }
            catch (Exception ex)
            {
                return $"Error fetching GitHub data: {ex.Message}";
            }
        }

        private async Task<string> GetRepositoriesAsync()
        {
            try
            {
                var repos = await _httpClient.GetFromJsonAsync<GitHubRepo[]>($"https://api.github.com/users/{GitHubUsername}/repos");
                
                if (repos == null || repos.Length == 0)
                {
                    return "No repositories found.";
                }

                var sb = new StringBuilder();
                sb.AppendLine($"GitHub Repositories for {GitHubUsername}:");
                sb.AppendLine();

                foreach (var repo in repos)
                {
                    sb.AppendLine($"- {repo.Name}");
                    sb.AppendLine($"  Description: {repo.Description ?? "No description"}");
                    sb.AppendLine($"  Language: {repo.Language ?? "Not specified"}");
                    sb.AppendLine($"  Stars: {repo.StargazersCount}");
                    sb.AppendLine($"  URL: {repo.HtmlUrl}");
                    sb.AppendLine();
                }

                return sb.ToString();
            }
            catch
            {
                // Fallback to mock data if API call fails
                return @"
GitHub Repositories for deinname:

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
";
            }
        }

        private async Task<string> GetProfileAsync()
        {
            try
            {
                var profile = await _httpClient.GetFromJsonAsync<GitHubProfile>($"https://api.github.com/users/{GitHubUsername}");
                
                if (profile == null)
                {
                    return "Profile not found.";
                }

                var sb = new StringBuilder();
                sb.AppendLine($"GitHub Profile: {profile.Name ?? profile.Login}");
                sb.AppendLine($"Bio: {profile.Bio ?? "No bio"}");
                sb.AppendLine($"Location: {profile.Location ?? "Not specified"}");
                sb.AppendLine($"Public Repositories: {profile.PublicRepos}");
                sb.AppendLine($"Followers: {profile.Followers}");
                sb.AppendLine($"Following: {profile.Following}");
                sb.AppendLine($"Profile URL: {profile.HtmlUrl}");

                return sb.ToString();
            }
            catch
            {
                // Fallback to mock data if API call fails
                return @"
GitHub Profile: Dein Name
Bio: Software Developer specializing in C# and Blazor
Location: Germany
Public Repositories: 10
Followers: 25
Following: 30
Profile URL: https://github.com/deinname
";
            }
        }

        private class GitHubRepo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public int StargazersCount { get; set; }
            public string HtmlUrl { get; set; }
        }

        private class GitHubProfile
        {
            public string Login { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public string Location { get; set; }
            public int PublicRepos { get; set; }
            public int Followers { get; set; }
            public int Following { get; set; }
            public string HtmlUrl { get; set; }
        }
    }
}
