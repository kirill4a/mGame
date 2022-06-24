using System;
using System.Threading.Tasks;
using Mgame.Core.Domain.Entities;

namespace Mgame.Core.Application.Interfaces.Repositories.Domain;

public interface IMemberRepository : IEntityRepository<Member, Guid>
{
    Member FindByNickName(string nickName);
    Task<Member> FindByNickNameAsync(string nickName);
}
