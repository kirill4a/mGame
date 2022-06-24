using System;
using Mgame.Core.Domain.Enums;

namespace Mgame.Core.Domain.Entities.Team;

/// <summary>
/// Team of two players
/// </summary>
public sealed class TeamDouble : BaseTeam
{
    public TeamDouble(Guid id, DateTimeOffset createdOn) : base(id, createdOn, TeamType.Double)
    {
    }
}
