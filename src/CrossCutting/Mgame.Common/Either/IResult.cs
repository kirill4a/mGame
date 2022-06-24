using System.Collections.Generic;

namespace Mgame.Common.Either
{
    /// <summary>
    /// Result object
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Errors of result
        /// </summary>
        IReadOnlyCollection<IResultError> Errors { get; }

        /// <summary>
        /// Message of result
        /// </summary>
        string Message { get; }

        bool IsSucceeded { get; }
    }

    /// <summary>
    /// Result object with data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
