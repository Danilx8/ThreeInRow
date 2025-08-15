using ThreeInRow.CLI.Options;
using ThreeInRow.Matrix.MatrixElements;

namespace ThreeInRow.Matrix;

public class Matrix
{
    private static Matrix? _matrix;
    private MatrixElement[][] _field;
    private static readonly Random Random = new();

    private Matrix()
    {
        _field = new MatrixElement[8][];
        for (int i = 0; i < 8; i++)
        {
            _field[i] = new MatrixElement[8];
            for (int j = 0; j < 8; j++)
            {
                _field[i][j] = GenerateNew();
            }
        }
    }

    public static Matrix Instance => _matrix ??= new Matrix();

    public MatrixElement GetByCoordinates(Coordinate coordinate) => _field[coordinate.RowIndex][coordinate.ColIndex];

    public void SetByCoordinates(Coordinate coordinate, MatrixElement element)
    {
        _field[coordinate.RowIndex][coordinate.ColIndex] = element;
    }

    private MatrixElement GenerateNew()
    {
        int type = Random.Next(5);
        return type switch
        {
            0 => new CandyElement(),
            1 => new PieElement(),
            2 => new WaffleElement(),
            3 => new BubbleElement(),
            4 => new OrangeElement(),
            _ => throw new InvalidOperationException("Unexpected random value")
        };
    }

    public MatrixElement[][] GetField()
    {
        return _field;
    }
}