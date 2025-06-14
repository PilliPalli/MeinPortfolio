using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MeinPortfolio.Models.Commands
{
    public class CvCommand : BaseCommand
    {
        private readonly IJSRuntime _jsRuntime;

        public override string Name => "cv";
        public override string Description => "Download CV as PDF";
        public override string Usage => "cv";

        public CvCommand(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<string> ExecuteAsync(string[] args)
        {
            await _jsRuntime.InvokeVoidAsync("downloadCv");
            return "Downloading CV as PDF...";
        }
    }
}