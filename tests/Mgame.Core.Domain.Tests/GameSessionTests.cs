using System;
using System.Threading;
using Mgame.Core.Domain.Aggregates;
using Xunit;

namespace Mgame.Core.Domain.Tests;

public class GameSessionTests
{
    [Fact]
    public void TestCreateSession()
    {
        var createDate = DateTimeOffset.Now;
        var expiryDate = createDate;
        Assert.Throws<ArgumentException>(() => _ = new GameSession(0, createDate, expiryDate));

        expiryDate = DateTimeOffset.Now.AddMinutes(-1);
        Assert.Throws<ArgumentException>(() => _ = new GameSession(0, createDate, expiryDate));

        expiryDate = DateTimeOffset.Now.AddHours(4);
        var session = new GameSession(0, createDate, expiryDate);
        Assert.Equal(0, session.Id);
        Assert.Equal(createDate, session.CreatedOn);
        Assert.Equal(expiryDate, session.ExpiryOn);            

        Assert.Null(session.OpenedOn);
        Assert.Null(session.ClosedOn);
        
        Assert.False(session.IsActive);
        Assert.False(session.IsExpired);
    }

    [Fact]
    public void TestOpenSession()
    {
        var createDate = DateTimeOffset.Now;
        var expiryDate = createDate.AddHours(1);
        var session = new GameSession(0, createDate, expiryDate);
        session.Open();
        
        Assert.NotNull(session.OpenedOn);
        Assert.InRange(session.OpenedOn.Value, session.CreatedOn, session.ExpiryOn);

        Assert.True(session.IsActive);
        Assert.False(session.IsExpired);

        // shouldn't open session more than once or reopen
        Assert.Throws<InvalidOperationException>(() => session.Open());
        session.Close();
        Assert.Throws<InvalidOperationException>(() => session.Open());
    }

    [Fact]
    public void TestCloseSession()
    {
        var createDate = DateTimeOffset.Now;
        var expiryDate = createDate.AddHours(1);
        var session = new GameSession(0, createDate, expiryDate);

        // shouldn't close unopened session
        Assert.Throws<InvalidOperationException>(() => session.Close());
                    
        session.Open();
        Assert.True(session.IsActive);
        Assert.NotNull(session.OpenedOn);

        Assert.Null(session.ClosedOn);
        session.Close();
        Assert.NotNull(session.ClosedOn);
        Assert.InRange(session.ClosedOn.Value, session.OpenedOn.Value, session.ExpiryOn);
        Assert.False(session.IsActive);            

        // shouldn't close session more than once
        Assert.Throws<InvalidOperationException>(() => session.Close());
    }

    [Fact]
    public void TestExpireSession()
    {
        var createDate = DateTimeOffset.Now;
        var expiryDate = createDate.AddMilliseconds(100);
        var session = new GameSession(0, createDate, expiryDate);
        Assert.Equal(expiryDate, session.ExpiryOn);
        Assert.False(session.IsExpired);
        Thread.Sleep(100);
        Assert.True(session.IsExpired);
        Assert.Equal(expiryDate, session.ExpiryOn);
    }
}
