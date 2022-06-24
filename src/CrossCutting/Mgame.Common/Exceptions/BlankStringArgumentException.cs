using System;

namespace Mgame.Common.Exceptions
{
    public class BlankStringArgumentException : ArgumentException
    {
        public BlankStringArgumentException(string paramName)
            : base("Value should be not empty or whitespace string", paramName)
        {
        }
    }
}