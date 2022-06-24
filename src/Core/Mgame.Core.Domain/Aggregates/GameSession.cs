using System;
using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.Aggregates;

/// <summary>
/// Session of the game
/// </summary>
public class GameSession : AggregateRootBase<int>
{
    public GameSession(int id, DateTimeOffset createdOn, DateTimeOffset expiryOn)
        : base(id, createdOn)
    {
        if (expiryOn <= createdOn) 
            throw new ArgumentException("Expiry date should be greater than create date");
        CreatedOn = createdOn;
        ExpiryOn = expiryOn;
    }

    public DateTimeOffset ExpiryOn { get; private set; }
    public DateTimeOffset? OpenedOn { get; private set; }
    public DateTimeOffset? ClosedOn { get; private set; }
    
    public bool IsExpired => DateTimeOffset.Now >= ExpiryOn;
    public bool IsActive => OpenedOn.HasValue && !ClosedOn.HasValue;

    public void Open()
    {
        if (IsExpired) throw new InvalidOperationException("Can't open expired session");
        if (OpenedOn.HasValue) throw new InvalidOperationException("Session is already opened");
        if (ClosedOn.HasValue) throw new InvalidOperationException("Reopening session is not allowed");

        OpenedOn = DateTimeOffset.UtcNow;
        //TODO: save to storage or raize event
    }

    public void Close()
    {
        if (!OpenedOn.HasValue) throw new InvalidOperationException("Can't close unopened session");
        if (ClosedOn.HasValue) throw new InvalidOperationException("Session is already closed");

        ClosedOn = DateTimeOffset.UtcNow;
        //TODO: save to storage or raize event
    }
}
