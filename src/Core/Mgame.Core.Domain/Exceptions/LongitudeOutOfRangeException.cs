using System;

namespace Mgame.Core.Domain.Exceptions;

public class LongitudeOutOfRangeException : Exception
{
    public LongitudeOutOfRangeException(double value)
       : base($"Value '{value}' is not valid for longitude. Should be between -180 and 180 inclusive")
    {
    }
}