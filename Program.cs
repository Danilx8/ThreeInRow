using CommandLine;
using ThreeInRow;
using ThreeInRow.EventHandlers;
using ThreeInRow.Parameters;

var cli = Cli.Instance;
cli.DrawMatrix();

while (true)
{
    Console.WriteLine("Enter command (e.g., --play 1:1,1:2, --stats, --exit):");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) continue;

    try
    {
        var arguments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Parser.Default.ParseArguments<Options>(arguments)
            .WithParsed(opts =>
            {
                try
                {
                    if (opts.Move != null)
                    {
                        cli.MakeMove(opts.Move);
                        cli.DrawMatrix();
                    }
                    else if (opts.LaneBonus != null)
                    {
                        cli.ApplyBonus(new LaneRemover(), move: opts.LaneBonus);
                    }
                    else if (opts.TypeBonus != null)
                    {
                        cli.ApplyBonus(new TypeRemover(), coordinate: opts.TypeBonus);
                    }
                    else if (opts.Stats)
                    {
                        cli.SeeStatistics();
                    }
                    else if (opts.Exit)
                    {
                        Environment.Exit(0);
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Game error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    Console.WriteLine("Game continues...");
                }
            })
            .WithNotParsed(errs =>
            {
                Console.WriteLine($"Command line errors: {errs}");
                Console.WriteLine("Examples:");
                Console.WriteLine("  --play 1:1,1:2");
                Console.WriteLine("  --stats");
                Console.WriteLine("  --exit");
            });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Command parsing error: {ex.Message}");
        Console.WriteLine("Please try again with a valid command.");
    }
}

class Options
{
    private MoveOption? _move;
    private MoveOption? _laneBonusCoordinates;
    private Coordinate? _typeBonusCoordinate;

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

    [Option('l', "lane", HelpText = "Apply lane bonus")]
    public string? LaneBonusString { get; set; }

    public MoveOption? LaneBonus
    {
        get
        {
            if (_laneBonusCoordinates == null && !string.IsNullOrEmpty(LaneBonusString))
                _laneBonusCoordinates = new MoveOption(LaneBonusString);
            return _laneBonusCoordinates;
        }
    }

    [Option('t', "type", HelpText = "Apply type bonus")]
    public string? TypeBonusString { get; set; }

    public Coordinate? TypeBonus
    {
        get
        {
            if (_typeBonusCoordinate == null && !string.IsNullOrEmpty(TypeBonusString))
                _typeBonusCoordinate = new Coordinate(TypeBonusString);
            return _typeBonusCoordinate;
        }
    }

    [Option('e', "exit", HelpText = "Exit game")]
    public bool Exit { get; set; }
}