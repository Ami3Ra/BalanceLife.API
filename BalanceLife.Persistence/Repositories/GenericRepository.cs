using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities;
using BalanceLife.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BalanceLife.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly BalanceDbContext _dbContext;

        public GenericRepository(BalanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        { 
           return  await _dbContext.Set<TEntity>().ToListAsync();
        }
        

        public async Task<TEntity?> GetByIdAsync(TKey id)
          => await _dbContext.Set<TEntity>().FindAsync(id);
        

        public async Task AddAsyns(TEntity entity)
        {
          await  _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var Query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications);
            return await Query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var query = SpecificationEvaluator
            .CreateQuery(_dbContext.Set<TEntity>(), specifications);

            return await query.ToListAsync();

        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator
                         .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                         .CountAsync();
        }
    }
}
