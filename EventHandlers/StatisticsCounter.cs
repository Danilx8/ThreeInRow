using ThreeInRow.Matrix;

namespace ThreeInRow.EventHandlers;

public class StatisticsCounter
{
    private static StatisticsCounter? _statistics;
    
    private StatisticsCounter()
    {
        throw new NotImplementedException();
    }

    public StatisticsCounter Instance => _statistics ??= new StatisticsCounter();
    
    public void AccountCombination()
    {
        throw new NotImplementedException();
    }

    public void AccountStep(GameStep step)
    {
        throw new NotImplementedException();
    }

    public int CountSCore()
    {
        throw new NotImplementedException();
    }

    public GameStep[] ListSteps()
    {
        throw new NotImplementedException();
    }
}