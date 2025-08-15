namespace ThreeInRow.Parameters;

public class MatchResult
{
    public int TotalElementsRemoved { get; set; }
    public int CascadeCount { get; set; }
    public List<List<Coordinate>> AllMatches { get; set; } = new();
}
