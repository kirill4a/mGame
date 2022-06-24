using System;
using System.Collections.Generic;

namespace Mgame.Common.Either
{
    public class Result : IResult
    {
        protected readonly List<IResultError> _errors = new List<IResultError>();

        protected Result(bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
        }

        public static Result Success() => new Result(true);
        public static Result Fail() => new Result(false);

        public bool IsSucceeded { get; }

        public string Message { get; protected set; }

        public IReadOnlyCollection<IResultError> Errors => _errors.AsReadOnly();

        public Result WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public Result WithError(IResultError error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            _errors.Add(error);
            return this;
        }

        public Result WithException(Exception exception)
        {
            _errors.Add(new ResultError(exception));
            return this;
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        private Result(bool isSucceeded) : base(isSucceeded)
        {
        }

        public static new Result<T> Success() => new Result<T>(true);
        public static new Result<T> Fail() => new Result<T>(false);

        public T Data { get; private set; }

        public Result<T> WithData(T data)
        {
            Data = data;
            return this;
        }

        public new Result<T> WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public new Result<T> WithError(IResultError error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            _errors.Add(error);
            return this;
        }

        public new Result<T> WithException(Exception exception)
        {
            _errors.Add(new ResultError(exception));
            return this;
        }
    }
}
