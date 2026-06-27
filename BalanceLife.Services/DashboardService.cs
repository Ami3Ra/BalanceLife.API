using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Domain.Entities.WaterModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.DashboardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalanceLife.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealTrackingService _mealTrackingService;
        private readonly IUserGoalsService _userGoalsService;

        public DashboardService(
            IUnitOfWork unitOfWork,
            IMealTrackingService mealTrackingService, 
            IUserGoalsService userGoalsService)
        {
            _unitOfWork = unitOfWork;
            _mealTrackingService = mealTrackingService;
            _userGoalsService = userGoalsService;
        }

        public async Task<DashboardDto> GetDashboardAsync(string userId, string userName)
        {
            var today = DateTime.UtcNow.Date;
            var goals = await _userGoalsService.GetGoalsAsync(userId);

            // ================= WATER =================
            var waterRepo = _unitOfWork.GetRepository<WaterIntake, int>();
            var waterData = await waterRepo.GetAllAsync();

            var todayWaterMl = waterData
                .Where(x => x.UserId == userId && x.CreatedAt.Date == today)
                .Sum(x => x.AmountInMl);

            var waterCups = todayWaterMl / 250;

            // ================= ACTIVITY =================
            var activityRepo = _unitOfWork.GetRepository<ActivitySession, int>();
            var activities = await activityRepo.GetAllAsync();


            var todayActivities = activities
                .Where(x => x.UserId == userId &&
                            x.StartTime.Date == today &&
                            x.EndTime.HasValue)
                .ToList();

            var activityMinutes = todayActivities
                .Sum(x => (int)(x.EndTime!.Value - x.StartTime).TotalMinutes);

            // ================= HEALTH TIP =================
            var remaining = Math.Max(0, 60 - activityMinutes);

            var tip = remaining > 0
                ? $"To optimize your day, try to add {remaining} more minutes of activity."
                : "Great job! You reached today's activity goal.";

            // ================= CALORIES =================
            var calories = await _mealTrackingService.GetTodayCaloriesAsync(userId);

            // ================= TIMELINE =================
            var timeline = new List<TimelineItemDto>();

            // Water timeline
            timeline.AddRange(
                waterData
                    .Where(x => x.UserId == userId && x.CreatedAt.Date == today)
                    .Select(x => new TimelineItemDto
                    {
                        Time = x.CreatedAt.ToString("HH:mm"),
                        Title = "Water",
                        Value = $"{x.AmountInMl} ml"
                    })
            );

            // Activity timeline
            timeline.AddRange(
                todayActivities.Select(x => new TimelineItemDto
                {
                    Time = x.StartTime.ToString("HH:mm"),
                    Title = x.ActivityType,
                    Value = $"{Math.Max(1,
                        (int)(x.EndTime!.Value - x.StartTime).TotalMinutes)} min"
                })
            );

            timeline = timeline
                .OrderBy(x => TimeSpan.Parse(x.Time))
                .ToList();

            // ================= RESPONSE =================
            return new DashboardDto
            {
                UserName = userName,
                Date = today,

                Calories = new CaloriesDto
                {
                    Current = calories,
                    Goal = goals?.CaloriesGoal ?? 2000
                },

                Water = new WaterDto
                {
                    Current = waterCups,
                    Goal = goals?.WaterGoal ?? 8
                },

                Activity = new ActivityDto
                {
                    Current = activityMinutes,
                    Goal = goals?.ActivityGoal ?? 60
                },

                HealthTip = tip,
                Timeline = timeline
            };
        }
    }
}