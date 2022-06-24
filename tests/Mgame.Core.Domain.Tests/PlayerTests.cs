using System;
using FluentAssertions;
using Mgame.Common.Exceptions;
using Mgame.Core.Domain.Entities;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class PlayerTests
{
    [Fact]
    public void TestPLayers()
    {
        Action wrongNickAction = () => _ = new Player(Guid.Empty, DateTimeOffset.Now, "  ");
        wrongNickAction.Should().ThrowExactly<BlankStringArgumentException>()
                                .WithMessage("Value should be not empty or whitespace string (Parameter 'nickName')")
                                .WithParameterName("nickName");

        var firstId = Guid.NewGuid();
        var firstNick = "Penguin adeliae";
        var firstPlayer = new Player(firstId, DateTimeOffset.Now, firstNick);
        firstPlayer.Id.Should().Be(firstId);
        firstPlayer.NickName.Should().Be(firstNick);

        // same players have same ids, even if the nicknames differs 
        var secondNick = "Penguin forsteri";
        var samePlayer = new Player(firstId, DateTimeOffset.Now, secondNick);
        samePlayer.Id.Should().Be(firstId);
        samePlayer.NickName.Should().Be(secondNick);

        samePlayer.Should().NotBeSameAs(firstPlayer);
        samePlayer.Should().Be(firstPlayer);

        // if player ids are different - player are different too, even if nicknames are the same
        var differentId = Guid.NewGuid();
        differentId.Should().NotBe(firstId);

        var differentPlayer = new Player(differentId, DateTimeOffset.Now, firstNick);
        differentPlayer.Id.Should().Be(differentId);
        differentPlayer.NickName.Should().Be(firstNick);
        differentPlayer.NickName.Should().Be(firstPlayer.NickName);
        differentPlayer.Should().NotBeSameAs(firstPlayer);
        differentPlayer.Should().NotBe(firstPlayer);
    }
}
