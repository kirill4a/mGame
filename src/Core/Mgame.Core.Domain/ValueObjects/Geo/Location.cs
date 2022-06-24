using System.Collections.Generic;
using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.ValueObjects.Geo;

/// <summary>
/// Point location on the Earth
/// </summary>
public class Location : ValueObject
{
    public Location(Latitude latitude, Longitude longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public Latitude Latitude { get; }
    public Longitude Longitude { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}
