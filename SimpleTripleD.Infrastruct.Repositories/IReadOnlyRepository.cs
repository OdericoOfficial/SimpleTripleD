﻿using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IReadOnlyRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        IQueryable<TAggregateRoot> AsQueryable();
    }

    public interface IReadOnlyRepository<TAggregateRoot, TKey> : IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
    }
}