using System;
using System.Net;
using System.Net.NetworkInformation;
using FluentAssertions;
using Mgame.Common.Exceptions;
using Mgame.Core.Domain.ValueObjects;
using Mgame.Core.Domain.ValueObjects.Geo;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class RoomAddressTests
{
    [Fact]
    public void TestCreateRoomAddress()
    {
        Action wrongDeviceNameAction = () => _ = new RoomAddress(null, "  ", PhysicalAddress.None, IPAddress.None);
        wrongDeviceNameAction.Should().ThrowExactly<BlankStringArgumentException>().WithMessage("*deviceName*");

        var location = new Location(new Latitude(0d), new Longitude(0d));
        var deviceName = "Nokia Perfect";
        var roomAddress = new RoomAddress(location, deviceName, PhysicalAddress.None, IPAddress.None);
        roomAddress.Location.Should().Be(location);
        roomAddress.DeviceName
                   .Should().NotBeNullOrWhiteSpace()
                   .And.Be(deviceName);
        roomAddress.DeviceMacAddress.Should().Be(PhysicalAddress.None);
        roomAddress.DeviceIpAddress.Should().Be(IPAddress.None);
    }

    [Fact]
    public void TestRoomAddressEquality()
    {
        var location = new Location(new Latitude(0d), new Longitude(0d));
        var deviceName = "Nokia Perfect";
        var originalAddress = new RoomAddress(location, deviceName, PhysicalAddress.None, IPAddress.None);
        var sameAddress = new RoomAddress(location, deviceName, PhysicalAddress.None, IPAddress.None);
        var differAddress = new RoomAddress(location, deviceName, PhysicalAddress.None, IPAddress.Loopback);

        originalAddress.Should().NotBeSameAs(sameAddress);
        originalAddress.Should().Be(sameAddress);

        originalAddress.Should().NotBeSameAs(differAddress);
        originalAddress.Should().NotBe(differAddress);
    }
}
