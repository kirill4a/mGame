using System;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.ValueObjects;

namespace Mgame.Core.Domain.Aggregates.Room;

/// <summary>
/// Room for player-vs-player game of single teams
/// </summary>
public sealed class RoomSingle : BaseRoom
{
    public RoomSingle(Guid id, DateTimeOffset createdOn, RoomMetadata metadata) 
        : base(id, createdOn, metadata)
    {
    }

    protected override TeamType TeamType => TeamType.Single;
}
