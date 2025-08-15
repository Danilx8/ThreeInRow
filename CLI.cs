namespace ThreeInRow;

public class Cli
{
    private static Cli? _cli;

    private Cli()
    {
    }

    public static Cli Instance => _cli ??= new Cli();

    public void MakeAction()
    {
        throw new NotImplementedException();
    }

    public void DrawMatrix()
    {
        throw new NotImplementedException();
    }

    public void SeeStatistics()
    {
        throw new NotImplementedException();
    }
}