namespace ThreeInRow.Matrix.MatrixElements;

public abstract class MatrixElement
{
    public abstract string GetName();

    public bool IsEmpty()
    {
        return GetType() == typeof(EmptyElement);
    }

    public override bool Equals(object? obj)
    {
        return GetType() == obj?.GetType() && !IsEmpty();
    }
}