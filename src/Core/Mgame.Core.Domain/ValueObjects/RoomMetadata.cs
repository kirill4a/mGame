using System;
using System.Collections.Generic;
using Mgame.Common.Exceptions;
using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.ValueObjects;

/// <summary>
/// Metadata associated with the room
/// </summary>
public class RoomMetadata : ValueObject
{
    public RoomMetadata(string owner, string name, RoomAddress address)
    {
        if (string.IsNullOrWhiteSpace(owner)) throw new BlankStringArgumentException(nameof(owner));
        if (string.IsNullOrWhiteSpace(name)) throw new BlankStringArgumentException(nameof(name));

        Owner = owner;
        Name = name;
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    //TODO: should be user account
    public string Owner { get; }

    public string Name { get; }

    public RoomAddress Address { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Owner;
        yield return Name;
        yield return Address;
    }
}
