using System;
using FluentAssertions;
using Mgame.Core.Domain.Exceptions;
using Mgame.Core.Domain.ValueObjects.Geo;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class GeoCoordinatesTests
{
    [Fact]
    public void TestLatitude()
    {
        var wrongLatitude = 91d;
        Action actionLatitude = () => _ = new Latitude(wrongLatitude);
        actionLatitude.Should().ThrowExactly<LatitudeOutOfRangeException>()
                       .WithMessage($"*{wrongLatitude}*Should be between -90 and 90*");

        var goodLatitude = -70.776944;
        var goodLatitudeDms = "-70°47'37\"";
        var latitude = new Latitude(goodLatitude);
        latitude.RawValue.Should().Be(goodLatitude);
        latitude.ToDegreesMinutesSeconds().Should().Be(goodLatitudeDms);
    }
    [Fact]
    public void TestLongitude()
    {
        var wrongLongitude = -181d;
        Action actionLongitude = () => _ = new Longitude(wrongLongitude);
        actionLongitude.Should().ThrowExactly<LongitudeOutOfRangeException>()
                       .WithMessage($"*{wrongLongitude}*Should be between -180 and 180*");

        var goodLongitude = 11.823889;
        var goodLongitudeDms = "11°49'26\"";
        var longitude = new Latitude(goodLongitude);
        longitude.RawValue.Should().Be(goodLongitude);
        longitude.ToDegreesMinutesSeconds().Should().Be(goodLongitudeDms);
    }

    [Fact]
    public void TestLocation()
    {
        var latitude = -70.776944;
        var longitude = 11.823889;

        var location = new Location(new Latitude(latitude), new Longitude(longitude));
        location.Latitude.RawValue.Should().Be(latitude);
        location.Longitude.RawValue.Should().Be(longitude);

        var sameLocation = new Location(new Latitude(latitude), new Longitude(longitude));
        sameLocation.Latitude.RawValue.Should().Be(latitude);
        sameLocation.Longitude.RawValue.Should().Be(longitude);
        sameLocation.Should().NotBeSameAs(location);
        sameLocation.Should().Be(location, "locations should compare by content");

        var differentLocation = new Location(new Latitude(56d), new Longitude(37d));
        differentLocation.Should().NotBeSameAs(location);
        differentLocation.Should().NotBe(location);
    }
}
