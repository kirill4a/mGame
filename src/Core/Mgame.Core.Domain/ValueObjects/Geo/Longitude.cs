using Mgame.Core.Domain.Exceptions;

namespace Mgame.Core.Domain.ValueObjects.Geo;

/// <summary>
/// Geographical longitude
/// </summary>
public sealed class Longitude : GeoCoordinate
{
    /// <summary>
    /// Creates a new instance of <see cref="Longitude"> 
    /// </summary>
    /// <param name="value">Value of longitude in degrees. Should be between -180° and 180° inclusive</param>
    public Longitude(double value) : base(value) { }

    protected override void Validate(double value)
    {
        if (value < -180 || value > 180)
            throw new LongitudeOutOfRangeException(value);
    }
}