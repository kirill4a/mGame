using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mgame.Common.Either;
using Mgame.Core.Application.Interfaces.Repositories;
using Mgame.Core.Application.Interfaces.Repositories.Domain;

namespace Mgame.Core.Application.Commands.Members;

public record CreatePlayerCommand(string NickName) : IRequest<Result<Guid>>
{
}

class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Result<Guid>>
{
    readonly IMemberRepository _memberRepository;
    readonly IUnitOfWork _unitOfWork;

    public CreatePlayerCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellation)
    {
        throw new NotImplementedException();
        //request.Deconstruct(out var nickName);
        //try
        //{
        //    var existedPlayer = await _memberRepository.FindByNickNameAsync(nickName).ConfigureAwait(false);
        //    if(existedPlayer != null)
        //        throw new

        //    var newPlayer = new Player(Guid.NewGuid(), DateTimeOffset.Now, nickName);
        //    _memberRepository.Add(newPlayer);
        //    await _unitOfWork.SaveChangesAsync(cancellation).ConfigureAwait(false);

        //    return Result<Guid>.Success().WithData(newPlayer.Id);
        //}
        //catch (Exception ex)
        //{
        //    return Result<Guid>.Fail().WithException(ex);
        //}
    }
}
