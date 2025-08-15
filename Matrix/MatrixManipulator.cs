using ThreeInRow.Matrix.MatrixElements;
using ThreeInRow.Parameters;

namespace ThreeInRow.Matrix;

public class MatrixManipulator
{
    private static MatrixManipulator? _matrixManipulator;
    private readonly Matrix _matrix;
    private readonly Iterator _iterator;
    
    private MatrixManipulator()
    {
        _matrix = Matrix.Instance;
        _iterator = new Iterator(_matrix);
    }

    public static MatrixManipulator Instance => _matrixManipulator ??= new MatrixManipulator();
    
    public void SwitchPlaces(MoveOption move)
    {
        var fromElement = _matrix.GetByCoordinates(move.FromCoordinate);
        var toElement = _matrix.GetByCoordinates(move.ToCoordinate);
        
        _matrix.SetByCoordinates(move.ToCoordinate, fromElement);
        _matrix.SetByCoordinates(move.FromCoordinate, toElement);
        var result = _iterator.ProcessMatches();
        Console.WriteLine(result.AllMatches);
        Console.WriteLine(result.CascadeCount);
        Console.WriteLine(result.TotalElementsRemoved);
    }

    public void UseBonus()
    {
        throw new NotImplementedException();
    }

    public void ActivateBonus()
    {
        throw new NotImplementedException();
    }

    public void AddBonus()
    {
        throw new NotImplementedException();
    }

    public MatrixElement[][] GetMatrixField()
    {
        return _matrix.GetField();
    }

    public void GetBonuses()
    {
        throw new NotImplementedException();
    }
}