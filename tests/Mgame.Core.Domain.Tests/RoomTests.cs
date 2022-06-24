using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using FluentAssertions;
using Mgame.Core.Domain.Aggregates.Room;
using Mgame.Core.Domain.Entities;
using Mgame.Core.Domain.Entities.Team;
using Mgame.Core.Domain.Exceptions;
using Mgame.Core.Domain.ValueObjects;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class RoomTests
{
    [Fact]
    public void TestConstructors()
    {
        var roomId = Guid.NewGuid();
        var now = DateTimeOffset.Now;
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);

        var room = new RoomSingle(roomId, now, metadata);
        room.Should().NotBeNull();
        room.Should().BeAssignableTo<BaseRoom>();
        room.Id.Should().Be(roomId);
        room.CreatedOn.Should().Be(now);
        room.Viewers.Should().NotBeNull().And.BeEmpty();
        room.Teams.Should().NotBeNull().And.BeEmpty();
        room.Teams.Should().BeAssignableTo<IReadOnlyCollection<BaseTeam>>().And.AllBeOfType<TeamSingle>();
    }

    [Fact]
    public void TestCreateSingleRoom()
    {
        var roomId = Guid.NewGuid();
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);

        var singlePlayersRoom = new RoomSingle(roomId, DateTimeOffset.Now, metadata);

        singlePlayersRoom.Should().BeAssignableTo<BaseRoom>();
        singlePlayersRoom.Id.Should().Be(roomId);

        singlePlayersRoom.Metadata.Should().NotBeNull();
        singlePlayersRoom.Metadata.Name.Should().Be(metadata.Name);
        singlePlayersRoom.Metadata.Owner.Should().Be(metadata.Owner);
        singlePlayersRoom.Metadata.Address.Should().Be(address);

        singlePlayersRoom.Teams.Should().NotBeNull();
        singlePlayersRoom.Teams.Should().BeEmpty();
        singlePlayersRoom.Viewers.Should().NotBeNull();
        singlePlayersRoom.Viewers.Should().BeEmpty();
    }

    [Fact]
    public void TestCreateDoubleRoom()
    {
        var roomId = Guid.NewGuid();
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);

        var doublePlayersRoom = new RoomDouble(roomId, DateTimeOffset.Now, metadata);
        doublePlayersRoom.Should().BeAssignableTo<BaseRoom>();
        doublePlayersRoom.Id.Should().Be(roomId);

        doublePlayersRoom.Metadata.Should().NotBeNull();
        doublePlayersRoom.Metadata.Name.Should().Be(metadata.Name);
        doublePlayersRoom.Metadata.Owner.Should().Be(metadata.Owner);
        doublePlayersRoom.Metadata.Address.Should().Be(address);

        doublePlayersRoom.Teams.Should().NotBeNull();
        doublePlayersRoom.Teams.Should().BeEmpty();
        doublePlayersRoom.Viewers.Should().NotBeNull();
        doublePlayersRoom.Viewers.Should().BeEmpty();
    }

    [Fact]
    public void TestRoomsEquality()
    {
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        var firstAddress = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var secondAddress = new RoomAddress(null, "Local device", PhysicalAddress.None, IPAddress.Loopback);

        var firstMetadata = new RoomMetadata("impressive owner", "the best room ever", firstAddress);
        var secondMetadata = new RoomMetadata("impressive owner", "the best room ever", secondAddress);

        var originalRoom = new RoomSingle(firstId, DateTimeOffset.Now, firstMetadata);
        originalRoom.Id.Should().Be(firstId);

        // room with the same Id and the same type
        var cloneRoom = new RoomSingle(firstId, DateTimeOffset.Now, secondMetadata);
        cloneRoom.Id.Should().Be(firstId).And.Be(originalRoom.Id);
        cloneRoom.Metadata.Should().NotBe(originalRoom.Metadata);
        cloneRoom.Should().NotBeSameAs(originalRoom);
        cloneRoom.Should().Be(originalRoom);

        // room with the same Id and the different type
        var differTeamTypeRoom = new RoomDouble(firstId, DateTimeOffset.Now, secondMetadata);
        differTeamTypeRoom.Id.Should().Be(firstId).And.Be(originalRoom.Id);
        differTeamTypeRoom.Metadata.Should().NotBe(originalRoom.Metadata);
        differTeamTypeRoom.Should().NotBeSameAs(originalRoom);
        differTeamTypeRoom.Should().NotBe(originalRoom);

        // room with the different Id and the same type
        var differIdRoom = new RoomSingle(secondId, DateTimeOffset.Now, firstMetadata);
        differIdRoom.Id.Should().Be(secondId).And.NotBe(originalRoom.Id);
        differIdRoom.Metadata.Should().Be(originalRoom.Metadata);
        differIdRoom.Should().NotBeSameAs(originalRoom);
        differIdRoom.Should().NotBe(originalRoom);
    }

    [Fact]
    public void TestAddSingleTeams()
    {
        var maxTeamsCount = 4;
        var roomId = Guid.NewGuid();
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);

        var room = new RoomSingle(roomId, DateTimeOffset.Now, metadata);
        room.Teams.Should().NotBeNull();
        room.Teams.Should().BeEmpty();

        while (maxTeamsCount > 0)
        {
            room.AddTeam();
            maxTeamsCount--;
        }            
        room.Teams.Should().HaveCount(4);
                    
        Action addExtraTeamAction = () => room.AddTeam();
        addExtraTeamAction.Should().ThrowExactly<RoomCapacityExceededException>();
        room.Teams.Should().HaveCount(4);

        room.Teams.Should().NotContainNulls();
        room.Teams.Should().OnlyHaveUniqueItems();
        room.Teams.Should().AllBeOfType<TeamSingle>();
    }

    [Fact]
    public void TestAddDoubleTeams()
    {
        var roomId = Guid.NewGuid();
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);
                    
        var room = new RoomDouble(roomId, DateTimeOffset.Now, metadata);
        room.Teams.Should().NotBeNull();
        room.Teams.Should().BeEmpty();

        room.AddTeam();
        room.Teams.Should().HaveCount(1);

        room.AddTeam();
        room.Teams.Should().HaveCount(2);

        Action addExtraTeamAction = () => room.AddTeam();
        addExtraTeamAction.Should().ThrowExactly<RoomCapacityExceededException>();
        room.Teams.Should().HaveCount(2);

        room.Teams.Should().NotContainNulls();
        room.Teams.Should().OnlyHaveUniqueItems();
        room.Teams.Should().AllBeOfType<TeamDouble>();
    }

    [Fact]
    public void TestAddViewer()
    {
        var roomId = Guid.NewGuid();
        var address = new RoomAddress(null, "Any device", PhysicalAddress.None, IPAddress.None);
        var metadata = new RoomMetadata("impressive owner", "the best room ever", address);

        var viewerOne = new Viewer(Guid.NewGuid(), DateTimeOffset.Now);
        var viewerTwo = new Viewer(Guid.NewGuid(), DateTimeOffset.Now);
        var viewerThree = new Viewer(Guid.NewGuid(), DateTimeOffset.Now);

        var room = new RoomSingle(roomId, DateTimeOffset.Now, metadata);
        room.Viewers.Should().NotBeNull();
        room.Viewers.Should().BeEmpty();

        room.AddViewer(viewerOne);
        room.Viewers.Should().HaveCount(1);

        Action addSameViewerAction = () => room.AddViewer(viewerOne);
        addSameViewerAction.Should().ThrowExactly<MemberAlreadyInRoomException>();
        room.Viewers.Should().HaveCount(1);

        room.AddViewer(viewerTwo);
        room.AddViewer(viewerThree);
        room.Viewers.Should().HaveCount(3);
        room.Viewers.Should().NotContainNulls();
        room.Viewers.Should().OnlyHaveUniqueItems();
    }
}
