using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.DashboardModule;
using BalanceLife.Shared.DTOs.DashboardDtos;

namespace BalanceLife.Services.Abstraction
{
    public interface IUserGoalsService
    {
        Task SetDefaultGoalsAsync(string userId);
        Task CreateGoalsAsync(
        string userId,
        int caloriesGoal,
        int waterGoal,
        int activityGoal);

        Task UpdateGoalsAsync(string userId, CreateUserGoalsDto dto);
        Task<UserGoals?> GetGoalsAsync(string userId);
    }
}
