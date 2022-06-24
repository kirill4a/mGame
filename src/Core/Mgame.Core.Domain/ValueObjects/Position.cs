using System.Collections.Generic;
using Mgame.Core.Domain.Base;
using Mgame.Core.Domain.Exceptions;

namespace Mgame.Core.Domain.ValueObjects;

/// <summary>
/// Position of the cell on game board (grid)
/// </summary>
public class Position : ValueObject
{
    //TODO: move to IPositionSettings
    const int _maxRows = 15;
    const int _maxColumns = 15;

    public Position(float row, float column)
    {
        if (!IsInRange(row, 0f, _maxRows - 1) || !IsInRange(column, 0f, _maxColumns - 1))
            throw new PositionOutOfRangeException(row, column);
    }

    /// <summary>
    /// Zero based row coordinate on the grid
    /// </summary>
    public float Row { get; }

    /// <summary>
    /// Zero based column coordinate on the grid
    /// </summary>
    public float Column { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Row;
        yield return Column;
    }

    bool IsInRange(float value, float min, float max) => value >= min && value <= max;       
}
