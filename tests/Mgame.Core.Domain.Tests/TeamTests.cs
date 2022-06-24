using System;
using FluentAssertions;
using Mgame.Core.Domain.Entities;
using Mgame.Core.Domain.Entities.Team;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.Exceptions;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class TeamTests
{
    [Fact]
    public void TestTeamFactory()
    {
        foreach(var teamType in TeamType.List)
        {
            var team = BaseTeam.Factory.NewTeam(teamType);
            team.Should().NotBeNull();
            team.Id.Should().NotBeEmpty();
            team.TeamCode.Should().Be(teamType.Name);
            team.Capacity.Should().Be(teamType.MembersCount);
            team.Should().BeAssignableTo<BaseTeam>();
        }
    }

    [Fact]
    public void TestTeamSingle()
    {
        var team = new TeamSingle(Guid.NewGuid(), DateTimeOffset.Now);
        team.Capacity.Should().Be(TeamType.Single.MembersCount);
        team.Players.Should().NotBeNull();

        for (var i = 0; i < team.Capacity; i++)
        {
            var playerId = Guid.NewGuid();
            var player = new Player(playerId, DateTimeOffset.Now, "just player");
            team.AddPlayer(player);
            Action addSamePlayerAction = () => team.AddPlayer(player);
            addSamePlayerAction.Should().ThrowExactly<PlayerAlreadyInTeamException>();
        }
        Action addExtraPlayerAction = () => team.AddPlayer(new Player(Guid.NewGuid(), DateTimeOffset.Now, "exceed player"));
        addExtraPlayerAction.Should().ThrowExactly<TeamCapacityExceededException>();

        team.Players.Should().NotBeNullOrEmpty();
        team.Players.Should().NotContainNulls();
        team.Players.Should().HaveCount(team.Capacity);
    }

    [Fact]
    public void TestTeamDouble()
    {
        var team = new TeamDouble(Guid.NewGuid(), DateTimeOffset.Now);
        team.Capacity.Should().Be(TeamType.Double.MembersCount);
        team.Players.Should().NotBeNull();

        for (var i = 0; i < team.Capacity; i++)
        {
            var playerId = Guid.NewGuid();
            var player = new Player(playerId, DateTimeOffset.Now, "just player");
            team.AddPlayer(player);
            Action addSamePlayerAction = () => team.AddPlayer(player);
            addSamePlayerAction.Should().ThrowExactly<PlayerAlreadyInTeamException>();
        }
        Action addExtraPlayerAction = () => team.AddPlayer(new Player(Guid.NewGuid(), DateTimeOffset.Now, "exceed player"));
        addExtraPlayerAction.Should().ThrowExactly<TeamCapacityExceededException>();

        team.Players.Should().NotBeNullOrEmpty();
        team.Players.Should().NotContainNulls();
        team.Players.Should().HaveCount(team.Capacity);
    }
}
