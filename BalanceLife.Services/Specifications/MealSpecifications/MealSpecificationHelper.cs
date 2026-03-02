using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Shared;

namespace BalanceLife.Services.Specifications.MealSpecifications
{
    internal static class MealSpecificationHelper
    {
        public static Expression<Func<Meal, bool>> BuildCriteria(MealQueryParams queryParams)
        {
            return m =>
                (!queryParams.isRecommended.HasValue
                    || m.IsRecommended == queryParams.isRecommended.Value)

                &&

                (string.IsNullOrEmpty(queryParams.search)
                    || m.Name.ToLower().Contains(queryParams.search.ToLower()));
        }
    }
}
