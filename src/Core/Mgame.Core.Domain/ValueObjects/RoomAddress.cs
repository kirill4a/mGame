using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Mgame.Common.Exceptions;
using Mgame.Core.Domain.Base;
using Mgame.Core.Domain.ValueObjects.Geo;

namespace Mgame.Core.Domain.ValueObjects;

/// <summary>
/// Virtual 'address' of the game room
/// </summary>
public class RoomAddress : ValueObject
{
    public RoomAddress(
                        Location location, 
                        string deviceName, 
                        PhysicalAddress deviceMacAddress, 
                        IPAddress deviceIpAddress) 
    {
        if (string.IsNullOrWhiteSpace(deviceName)) throw new BlankStringArgumentException(nameof(deviceName));
        Location = location;
        DeviceName = deviceName;
        DeviceMacAddress = deviceMacAddress;
        DeviceIpAddress = deviceIpAddress;
    }

    public Location Location { get; }
    public string DeviceName { get; }
    public PhysicalAddress DeviceMacAddress { get; }
    public IPAddress DeviceIpAddress { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Location;
        yield return DeviceName;
        yield return DeviceMacAddress;
        yield return DeviceIpAddress;
    }
}
