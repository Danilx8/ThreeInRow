using ThreeInRow.EventHandlers.Bonuses;
using ThreeInRow.EventHandlers.GameStep;
using ThreeInRow.Parameters;

namespace ThreeInRow.EventHandlers;

public class StatisticsCounter
{
    private static StatisticsCounter? _statistics;
    private readonly string _filePath = "game_stats.txt";
    private int _stepNumber = 0;
    private int _score = 0;

    private StatisticsCounter()
    {
        if (!File.Exists(_filePath))
        {
            using var writer = new StreamWriter(_filePath, false);
            writer.WriteLine("Score: ");
            writer.WriteLine("Moves:");
        }
    }

    public static StatisticsCounter Instance => _statistics ??= new StatisticsCounter();

    // Команды
    public void AccountCombinations(List<CompleteRow> rows)
    {
        foreach (var row in rows)
        {
            switch (row.Count)
            {
                case 3:
                    _score += 10;
                    break;
                case 4:
                    _score += 15;
                    break;
                case 5:
                    _score += 25;
                    break;
                case 6:
                    _score += 40;
                    break;
                case 7:
                    _score += 65;
                    break;
                case 8:
                    _score += 100;
                    break;
            }
        }
    }

    public void AccountStep(MoveOption move)
    {
        ++_stepNumber;
        var step = new SwitchElements(_stepNumber, move);
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine(step);
    }

    public void AccountBonusUse(Bonus bonus, Coordinate coordinate)
    {
        ++_stepNumber;
        var step = new UseBonus(_stepNumber, coordinate, bonus);
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine(step);
    }

    // Запросы 
    public int CountScore(bool store = false)
    {
        if (!store) return _score;

        var lines = File.ReadAllLines(_filePath).ToList();
        lines[0] = "Score : " + _score;
        File.WriteAllLines(_filePath, lines);
        return _score;
    }
}