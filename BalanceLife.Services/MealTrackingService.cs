using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Services.Abstraction;

namespace BalanceLife.Services
{
    public class MealTrackingService:IMealTrackingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MealTrackingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ================= ADD MEAL LOG =================
        public async Task AddMealLogAsync(string userId, int mealId)
        {
            var repo = _unitOfWork.GetRepository<MealLog, int>();

            await repo.AddAsyns(new MealLog
            {
                UserId = userId,
                MealId = mealId,
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();
        }
        // ================= TODAY CALORIES =================
        public async Task<int> GetTodayCaloriesAsync(string userId)
        {
            var today = DateTime.UtcNow.Date;

            var mealRepo = _unitOfWork.GetRepository<Meal, int>();
            var logRepo = _unitOfWork.GetRepository<MealLog, int>();

            var logs = await logRepo.GetAllAsync();
            var meals = await mealRepo.GetAllAsync();

            var todayLogs = logs
                .Where(x => x.UserId == userId &&
                            x.CreatedAt.Date == today);

            return todayLogs
                .Join(
                    meals,
                    log => log.MealId,
                    meal => meal.Id,
                    (log, meal) => meal.Calories
                )
                .Sum();
        }
    }
}
