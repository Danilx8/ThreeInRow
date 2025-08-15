namespace ThreeInRow.Parameters;

public class Coordinate
{
    public int RowIndex { get; }
    public int ColIndex { get; }

    public Coordinate(int row, int col)
    {
        RowIndex = row;
        ColIndex = col;
    }

    public Coordinate(string coordinateString)
    {
        if (string.IsNullOrEmpty(coordinateString)) throw new ArgumentException("Coordinate cannot be empty.");
        var coordinate = coordinateString.Split(':').Select(int.Parse).ToArray();

        if (coordinate.Length != 2) throw new ArgumentException("Coordinate must be in format 'row:col'.");
        
        int row = coordinate[0];
        int col = coordinate[1];
        if (row < 1 || row > 8 || col < 1 || col > 8)
            throw new ArgumentException("Coordinates must be between 1 and 8.");
        RowIndex = row;
        ColIndex = col;
    }

    public override string ToString()
    {
        return $"{RowIndex}:{ColIndex}";
    }
}