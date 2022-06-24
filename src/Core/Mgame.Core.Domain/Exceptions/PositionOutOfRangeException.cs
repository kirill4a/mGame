using System;

namespace Mgame.Core.Domain.Exceptions;

public class PositionOutOfRangeException : Exception
{
    public PositionOutOfRangeException(float row, float column)
        : base($"Position row:{row} column:{column} is out of the range")
    {
    }
}