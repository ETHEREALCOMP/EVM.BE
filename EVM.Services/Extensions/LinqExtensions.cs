using EVM.Data.Enums;
using EVM.Data.Interfaces;
using EVM.Services.Features.Models.Requests;

namespace EVM.Services.Extensions;

#pragma warning disable IDE0046 // Convert to conditional expression
#pragma warning disable CS8602 // Dereference of a possibly null reference.
public static class LinqExtensions
{
    public static IQueryable<T> WithPagination<T>(this IQueryable<T> query, PaginationRequest paginationRequest)
    {
        return query
            .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize);
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, SortType? type)
        where T : ISortable
    {
        type ??= SortType.CheapLast;

        return type switch
        {
            SortType.CheapLast => query.OrderByDescending(x => x.Price),
            SortType.CheapFirst => query.OrderBy(x => x.Price),
            SortType.Name => query.OrderBy(x => x.Name),
            _ => query,
        };
    }

    public static IQueryable<T> WithFilters<T>(this IQueryable<T> query, IEnumerable<FilterRequest>? filters)
    {
        if (filters is null)
        {
            return query;
        }

        foreach (var filter in filters)
        {
            query = filter.Code switch
            {
                FilterType.StartDate => ApplyStartDateFilter(query, filter),
                FilterType.EndDate => ApplyEndDateFilter(query, filter),
                FilterType.MinPrice => ApplyMinPriceFilter(query, filter),
                FilterType.MaxPrice => ApplyMaxPriceFilter(query, filter),
                FilterType.Name => ApplyNameFilter(query, filter),
                _ => query,
            };
        }

        return query;
    }

    private static IQueryable<T> ApplyStartDateFilter<T>(IQueryable<T> query, FilterRequest filter)
    {
        if (typeof(IStartDate).IsAssignableFrom(typeof(T)) && filter.Value.TryGetDateTime(out var startDate))
        {
            return query.Where(x => (x as IStartDate).StartDate >= startDate);
        }

        return query;
    }

    private static IQueryable<T> ApplyEndDateFilter<T>(IQueryable<T> query, FilterRequest filter)
    {
        if (typeof(IEndDate).IsAssignableFrom(typeof(T)) && filter.Value.TryGetDateTime(out var endDate))
        {
            return query.Where(x => (x as IEndDate).EndDate <= endDate);
        }

        return query;
    }

    private static IQueryable<T> ApplyMinPriceFilter<T>(IQueryable<T> query, FilterRequest filter)
    {
        if (typeof(IPrice).IsAssignableFrom(typeof(T)) && filter.Value.TryGetInt32(out var minPrice))
        {
            return query.Where(x => (x as IPrice).Price >= minPrice);
        }

        return query;
    }

    private static IQueryable<T> ApplyMaxPriceFilter<T>(IQueryable<T> query, FilterRequest filter)
    {
        if (typeof(IPrice).IsAssignableFrom(typeof(T)) && filter.Value.TryGetInt32(out var maxPrice))
        {
            return query.Where(x => (x as IPrice).Price <= maxPrice);
        }

        return query;
    }

    private static IQueryable<T> ApplyNameFilter<T>(IQueryable<T> query, FilterRequest filter)
    {
        if (typeof(IName).IsAssignableFrom(typeof(T)) && filter.Value.TryGetString(out var name))
        {
            return query.Where(x => (x as IName).Name.Contains(name!));
        }

        return query;
    }
}
#pragma warning restore IDE0046 // Convert to conditional expression
#pragma warning restore CS8602 // Dereference of a possibly null reference.