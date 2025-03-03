using System.Threading.Tasks;

namespace MeinPortfolio.Models.Commands
{
    

    public class HireMeCommand : BaseCommand
    {
        public override string Name => "sudo hire_me";
        public override string Description => "A special command with a surprise";
        public override string Usage => "sudo hire_me";

        public override Task<string> ExecuteAsync(string[] args)
        {
            return Task.FromResult("ACCESS GRANTED. Welcome to the team! 🎉");
        }
    }

    public class AsciiArtCommand : BaseCommand
    {
        public override string Name => "ascii";
        public override string Description => "Display ASCII art";
        public override string Usage => "ascii [art]";

        public override Task<string> ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                return Task.FromResult("Available ASCII art: computer, rocket, cat");
            }

            var art = args[0].ToLower();
            
            switch (art)
            {
                case "computer":
                    return Task.FromResult(@"
     .---.
    /     \
    |--(o)--|
   `---------'
    (_______)
    |       |
    |       |
    `-------'
");
                
                case "rocket":
                    return Task.FromResult(@"
      /\
     /  \
    |    |
    |    |
    |    |
   /|    |\
  / |    | \
 /__|____|__\
     /\
    /  \
    ----
");
                
                case "cat":
                    return Task.FromResult(@"
 /\_/\
( o.o )
 > ^ <
");
                
                default:
                    return Task.FromResult($"ASCII art '{art}' not found. Available options: computer, rocket, cat");
            }
        }
    }
}
