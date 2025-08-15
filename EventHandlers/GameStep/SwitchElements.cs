using System.Text;
using ThreeInRow.Parameters;

namespace ThreeInRow.EventHandlers.GameStep;

public class SwitchElements(int index, MoveOption moveOption) : GameStep
{
    private int _index = index;
    private MoveOption _moveOption = moveOption;
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"{_index}. Switched item from {moveOption.FromCoordinate} to {moveOption.ToCoordinate}");
        return builder.ToString();
    }
}