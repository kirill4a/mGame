using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mgame.Common.Either;
using Mgame.Core.Application.Exceptions;
using Mgame.Core.Application.Interfaces.Repositories.Domain;
using Mgame.Core.Domain.Entities;

namespace Mgame.Core.Application.Commands.Rooms;

public class AddViewerCommand : IRequest<Result<Guid>>
{
    private AddViewerCommand(Guid roomId)
    {
        if (roomId == default)
            throw new ArgumentException($"{nameof(roomId)} can't be empty", nameof(roomId));
        RoomId = roomId;
    }

    /// <summary>
    /// Adds existing (registered) viewer to the room
    /// </summary>
    /// <param name="roomId">Room identifier</param>
    /// <param name="viewerId">Existing viewer identifier</param>
    /// <exception cref="ArgumentException">Viewer with given id not found</exception>
    public AddViewerCommand(Guid roomId, Guid viewerId) : this(roomId)
    {
        if (viewerId == default)
            throw new ArgumentException($"{nameof(viewerId)} can't be empty", nameof(viewerId));
        ViewerId = viewerId;
    }

    /// <summary>
    /// Adds new transient viewer to the room
    /// </summary>
    /// <param name="roomId">Room identifier</param>
    /// <param name="nickName">Viewer's nick name</param>
    /// <exception cref="ArgumentException">NickName is empty</exception>
    public AddViewerCommand(Guid roomId, string nickName) : this(roomId)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            throw new ArgumentException($"{nameof(nickName)} can't be empty", nameof(nickName));
        NickName = nickName;
    }

    public Guid RoomId { get; }
    public Guid ViewerId { get; }
    public string NickName { get; }
}

class AddViewerCommandHandler : IRequestHandler<AddViewerCommand, Result<Guid>>
{
    readonly IRoomRepository _roomRepository;
    readonly IMemberRepository _memberRepository;

    public AddViewerCommandHandler(IRoomRepository roomRepository, IMemberRepository memberRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
    }

    public async Task<Result<Guid>> Handle(AddViewerCommand request, CancellationToken cancellationToken)
    {
        var (roomId, viewerId, nickName) = (request.RoomId, request.ViewerId, request.NickName);
        var room = await _roomRepository.FindAsync(roomId).ConfigureAwait(false);

        Viewer viewer;
        if (viewerId != default)
        {
            viewer = (await _memberRepository.FindAsync(viewerId).ConfigureAwait(false) as Viewer)
                            ?? throw new EntityNotFoundException<Viewer, Guid>(viewerId);
        }
        else
        {
            viewer = new Viewer(Guid.NewGuid(), DateTimeOffset.Now, nickName);
        }
        room.AddViewer(viewer);

        return Result<Guid>.Success().WithData(viewer.Id);
    }
}
