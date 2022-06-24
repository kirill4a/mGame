using System;
using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.Entities;

/// <summary>
/// Member of the game (player, viewer, judge etc)
/// </summary>
public abstract class Member : EntityBase<Guid>
{
    protected Member(Guid id, DateTimeOffset createdOn) : base(id, createdOn)
    {
    }

    public string NickName { get; protected set; }

    public abstract void BroadcastMessage(string message);
}
