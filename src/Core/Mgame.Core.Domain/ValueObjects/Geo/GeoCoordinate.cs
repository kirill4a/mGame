using System;
using System.Collections.Generic;
using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.ValueObjects.Geo;

/// <summary>
/// Geographic coordinate of the location on the Earth
/// </summary>
public abstract class GeoCoordinate : ValueObject
{
    protected GeoCoordinate(double value)
    {
        Validate(value);
        RawValue = value;

        CalculateParts();
    }

    protected abstract void Validate(double value);

    public double RawValue { get; }

    public int Degrees { get; private set; }
    public double Minutes { get; private set; }
    public double Seconds { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RawValue;
    }

    public string ToDegreesMinutesSeconds() => $"{Degrees}°{Minutes:00}'{Seconds:00}\"";

    void CalculateParts()
    {
        var fractionDegrees = RawValue % 1;
        Degrees = (int)(RawValue - fractionDegrees);

        Minutes = Math.Round(Math.Abs(fractionDegrees) * 60, 4);

        var fractionMinutes = Minutes % 1;
        Seconds = Math.Round(fractionMinutes * 60, 4);
    }
}
