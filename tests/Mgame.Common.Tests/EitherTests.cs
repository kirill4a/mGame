using System;
using System.Linq;
using FluentAssertions;
using Mgame.Common.Either;
using Xunit;

namespace Mgame.Common.Tests;

public class EitherTests
{
    [Fact]
    public void TestSuccessResult()
    {
        var result = Result.Success();
        result.IsSucceeded.Should().BeTrue();
        result.Message.Should().BeNullOrEmpty();
        result.Errors.Should().NotBeNull().And.BeEmpty();

        var message = "Hi! All is ok!";
        var resultSameInstance = result.WithMessage(message);
        resultSameInstance.Should().BeSameAs(result);
        result.IsSucceeded.Should().BeTrue();
        result.Message.Should().Be(message);
        result.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void TestFailResult()
    {
        var message = "Some troubles";
        var result = Result.Fail().WithMessage(message);
        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(message);
        result.Errors.Should().NotBeNull().And.BeEmpty();

        var anotherMessage = "I can fix it";
        var firstError = new ResultError("Ou, shi! Null reference!");
        var resultSameInstance = result.WithMessage(anotherMessage).WithError(firstError);
        resultSameInstance.Should().BeSameAs(result);
        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(anotherMessage);
        result.Errors.Should().NotBeNullOrEmpty().And.NotContainNulls();
        result.Errors.Should().HaveCount(1);

        var secondError = new ResultError("Unauthorized!");
        var thirdError = new ResultError("Not implemented!");
        var yetAnotherSameInstance = result.WithError(secondError).WithError(thirdError);
        yetAnotherSameInstance.Should().BeSameAs(result);
        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(anotherMessage);
        result.Errors.Should().NotBeNullOrEmpty().And.NotContainNulls();
        result.Errors.Should().HaveCount(3);
        result.Errors.Last().Should().Be(thirdError);
    }

    [Fact]
    public void TestExceptionResult()
    {
        var message = "Not implemented yet";
        var exception = new NotImplementedException(message);
        var result = Result.Fail().WithException(exception);

        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().BeNullOrEmpty();

        result.Errors.Should().NotBeNullOrEmpty().And.NotContainNulls();
        result.Errors.Should().HaveCount(1);

        var firstError = result.Errors.First();
        firstError.Code.Should().BeNullOrEmpty();
        firstError.Error.Should().BeNullOrEmpty();
        firstError.Exception.Should().NotBeNull();
        firstError.Exception.Should().Be(exception);
        firstError.Exception.Should().BeSameAs(exception);
        firstError.Exception.Should().BeOfType<NotImplementedException>();
        firstError.Exception.Message.Should().Be(message);
    }

    [Fact]
    public void TestResultWithData()
    {
        var messageStart = "Here will be my string data";
        var data = "QWERTY";
        var messageFinish = "My data has been come";
        var result = Result<string>.Success().WithMessage(messageStart);

        result.IsSucceeded.Should().BeTrue();
        result.Data.Should().BeNull();
        result.Message.Should().Be(messageStart);

        _ = result.WithData(data).WithMessage(messageFinish);
        result.Data.Should().NotBeNullOrEmpty().And.Be(data);
        result.Message.Should().Be(messageFinish);

        result.Errors.Should().NotBeNull().And.BeEmpty();
    }
}
