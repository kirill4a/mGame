using System;
using System.Collections.Generic;
using System.Linq;
using Mgame.Core.Domain.Base;
using Mgame.Core.Domain.Entities;
using Mgame.Core.Domain.Entities.Team;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.Exceptions;
using Mgame.Core.Domain.ValueObjects;

namespace Mgame.Core.Domain.Aggregates.Room;

public abstract class BaseRoom : AggregateRootBase<Guid>
{
    const int _maxPlayersCount = 4;

    GameSession _session;
    readonly List<BaseTeam> _teams = new List<BaseTeam>();
    readonly List<Viewer> _viewers = new List<Viewer>();

    protected BaseRoom(Guid id, DateTimeOffset createdOn, RoomMetadata metadata)
        : base(id, createdOn)
    {
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    protected abstract TeamType TeamType { get; }

    public RoomMetadata Metadata { get; }

    public IReadOnlyCollection<BaseTeam> Teams => _teams.AsReadOnly();

    public IReadOnlyCollection<Viewer> Viewers => _viewers.AsReadOnly();

    public BaseTeam AddTeam()
    {
        var newTeam = BaseTeam.Factory.NewTeam(TeamType);
        if (GetTotalRoomCapacity() + newTeam.Capacity > _maxPlayersCount)
            throw new RoomCapacityExceededException(this);
        _teams.Add(newTeam);
        return newTeam;
    }

    public void AddViewer(Viewer value)
    {
        if (_viewers.Contains(value)) throw new MemberAlreadyInRoomException(value, this);
        _viewers.Add(value);
    }

    public void StartGame(DateTimeOffset expiryDate)
    {
        if (_session?.IsActive ?? false)
            throw new InvalidOperationException($"Room '{Id}' already has active game session");
        _session = new GameSession(0, DateTimeOffset.Now, expiryDate);
        _session.Open();
    }

    public void StopGame()
    {
        if (_session == null) throw new InvalidOperationException("Room '{Id}' has no active game session");
        _session.Close();
    }

    int GetTotalRoomCapacity() => Teams.Sum(t => t.Capacity);
}
