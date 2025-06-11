namespace MeinPortfolio.Models
{
    public class CommandResult
    {
        public string Output { get; set; }
        public bool IsHtml { get; set; }
        public bool ShouldClear { get; set; }

        public CommandResult(string output, bool isHtml = false,  bool shouldClear = false)
        {
            Output = output;
            IsHtml = isHtml;
         
            ShouldClear = shouldClear;
        }

        public static CommandResult FromText(string text)
        {
            return new CommandResult(text);
        }
        
    }
}
