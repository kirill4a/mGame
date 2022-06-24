using System;
using Mgame.Common.Exceptions;

namespace Mgame.Common.Either
{
    public class ResultError : IResultError
    {
        public string Code { get; }

        public string Error { get; }

        public Exception Exception { get; }

        public ResultError(string error)
        {
            if (string.IsNullOrWhiteSpace(error)) throw new BlankStringArgumentException(error);
            Error = error;
        }

        public ResultError(string error, string code) : this(error)
        {
            Code = code;
        }

        internal ResultError(Exception exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public override string ToString()
        {
            var codeOrEmpty = string.IsNullOrWhiteSpace(Code) ? string.Empty : $"[{Code}]";
            return $"Error{codeOrEmpty}: {Error}";
        }
    }
}
