﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Data.Common.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<IQueryable<TEntity>> All();

        // IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
