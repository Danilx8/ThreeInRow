using System.Text;
using ThreeInRow.EventHandlers.Bonuses;
using ThreeInRow.Parameters;

namespace ThreeInRow.EventHandlers.GameStep;

public class UseBonus(int index, Coordinate coordinate, Bonus bonus)
{
    private int _index = index;
    private Bonus _bonus = bonus;

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"{_index}. Used bonus {_bonus.ToString()} at {coordinate}");
        
        return builder.ToString();
    }
}