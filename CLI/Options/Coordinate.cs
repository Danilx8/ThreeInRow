namespace ThreeInRow.CLI.Options;

public class Coordinate(int row, int col)
{
    public int RowIndex { get; } = row;
    public int ColIndex { get; } = col;
}