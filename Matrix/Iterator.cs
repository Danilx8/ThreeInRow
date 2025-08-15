using ThreeInRow.Matrix.MatrixElements;
using ThreeInRow.Parameters;

namespace ThreeInRow.Matrix;

public class Iterator(Matrix matrix)
{
    public MatchResult ProcessMatches()
    {
        var result = new MatchResult();

        while (true)
        {
            // Find all matches in current state
            var matches = FindAllMatches();

            if (matches.Count == 0)
                break; // No more matches found

            // Remove matched elements
            int removedInThisCascade = RemoveMatches(matches);
            result.TotalElementsRemoved += removedInThisCascade;
            result.AllMatches.Add(matches.SelectMany(m => m).ToList());

            // Apply gravity
            ApplyGravity();

            result.CascadeCount++;

            // Continue to next cascade iteration
        }

        return result;
    }

    private List<List<Coordinate>> FindAllMatches()
    {
        var allMatches = new List<List<Coordinate>>();
        var processedPositions = new HashSet<Coordinate>();

        // Find horizontal matches
        for (int row = 0; row < 8; row++)
        {
            var horizontalMatches = FindMatchesInLine(row, 0, 0, 1, processedPositions);
            allMatches.AddRange(horizontalMatches);
        }

        // Find vertical matches
        for (int col = 0; col < 8; col++)
        {
            var verticalMatches = FindMatchesInLine(0, col, 1, 0, processedPositions);
            allMatches.AddRange(verticalMatches);
        }

        return allMatches;
    }

    private List<List<Coordinate>> FindMatchesInLine(
        int startRow, int startCol, int deltaRow, int deltaCol,
        HashSet<Coordinate> processedPositions)
    {
        var matches = new List<List<Coordinate>>();
        var currentMatch = new List<Coordinate>();
        MatrixElement currentElement = new EmptyElement();

        int row = startRow;
        int col = startCol;

        while (row >= 0 && row < 8 && col >= 0 && col < 8)
        {
            var coordinate = new Coordinate(row, col);

            var element = matrix.GetByCoordinates(coordinate);

            // Skip if position already processed or element is empty
            if (processedPositions.Contains(coordinate) || element.IsEmpty())
            {
                // Finish current match if it exists
                if (currentMatch.Count >= 3)
                {
                    matches.Add([..currentMatch]);
                    foreach (var pos in currentMatch)
                        processedPositions.Add(pos);
                }

                currentMatch.Clear();
                currentElement = new EmptyElement();
            }
            else if (currentElement.IsEmpty() || currentElement.Equals(element))
            {
                // Start new match or continue current match
                if (currentElement.IsEmpty())
                    currentElement = element;

                currentMatch.Add(coordinate);
            }
            else
            {
                // Different element found, finish current match if valid
                if (currentMatch.Count >= 3)
                {
                    matches.Add(new List<Coordinate>(currentMatch));
                    foreach (var pos in currentMatch)
                        processedPositions.Add(pos);
                }

                // Start new match with current element
                currentMatch.Clear();
                currentMatch.Add(coordinate);
                currentElement = element;
            }

            row += deltaRow;
            col += deltaCol;
        }

        // Handle final match
        if (currentMatch.Count >= 3)
        {
            matches.Add(currentMatch);
            foreach (var pos in currentMatch)
                processedPositions.Add(pos);
        }

        return matches;
    }

    private int RemoveMatches(List<List<Coordinate>> matches)
    {
        int totalRemoved = 0;
        var positionsToRemove = new HashSet<Coordinate>();

        // Collect all positions to remove (avoid duplicates)
        foreach (var match in matches)
        {
            foreach (var coordinate in match)
            {
                positionsToRemove.Add(coordinate);
            }
        }

        // Remove elements
        foreach (var coordinate in positionsToRemove)
        {
            matrix.SetByCoordinates(coordinate, new EmptyElement());
            totalRemoved++;
        }

        return totalRemoved;
    }

    private void ApplyGravity()
    {
        for (int col = 0; col < 8; col++)
        {
            // Collect all non-empty elements in this column from bottom to top
            var nonEmptyElements = new List<MatrixElement>();

            for (int row = 8 - 1; row >= 0; row--)
            {
                var coordinate = new Coordinate(row, col);
                if (!matrix.IsEmptyByCoordinates(coordinate))
                {
                    nonEmptyElements.Add(matrix.GetByCoordinates(coordinate));
                }
            }

            // Clear the column
            for (int row = 0; row < 8; row++)
            {
                matrix.SetByCoordinates(new Coordinate(row, col), new EmptyElement());
            }

            // Place non-empty elements at the bottom
            for (int i = 0; i < nonEmptyElements.Count; i++)
            {
                var coordinate = new Coordinate(7 - i, col);
                matrix.SetByCoordinates(coordinate, nonEmptyElements[i]);
            }
        }
    }
}