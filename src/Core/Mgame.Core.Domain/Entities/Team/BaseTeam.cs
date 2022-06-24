using System;
using System.Collections.Generic;
using Mgame.Core.Domain.Base;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.Exceptions;

namespace Mgame.Core.Domain.Entities.Team;

/// <summary>
/// Group of players as single team
/// </summary>
public abstract class BaseTeam : AggregateRootBase<Guid>
{
    readonly TeamType _teamType;
    readonly List<Player> _players;

    protected BaseTeam(Guid id, DateTimeOffset createdOn, TeamType teamType) : base(id, createdOn)
    {
        _teamType = teamType;
        _players = new List<Player>(_teamType.MembersCount);
    }

    public string TeamCode => _teamType.Name;

    public int Capacity => _teamType.MembersCount;

    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    public void AddPlayer(Player value)
    {
        if (_players.Contains(value)) throw new PlayerAlreadyInTeamException(value, this);
        if (_players.Count >= Capacity) throw new TeamCapacityExceededException(this);
        _players.Add(value);
    }

    /// <summary>
    /// Factory for creating teams
    /// </summary>
    public static class Factory
    {
        static readonly IDictionary<TeamType, Func<BaseTeam>> _creators = new Dictionary<TeamType, Func<BaseTeam>>
        {
            { TeamType.Single, () => new TeamSingle(Guid.NewGuid(), DateTimeOffset.Now) },
            { TeamType.Double, () => new TeamDouble(Guid.NewGuid(), DateTimeOffset.Now) }
        };

        /// <summary>
        /// Creates new team based on team type
        /// </summary>
        public static BaseTeam NewTeam(TeamType teamType)
        {
            if (!_creators.TryGetValue(teamType, out var createFunc))
                throw new NotSupportedException($"{nameof(TeamType)} '{teamType.Name}' is not supported");
            return createFunc.Invoke();
        }
    }
}
