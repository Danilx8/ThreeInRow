using ThreeInRow.Matrix.MatrixElements;

namespace ThreeInRow.Matrix;

public class Matrix
{
    private static Matrix? _matrix;
    private MatrixElement[][] _field;
    
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

    public void SetCoordinates()
    {
        throw new NotImplementedException();
    }
    
    private MatrixElement GenerateNew()
    {
        throw new NotImplementedException();
    }

    public void Iterate()
    {
        throw new NotImplementedException();
    }

    public MatrixElement[][] GetMatrix()
    {
        throw new NotImplementedException();
    }
}