using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeinPortfolio.Services;

namespace MeinPortfolio.Models.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly CommandService _commandService;

        public override string Name => "help";
        public override string Description => "Displays help information for available commands";
        public override string Usage => "help [command]";

        public HelpCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length > 0)
            {
                var commandName = args[0].ToLower();
                var command = _commandService.GetCommand(commandName);
                
                if (command != null)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Command: {command.Name}");
                    sb.AppendLine($"Description: {command.Description}");
                    sb.AppendLine($"Usage: {command.Usage}");
                    return Task.FromResult(sb.ToString());
                }
                else
                {
                    return Task.FromResult($"Command '{commandName}' not found. Try 'help' to see all available commands.");
                }
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("Available commands:");
                sb.AppendLine();

                var commands = _commandService.GetCommandNames().ToList();
                var maxLength = commands.Max(c => c.Length);

                foreach (var commandName in commands)
                {
                    var command = _commandService.GetCommand(commandName);
                    sb.AppendLine($"{commandName.PadRight(maxLength + 2)} - {command.Description}");
                }

                sb.AppendLine();
                sb.AppendLine("Type 'help [command]' for more information about a specific command.");
                
                return Task.FromResult(sb.ToString());
            }
        }
    }
}
