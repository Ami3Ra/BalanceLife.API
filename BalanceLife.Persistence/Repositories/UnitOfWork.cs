using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities;
using BalanceLife.Persistence.Data.DbContexts;

namespace BalanceLife.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BalanceDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(BalanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var entityType = typeof(TEntity);
            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }
            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);

            _repositories[entityType] = newRepo;
            return newRepo;
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
    }
}
