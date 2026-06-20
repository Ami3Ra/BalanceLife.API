using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.SportModule;

namespace BalanceLife.Services.Specifications.SportSpecifications
{
    public class WorkoutSpecification : BaseSpecifications<Workout, int>
    {
        public WorkoutSpecification(string? category, string? level)
        : base(w =>
            (string.IsNullOrEmpty(category) || w.Category == category) &&
            (string.IsNullOrEmpty(level) || w.Level.ToString() == level))
        {
        }
    }
}
