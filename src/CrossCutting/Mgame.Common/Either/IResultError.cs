using System;

namespace Mgame.Common.Either
{
    /// <summary>
    /// Error object
    /// </summary>
    public interface IResultError
    {
        /// <summary>
        /// Error code
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Error text
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Exception object
        /// </summary>
        Exception Exception { get; }
    }
}