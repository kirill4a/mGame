using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mgame.Common.Either;
using Mgame.Core.Application.Interfaces.Repositories;
using Mgame.Core.Application.Interfaces.Repositories.Domain;
using Mgame.Core.Domain.Aggregates.Room;
using Mgame.Core.Domain.Enums;
using Mgame.Core.Domain.ValueObjects;

namespace Mgame.Core.Application.Commands.Rooms;

//TODO: incapsulate room attributies to DTO
public record CreateRoomCommand(string Owner, string Name, RoomAddress Address, TeamType TeamType) 
    : IRequest<Result<Guid>>
{
}

class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<Guid>>
{
    static readonly IDictionary<TeamType, Func<RoomMetadata, BaseRoom>> _creators = new Dictionary<TeamType, Func<RoomMetadata, BaseRoom>>
       {
            { TeamType.Single, (metadata) => new RoomSingle(Guid.NewGuid(), DateTimeOffset.Now, metadata) },
            { TeamType.Double, (metadata) => new RoomDouble(Guid.NewGuid(), DateTimeOffset.Now, metadata) }
       };

    readonly IRoomRepository _roomRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(CreateRoomCommand request, CancellationToken cancellation)
    {
        request.Deconstruct(out var owner, out var name, out var address, out var teamType);
        //TODO: use mapper DTO->domain value object
        var room = CreateNewRoom(teamType, new RoomMetadata(owner, name, address));
        await _roomRepository.AddAsync(room).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellation).ConfigureAwait(false);
        
        return Result<Guid>.Success().WithData(room.Id);
    }

    static BaseRoom CreateNewRoom(TeamType teamType, RoomMetadata metadata)
    {
        if (!_creators.TryGetValue(teamType, out var createFunc))
            throw new NotSupportedException($"{nameof(TeamType)} '{teamType.Name}' is not supported in rooms");
        return createFunc.Invoke(metadata);
    }
}
