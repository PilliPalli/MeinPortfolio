using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MeinPortfolio.Models.Commands
{
    public class ResumeCommand : BaseCommand
    {
        private readonly IJSRuntime _jsRuntime;

        public override string Name => "resume";
        public override string Description => "Download resume as PDF";
        public override string Usage => "resume";

        public ResumeCommand(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<string> ExecuteAsync(string[] args)
        {
            await _jsRuntime.InvokeVoidAsync("downloadResume");
            return "Downloading resume as PDF...";
        }
    }
}