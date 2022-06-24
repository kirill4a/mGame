using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mgame.Common.Either;
using Mgame.Core.Application.Exceptions;
using Mgame.Core.Application.Interfaces.Repositories.Domain;
using Mgame.Core.Domain.Entities;
using Mgame.Core.Domain.Entities.Team;

namespace Mgame.Core.Application.Commands.Teams;

public record AddPlayerCommand(Guid TeamId, Guid PlayerId) : IRequest<Result>
{
}

class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Result>
{
    readonly IMemberRepository _memberRepository;
    readonly ITeamRepository _teamRepository;
    readonly IRoomRepository _roomRepository;

    public AddPlayerCommandHandler(
        IMemberRepository memberRepository,
        ITeamRepository teamRepository,
        IRoomRepository roomRepository)
    {
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }

    public async Task<Result> Handle(AddPlayerCommand request, CancellationToken cancellation)
    {
        request.Deconstruct(out var teamId, out var playerId);
        try
        {
            var player = (await _memberRepository.FindAsync(playerId).ConfigureAwait(false) as Player) ??
                throw new EntityNotFoundException<Player, Guid>(playerId);
            var team = (await _teamRepository.FindAsync(teamId).ConfigureAwait(false)) ??
                throw new EntityNotFoundException<BaseTeam, Guid>(teamId);
            if (_roomRepository.AnyOpenedRoom(player.Id))
                throw new PlayerIsBusyException(player);

            team.AddPlayer(player);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Fail().WithException(ex);
        }
    }
}
