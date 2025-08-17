namespace ThreeInRow.EventHandlers.Bonuses;

public abstract class Bonus
{
    public override bool Equals(object? obj)
    {
        return GetType() == obj?.GetType();
    }
}