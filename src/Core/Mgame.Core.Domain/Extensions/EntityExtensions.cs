using System.Collections.Generic;
using System.Linq;
using Mgame.Core.Domain.Base.Interfaces;

namespace Mgame.Core.Domain.Extensions;

static class EntityExtensions
{
    /// <summary>
    /// Checks if collection is not null and all items in the collection are unique
    /// </summary>
    internal static bool AllItemsUnique<TId>(this IEnumerable<IEntity<TId>> items) =>
        !items.Any()
        || items.Distinct().Count() == items.Count();
}
