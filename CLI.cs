using ThreeInRow.EventHandlers;
using ThreeInRow.EventHandlers.Bonuses;
using ThreeInRow.Matrix;
using ThreeInRow.Parameters;

namespace ThreeInRow;

public class Cli
{
    private static Cli? _cli;
    private readonly MatrixManipulator _matrix;
    private readonly StatisticsCounter _statistics;

    private Cli()
    {
        _matrix = MatrixManipulator.Instance;
        _statistics = StatisticsCounter.Instance;
    }

    public static Cli Instance => _cli ??= new Cli();

    public void MakeMove(MoveOption move)
    {
        _matrix.SwitchPlaces(move);
    }

    public void ApplyBonus(Bonus bonus, MoveOption? move = null, Coordinate? coordinate = null)
    {
        var bonuses = _matrix.GetBonuses();
        if (bonuses.Contains(bonus)) _matrix.ActivateBonus(bonus, move: move, coordinate: coordinate);
        else Console.WriteLine("You don't have this bonus");
    }

    public void DrawMatrix()
    {
        var field = _matrix.GetMatrixField();

        for (int i = 0; i < 9; i++)
        {
            Console.Write(i == 0 ? "@" : i);
            Console.Write(" ");
        }
        
        Console.WriteLine();

        for (int i = 0; i < 8; i++)
        {
            Console.Write(i + 1 + " ");
            for (int j = 0; j < 8; j++)
            {
                Console.Write($"{field[i][j].GetName()} ");
            }

            Console.WriteLine();
        }
    }

    public void SeeStatistics()
    {
        Console.WriteLine(_statistics.CountScore());
    }
}