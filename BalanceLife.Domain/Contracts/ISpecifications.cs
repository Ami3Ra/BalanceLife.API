using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities;

namespace BalanceLife.Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity:BaseEntity<TKey> 
    {
        Expression<Func<TEntity,bool>> Criteria { get; }
        Expression<Func<TEntity,object>> OrderBy { get; }
        Expression<Func<TEntity,object>> OrderByDescending {  get; }
        int Skip {  get; }
        int Take {  get; }

        bool IsPaginated { get; }
    }
}
