using System;

namespace Mgame.Core.Domain.Exceptions;

public class LatitudeOutOfRangeException : Exception
{
    public LatitudeOutOfRangeException(double value)
        : base($"Value '{value}' is not valid for latitude. Should be between -90 and 90 inclusive")
    {
    }
}