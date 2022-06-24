using System;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.ValueObjects;

namespace Mgame.Core.Domain.Aggregates.Room;

/// <summary>
/// Room for player-vs-player game of double teams
/// </summary>
public sealed class RoomDouble : BaseRoom
{
    public RoomDouble(Guid id, DateTimeOffset createdOn, RoomMetadata metadata) 
        : base(id, createdOn, metadata)
    {
    }

    protected override TeamType TeamType => TeamType.Double;
}
