using ThreeInRow.EventHandlers;
using ThreeInRow.EventHandlers.Bonuses;
using ThreeInRow.Matrix.MatrixElements;
using ThreeInRow.Parameters;

namespace ThreeInRow.Matrix;

public class MatrixManipulator
{
    private static MatrixManipulator? _matrixManipulator;
    private readonly StatisticsCounter _statistics;
    private readonly Matrix _matrix;
    private readonly Iterator _iterator;

    private readonly Dictionary<Type, int> _bonuses = new()
    {
        { typeof(LaneRemover), 0 },
        { typeof(TypeRemover), 0 }
    };

    private MatrixManipulator()
    {
        _matrix = Matrix.Instance;
        _iterator = new Iterator(_matrix);
        _statistics = StatisticsCounter.Instance;
    }

    public static MatrixManipulator Instance => _matrixManipulator ??= new MatrixManipulator();

    public void SwitchPlaces(MoveOption move)
    {
        _statistics.AccountStep(move);
        var fromElement = _matrix.GetByCoordinates(move.FromCoordinate);
        var toElement = _matrix.GetByCoordinates(move.ToCoordinate);

        _matrix.SetByCoordinates(move.ToCoordinate, fromElement);
        _matrix.SetByCoordinates(move.FromCoordinate, toElement);
        ProcessField();
    }

    public void ActivateBonus(Bonus bonus, Coordinate? coordinate, MoveOption? move)
    {
        switch (bonus)
        {
            case TypeRemover typeRemover:
                RemoveByType(_matrix.GetByCoordinates(coordinate ??
                                                      throw new ArgumentException(
                                                          "Coordinate can't be null for RemoveByType bonus")));
                _statistics.AccountBonusUse(bonus, coordinate);
                break;
            case LaneRemover laneRemover:
                RemoveLane(move ?? throw new ArgumentException("Move can't be null for RemoveLane bonus"));
                _statistics.AccountBonusUse(bonus, move.FromCoordinate);
                break;
        }

        ProcessField();
    }

    private void ProcessField()
    {
        var result = _iterator.ProcessMatches();
        _statistics.AccountCombinations(result.AllMatches);

        if (result.TotalElementsRemoved > 10)
        {
            AddBonus(typeof(TypeRemover));
        }

        if (result.AllMatches.Find(row => row.Count > 5) != null)
        {
            AddBonus(typeof(LaneRemover));
        }
    }

    public void AddBonus(Type bonus)
    {
        ++_bonuses[bonus];
    }

    private void RemoveLane(MoveOption move)
    {
        if (move.FromCoordinate.ColIndex == move.ToCoordinate.ColIndex)
        {
            int columnIndex = move.FromCoordinate.ColIndex;
            for (var rowIndex = 0; rowIndex < 8; ++rowIndex)
            {
                var coordinate = new Coordinate(rowIndex, columnIndex);
                _matrix.SetByCoordinates(coordinate, new EmptyElement());
            }
        }

        if (move.FromCoordinate.RowIndex == move.ToCoordinate.RowIndex)
        {
            int rowIndex = move.ToCoordinate.RowIndex;
            for (var columnIndex = 0; columnIndex < 8; ++columnIndex)
            {
                var coordinate = new Coordinate(rowIndex, columnIndex);
                _matrix.SetByCoordinates(coordinate, new EmptyElement());
            }
        }
    }

    private void RemoveByType(MatrixElement elementOfType)
    {
        for (int i = 0; i < 8; i++)
        {
            Console.Write(i + 1 + " ");
            for (int j = 0; j < 8; j++)
            {
                var coordinate = new Coordinate(i, j);
                if (_matrix.GetByCoordinates(coordinate).Equals(elementOfType))
                    _matrix.SetByCoordinates(coordinate, new EmptyElement());
            }
        }
    }

    public MatrixElement[][] GetMatrixField()
    {
        return _matrix.GetField();
    }

    public Dictionary<Bonus, int> GetBonuses()
    {
        return new Dictionary<Bonus, int>
        {
            { new TypeRemover(), _bonuses[typeof(TypeRemover)] },
            { new LaneRemover(), _bonuses[typeof(LaneRemover)] }
        };
    }
}