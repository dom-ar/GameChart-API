using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class GameRepositoryExtensions
{
    public static IQueryable<Game> Sort(this IQueryable<Game> games, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return games.OrderBy(g => g.Id);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Game>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return games.OrderBy(g => g.Id);

        return games.OrderBy(orderQuery);
    }
}