using System;
using Mgame.Common.Exceptions;

namespace Mgame.Core.Domain.Entities;

/// <summary>
/// Viewer of the game
/// </summary>
public class Viewer : Member
{
    const string AnonymousNickName = "Anonymous";

    public Viewer(Guid id, DateTimeOffset createdOn, string nickName) : base(id, createdOn)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            throw new BlankStringArgumentException(nameof(nickName));
        NickName = nickName;
    }

    public Viewer(Guid id, DateTimeOffset createdOn) : this(id, createdOn, AnonymousNickName)
    {
    }

    public bool Readonly { get; private set; }

    public void Mute() => Readonly = true;
    public void Unmute() => Readonly = false;

    public override void BroadcastMessage(string message)
    {
        var msg = $"Greeting! I'm '{NickName}', I'm {(Readonly ? nameof(Readonly) : "Speakable")} viewer!";
        Console.WriteLine(msg);
    }
}
