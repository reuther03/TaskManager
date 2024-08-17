using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Pagination;

namespace TaskManager.Abstractions.QueriesAndCommands.TypeExtensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);

        var results = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(page, pageSize, count, results);
    }

    public static async Task<PaginatedList<TOut>> ToPagedListAsync<T, TOut>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        Expression<Func<T, TOut>> mappingExpression,
        CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);

        var results = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(mappingExpression)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TOut>(page, pageSize, count, results);
    }

    public static IQueryable<T> FilterByProperty<T>(
        this IQueryable<T> source,
        Expression<Func<T, string>> propertySelector,
        string searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
            return source;

        var memberExpression = (MemberExpression)propertySelector.Body;
        if (memberExpression.Type != typeof(string))
            throw new ArgumentException("Property must be of type string");

        var parameter = propertySelector.Parameters[0];
        var notNullExpression = Expression.NotEqual(memberExpression, Expression.Constant(null, typeof(string)));
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)]);
        var containsExpression = Expression.Call(memberExpression, containsMethod, Expression.Constant(searchValue));
        var andExpression = Expression.AndAlso(notNullExpression, containsExpression);

        var lambda = Expression.Lambda<Func<T, bool>>(andExpression, parameter);

        return source.Where(lambda);
    }

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }
}