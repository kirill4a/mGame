using System;

namespace Mgame.Common.Exceptions
{
    public class NotUniqueItemsCollectionException : ArgumentException
    {
        public NotUniqueItemsCollectionException(string paramName)
            : base($"{paramName} should be unique items collection", paramName)
        {
        }
    }
}