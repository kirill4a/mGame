using System;
using Mgame.Common.Exceptions;

namespace Mgame.Core.Domain.Entities;

/// <summary>
/// Player (participant) of the game
/// </summary>
public sealed class Player : Member
{
    public Player(Guid id, DateTimeOffset createdOn, string nickName)
        : base(id, createdOn)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            throw new BlankStringArgumentException(nameof(nickName));
        NickName = nickName;
    }

    public override void BroadcastMessage(string message)
    {
        var msg = $"Greeting! I'm '{NickName}', I'm player!";
        Console.WriteLine(msg);
    }
}
