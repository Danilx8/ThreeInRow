namespace ThreeInRow.CLI.Options;

public class MoveOption
{
    public Coordinate FromCoordinate { get; }
    public Coordinate ToCoordinate { get; }

    public MoveOption(string move)
    {
        if (string.IsNullOrEmpty(move))
            throw new ArgumentException("Move cannot be empty.");

        var parts = move.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            throw new ArgumentException("Move must contain two coordinates separated by a comma.");

        var first = parts[0].Split(':').Select(int.Parse).ToArray();
        var second = parts[1].Split(':').Select(int.Parse).ToArray();
        if (first.Length != 2 || second.Length != 2)
            throw new ArgumentException("Coordinates must be in format 'row:col row:col'.");

        int row1 = first[0], col1 = first[1], row2 = second[0], col2 = second[1];
        if (row1 < 1 || row1 > 8 || col1 < 1 || col1 > 8 || row2 < 1 || row2 > 8 || col2 < 1 || col2 > 8)
            throw new ArgumentException("Coordinates must be between 1 and 8.");

        bool isAdjacent = (Math.Abs(row1 - row2) == 1 && col1 == col2) || (Math.Abs(col1 - col2) == 1 && row1 == row2);
        if (!isAdjacent)
            throw new ArgumentException("Coordinates must be adjacent.");

        FromCoordinate = new Coordinate(row1 - 1, col1 - 1);
        ToCoordinate = new Coordinate(row2 - 1, col2 - 1);
    }
}