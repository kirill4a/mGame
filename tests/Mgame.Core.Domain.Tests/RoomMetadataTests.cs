using System;
using System.Net;
using System.Net.NetworkInformation;
using FluentAssertions;
using Mgame.Common.Exceptions;
using Mgame.Core.Domain.ValueObjects;
using Mgame.Core.Domain.ValueObjects.Geo;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class RoomMetadataTests
{
    [Fact]
    public void TestCreateRoomMetadata()
    {
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);

        // owner violation
        Action wrongOwnerAction = () => _ = new RoomMetadata(null, "room one", address);
        wrongOwnerAction.Should().ThrowExactly<BlankStringArgumentException>().WithMessage("*owner*");

        // name violation
        Action wrongNameAction = () => _ = new RoomMetadata("B-owner", " ", address);
        wrongNameAction.Should().ThrowExactly<BlankStringArgumentException>().WithMessage("*name*");

        // address violation
        Action wrongAddressAction = () => _ = new RoomMetadata("B-owner", "room one", null);
        wrongAddressAction.Should().ThrowExactly<ArgumentNullException>().WithMessage("*address*");

        var owner = "Test owner";
        var name = "Test name";
        var roomMetadata = new RoomMetadata(owner, name, address);
        roomMetadata.Owner.Should().Be(owner);
        roomMetadata.Name.Should().Be(name);
        roomMetadata.Address.Should().Be(address);
    }

    [Fact]
    public void TestRoomMetadataEquality()
    {
        var location = new Location(new Latitude(0d), new Longitude(0d));
        var addressWithLocation = new RoomAddress(location, "Any device", PhysicalAddress.None, IPAddress.None);
        var addressNoLocation = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);

        var owner = "Test owner";
        var name = "Test name";
        var originalMetadata = new RoomMetadata(owner, name, addressWithLocation);
        var sameMetadata = new RoomMetadata(owner, name, addressWithLocation);

        var differOwnerMetadata = new RoomMetadata("Another owner", name, addressWithLocation);
        var differNameMetadata = new RoomMetadata(owner, "Another name", addressWithLocation);
        var differAddressMetadata = new RoomMetadata(owner, name, addressNoLocation);

        // same metadata
        originalMetadata.Should().NotBeSameAs(sameMetadata);
        originalMetadata.Should().Be(sameMetadata);

        // different owners
        originalMetadata.Should().NotBeSameAs(differOwnerMetadata);
        originalMetadata.Should().NotBe(differOwnerMetadata);

        // different names
        originalMetadata.Should().NotBeSameAs(differNameMetadata);
        originalMetadata.Should().NotBe(differNameMetadata);

        // different addresses
        originalMetadata.Should().NotBeSameAs(differAddressMetadata);
        originalMetadata.Should().NotBe(differAddressMetadata);
    }
}
