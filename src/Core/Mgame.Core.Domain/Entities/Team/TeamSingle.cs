using System;
using Mgame.Core.Domain.Enums;

namespace Mgame.Core.Domain.Entities.Team;

/// <summary>
/// Team of one player
/// </summary>
public sealed class TeamSingle : BaseTeam
{
    public TeamSingle(Guid id, DateTimeOffset createdOn) : base(id, createdOn, TeamType.Single)
    {
    }
}
