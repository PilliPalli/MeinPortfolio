using System.Threading.Tasks;

namespace MeinPortfolio.Models
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string Usage => Name;

        public abstract Task<string> ExecuteAsync(string[] args);

        protected string FormatError(string message)
        {
            return $"Error: {message}";
        }

        protected string FormatUsage()
        {
            return $"Usage: {Usage}";
        }
    }
}
