using CommandLine;
using ThreeInRow.CLI;
using ThreeInRow.CLI.Options;

var cli = Cli.Instance;
cli.DrawMatrix();

while (true)
{
    Console.WriteLine("Enter command (e.g., --play 1:1,1:2, --stats, --exit):");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) continue;

    var arguments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    Parser.Default.ParseArguments<Options>(arguments)
        .WithParsed(opts =>
        {
            if (opts.Move != null)
            {
                cli.MakeMove(opts.Move);
                cli.DrawMatrix();
            }
            else if (opts.Stats)
            {
                cli.SeeStatistics();
            }
            else if (opts.Exit)
            {
                // cli.SeeStatistics();
                Environment.Exit(0);
            }
        })
        .WithNotParsed(errs => Console.WriteLine("Invalid command. Use --help for usage."));
}

class Options
{
    private MoveOption? _move;

    [Option('p', "play", HelpText = "Make a move with two coordinates (e.g., '1:1,1:2')")]
    public string? MoveString { get; set; }

    public MoveOption? Move
    {
        get
        {
            if (_move == null && !string.IsNullOrEmpty(MoveString))
                _move = new MoveOption(MoveString);
            return _move;
        }
    }

    [Option('s', "stats", HelpText = "Show game statistics")]
    public bool Stats { get; set; }

    [Option('e', "exit", HelpText = "Exit game")]
    public bool Exit { get; set; }
}