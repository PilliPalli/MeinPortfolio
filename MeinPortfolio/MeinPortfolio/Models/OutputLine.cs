namespace MeinPortfolio.Models
{
    public class OutputLine
    {
        public string Content { get; set; }
        public bool IsCommand { get; set; }
        public bool IsError { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsHtml { get; set; }

        public OutputLine(string content, bool isCommand = false, bool isError = false, bool isSuccess = false, bool isHtml = false)
        {
            Content = content;
            IsCommand = isCommand;
            IsError = isError;
            IsSuccess = isSuccess;
            IsHtml = isHtml;
        }
    }
}
