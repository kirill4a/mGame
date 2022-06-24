using Mgame.Core.Domain.Exceptions;

namespace Mgame.Core.Domain.ValueObjects.Geo;

/// <summary>
/// Geographical latitude
/// </summary>
public sealed class Latitude : GeoCoordinate
{
    /// <summary>
    /// Creates a new instance of <see cref="Latitude"> 
    /// </summary>
    /// <param name="value">Value of latitude in degrees. Should be between -90° and 90° inclusive</param>
    public Latitude(double value) : base(value) { }

    protected override void Validate(double value)
    {
        if (value < -90 || value > 90)
            throw new LatitudeOutOfRangeException(value);
    }
}