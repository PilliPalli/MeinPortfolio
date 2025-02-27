namespace MeinPortfolio.Models
{
    public class CommandResult
    {
        public string Output { get; set; }
        public bool IsHtml { get; set; }
        public bool IsAnimation { get; set; }
        public bool ShouldClear { get; set; }

        public CommandResult(string output, bool isHtml = false, bool isAnimation = false, bool shouldClear = false)
        {
            Output = output;
            IsHtml = isHtml;
            IsAnimation = isAnimation;
            ShouldClear = shouldClear;
        }

        public static CommandResult FromText(string text)
        {
            return new CommandResult(text);
        }

        public static CommandResult FromHtml(string html)
        {
            return new CommandResult(html, isHtml: true);
        }

        public static CommandResult FromAnimation(string animation)
        {
            return new CommandResult(animation, isAnimation: true);
        }

        public static CommandResult WithClear(string output)
        {
            return new CommandResult(output, shouldClear: true);
        }
    }
}
