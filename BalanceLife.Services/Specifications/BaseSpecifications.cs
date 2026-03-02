using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities;

namespace BalanceLife.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> :ISpecifications<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity,bool>> criteriaExp) 
        {
            Criteria = criteriaExp;
        }

        #region Filteration
        public Expression<Func<TEntity, bool>> Criteria { get; } 
        #endregion


        #region Pagination
        public int Skip { private set; get; }

        public int Take { private set; get; }

        public bool IsPaginated { private set; get; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
        #endregion

        #region Ordering
        public Expression<Func<TEntity, object>> OrderBy { private set; get; }

        public Expression<Func<TEntity, object>> OrderByDescending { private set; get; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp)
        {
            OrderByDescending = orderByDescExp;
        } 
        #endregion

    }
}
