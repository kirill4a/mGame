using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mgame.Common.Either;
using Mgame.Core.Application.Exceptions;
using Mgame.Core.Application.Interfaces.Repositories;
using Mgame.Core.Application.Interfaces.Repositories.Domain;
using Mgame.Core.Domain.Aggregates.Room;

namespace Mgame.Core.Application.Commands.Rooms;

public record AddTeamCommand(Guid RoomId) : IRequest<Result<Guid>>
{
}

class AddTeamToRoomCommandHandler : IRequestHandler<AddTeamCommand, Result<Guid>>
{
    readonly IRoomRepository _roomRepository;
    readonly IUnitOfWork _unitOfWork;

    public AddTeamToRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(AddTeamCommand request, CancellationToken cancellation)
    {
        request.Deconstruct(out var roomId);
        var room = (await _roomRepository.FindAsync(roomId).ConfigureAwait(false))
            ?? throw new EntityNotFoundException<BaseRoom, Guid>(roomId);

        var team = room.AddTeam();
        _roomRepository.Update(room);
        await _unitOfWork.SaveChangesAsync(cancellation).ConfigureAwait(false);

        return Result<Guid>.Success().WithData(team.Id);
    }
}
