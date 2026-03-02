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
    internal class MealSpecification : BaseSpecifications<Meal, int>
    {
        public MealSpecification(MealQueryParams queryParams)
        : base(MealSpecificationHelper.BuildCriteria(queryParams))
        {
            switch (queryParams.sort)
            {
                case MealSortingOptions.NameAsc:
                    AddOrderBy(M => M.Name);
                    break;
                case MealSortingOptions.NameDesc:
                    AddOrderByDescending(M => M.Name);
                    break;
                case MealSortingOptions.CaloriesAsc:
                    AddOrderBy(M => M.Calories);
                    break;
                case MealSortingOptions.CaloriesDesc:
                    AddOrderByDescending(M => M.Calories);
                    break;
                case MealSortingOptions.RecommendedAsc:
                    AddOrderBy(M => M.IsRecommended);
                    break;
                case MealSortingOptions.RecommendedDesc:
                    AddOrderByDescending(M => M.IsRecommended);
                    break;
                default:
                    AddOrderBy(M => M.Id);
                    break;

            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }

        public MealSpecification(int id):base(X=>X.Id==id)
        {
            
        }

        
    }
}
