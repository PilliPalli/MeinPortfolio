using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeinPortfolio.Models;

namespace MeinPortfolio.Services
{
    public class CommandService
    {
        private readonly Dictionary<string, ICommand> _commands = new();
        private readonly List<string> _commandHistory = new();
        private int _historyIndex = -1;

        public void RegisterCommand(ICommand command)
        {
            _commands[command.Name.ToLower()] = command;
        }

        public void RegisterCommands(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                RegisterCommand(command);
            }
        }

        public async Task<CommandResult> ExecuteCommandAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return CommandResult.FromText(string.Empty);

            // Add to history
            _commandHistory.Add(input);
            _historyIndex = _commandHistory.Count;

            // Parse command and arguments
            var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandName = parts[0].ToLower();
            var args = parts.Skip(1).ToArray();

            // Execute command if it exists
            if (_commands.TryGetValue(commandName, out var command))
            {
                try
                {
                    var result = await command.ExecuteAsync(args);
                    return CommandResult.FromText(result);
                }
                catch (Exception ex)
                {
                    return CommandResult.FromText($"Error executing command: {ex.Message}");
                }
            }

            return CommandResult.FromText($"Command not found: {commandName}. Try 'help' to see available commands.");
        }

        public string GetPreviousCommand()
        {
            if (_commandHistory.Count == 0 || _historyIndex <= 0)
                return string.Empty;

            _historyIndex = Math.Max(0, _historyIndex - 1);
            return _commandHistory[_historyIndex];
        }

        public string GetNextCommand()
        {
            if (_commandHistory.Count == 0 || _historyIndex >= _commandHistory.Count)
                return string.Empty;

            _historyIndex++;
            return _historyIndex < _commandHistory.Count ? _commandHistory[_historyIndex] : string.Empty;
        }

        public IEnumerable<string> GetCommandNames()
        {
            return _commands.Keys.OrderBy(k => k);
        }

        public ICommand GetCommand(string name)
        {
            return _commands.TryGetValue(name.ToLower(), out var command) ? command : null;
        }
    }
}
