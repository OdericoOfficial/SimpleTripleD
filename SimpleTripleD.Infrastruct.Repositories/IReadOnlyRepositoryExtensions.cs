using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;
using System.Linq.Expressions;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public static class IReadOnlyRepositoryExtensions
    {
        public static Task<bool> AnyAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AnyAsync(expression, cancellationToken);

        public static Task<bool> AllAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AllAsync(expression, cancellationToken);

        public static Task<bool> ContainsAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .ContainsAsync(aggregateRoot, cancellationToken);

        public static Task<TAggregateRoot> FirstAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .FirstAsync(expression, cancellationToken);

        public static Task<TAggregateRoot?> FirstOrDefaultAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .FirstOrDefaultAsync(expression, cancellationToken);

        public static Task<bool> AnyAsync<TAggregateRoot, TKey>(this IReadOnlyRepository<TAggregateRoot, TKey> repository, TKey id,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>
            => repository.AnyAsync(item => item.Id!.Equals(id), cancellationToken);

        public static Task<TAggregateRoot> FirstAsync<TAggregateRoot, TKey>(this IReadOnlyRepository<TAggregateRoot, TKey> repository, TKey id,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>
            => repository.FirstAsync(item => item.Id!.Equals(id), cancellationToken);

        public static Task<TAggregateRoot?> FirstOrDefaultAsync<TAggregateRoot, TKey>(this IReadOnlyRepository<TAggregateRoot, TKey> repository, TKey id,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>
            => repository.FirstOrDefaultAsync(item => item.Id!.Equals(id), cancellationToken);

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .ToListAsync(cancellationToken);

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, bool>> expression, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .ToListAsync(cancellationToken);

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository,
            int skip, int take, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, bool>> expression, int skip, int take, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, TResult>> selector, IComparer<TResult>? comparer = null,
                bool desc = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
        {
            var query = repository.AsQueryable();

            if (desc)
            {
                if (comparer is null)
                    query = query.OrderByDescending(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }
            else
            {
                if (comparer is null)
                    query = query.OrderBy(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }

            return query.ToListAsync(cancellationToken);
        }

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, TResult>> selector, int skip, int take, IComparer<TResult>? comparer = null,
                bool desc = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
        {
            var query = repository.AsQueryable();

            if (desc)
            {
                if (comparer is null)
                    query = query.OrderByDescending(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }
            else
            {
                if (comparer is null)
                    query = query.OrderBy(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }

            return query.Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken);
        }

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, TResult>> selector, int skip, int take,
                IComparer<TResult>? comparer = null, bool desc = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
        {
            var query = repository.AsQueryable()
                .Where(expression);

            if (desc)
            {
                if (comparer is null)
                    query = query.OrderByDescending(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }
            else
            {
                if (comparer is null)
                    query = query.OrderBy(selector);
                else
                    query = query.OrderByDescending(selector, comparer);
            }

            return query.Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken);
        }

        public static Task<List<TAggregateRoot>> ToListAsync<TAggregateRoot, TKey>(this IReadOnlyRepository<TAggregateRoot, TKey> repository, IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>
            => (from item in repository.AsQueryable()
                join id in ids
                on item.Id equals id
                select item).ToListAsync(cancellationToken);

        public static Task<long> CountAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, CancellationToken cancellationToken = default)
            where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .LongCountAsync(cancellationToken);

        public static Task<long> CountAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .LongCountAsync(cancellationToken);

        public static Task<TResult> MinAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, TResult>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .MinAsync(selector, cancellationToken);

        public static Task<TResult> MinAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, TResult>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .MinAsync(selector, cancellationToken);

        public static Task<TResult> MaxAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, TResult>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .MaxAsync(selector, cancellationToken);

        public static Task<TResult> MaxAsync<TAggregateRoot, TResult>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, TResult>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .MaxAsync(selector, cancellationToken);

        public static Task<decimal> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, decimal>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<decimal> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, decimal>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<decimal?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, decimal?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<decimal?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, decimal?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<int> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, int>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<int> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, int>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<int?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, int?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<int?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, int?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<long> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, long>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<long> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, long>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<long?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, long?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<long?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, long?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<float> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, float>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<float> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, float>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<float?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, float?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<float?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, float?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<double> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, double>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<double> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, double>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<double?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, double?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .SumAsync(selector, cancellationToken);

        public static Task<double?> SumAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, double?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .SumAsync(selector, cancellationToken);

        public static Task<decimal> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, decimal>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<decimal> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, decimal>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<decimal?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, decimal?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<decimal?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, decimal?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, int>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, int>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, int?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, int?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, long>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, long>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, long?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, long?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, double>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, double>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, double?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, double?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<float> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, float>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<float> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, float>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);

        public static Task<float?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, float?>> selector,
            CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .AverageAsync(selector, cancellationToken);

        public static Task<float?> AverageAsync<TAggregateRoot>(this IReadOnlyRepository<TAggregateRoot> repository, Expression<Func<TAggregateRoot, bool>> expression,
            Expression<Func<TAggregateRoot, float?>> selector, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot
            => repository.AsQueryable()
                .Where(expression)
                .AverageAsync(selector, cancellationToken);
    }
}