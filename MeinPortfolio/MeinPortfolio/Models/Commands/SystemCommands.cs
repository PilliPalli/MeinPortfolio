using System.Threading.Tasks;

namespace MeinPortfolio.Models.Commands
{
    public class ClearCommand : BaseCommand
    {
        public override string Name => "clear";
        public override string Description => "Clear the terminal screen";
        public override string Usage => "clear";

        public override Task<string> ExecuteAsync(string[] args)
        {
            return Task.FromResult("clear");
        }

    }

    public class EchoCommand : BaseCommand
    {
        public override string Name => "echo";
        public override string Description => "Display a line of text";
        public override string Usage => "echo [text]";

        public override Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                return Task.FromResult(string.Empty);
            }

            return Task.FromResult(string.Join(" ", args));
        }
    }

    public class DateCommand : BaseCommand
    {
        public override string Name => "date";
        public override string Description => "Display the current date and time";
        public override string Usage => "date";

        public override Task<string> ExecuteAsync(string[] args)
        {
            return Task.FromResult(System.DateTime.Now.ToString("F"));
        }
    }

    public class WhoamiCommand : BaseCommand
    {
        public override string Name => "whoami";
        public override string Description => "Display information about the portfolio owner";
        public override string Usage => "whoami";

        public override Task<string> ExecuteAsync(string[] args)
        {
            return Task.FromResult("Name: Moritz Nicola Kreis\n");
        }
    }
}
