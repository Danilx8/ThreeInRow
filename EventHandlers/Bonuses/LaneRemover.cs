using ThreeInRow.EventHandlers.Bonuses;

namespace ThreeInRow.EventHandlers;

public class LaneRemover: Bonus
{
    public override string GetName()
    {
        return "Lane remover";
    }
}