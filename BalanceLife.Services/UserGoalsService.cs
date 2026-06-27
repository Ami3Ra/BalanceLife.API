using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.DashboardModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.DashboardDtos;

namespace BalanceLife.Services
{
    public class UserGoalsService : IUserGoalsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserGoalsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SetDefaultGoalsAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<UserGoals, int>();

            var all = await repo.GetAllAsync();

            var exists = all.Any(x => x.UserId == userId);

            if (exists) return;

            await repo.AddAsyns(new UserGoals
            {
                UserId = userId,
                CaloriesGoal = 2000,
                WaterGoal = 8,
                ActivityGoal = 60
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateGoalsAsync(
    string userId,
    int caloriesGoal,
    int waterGoal,
    int activityGoal)
        {
            var repo = _unitOfWork.GetRepository<UserGoals, int>();

            var goals = (await repo.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (goals != null)
            {
                goals.CaloriesGoal = caloriesGoal;
                goals.WaterGoal = waterGoal;
                goals.ActivityGoal = activityGoal;
            }
            else
            {
                await repo.AddAsyns(new UserGoals
                {
                    UserId = userId,
                    CaloriesGoal = caloriesGoal,
                    WaterGoal = waterGoal,
                    ActivityGoal = activityGoal
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateGoalsAsync(string userId, CreateUserGoalsDto dto)
        {
            var repo = _unitOfWork.GetRepository<UserGoals, int>();

            var goals = (await repo.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (goals == null)
                throw new Exception("Goals not found");

            goals.CaloriesGoal = dto.CaloriesGoal;
            goals.WaterGoal = dto.WaterGoal;
            goals.ActivityGoal = dto.ActivityGoal;

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<UserGoals?> GetGoalsAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<UserGoals, int>();

            return (await repo.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);
        }
    }
}

