using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.WaterModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Services.Exceptions;
using BalanceLife.Services.Specifications;
using BalanceLife.Services.Specifications.WaterSpecifications;
using BalanceLife.Shared.DTOs.WaterDTOs;

namespace BalanceLife.Services
{
    public class WaterService:IWaterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WaterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddWaterAsync(string userId, int amountInMl)
        {
            var water = new WaterIntake
            {
                UserId = userId,
                AmountInMl = amountInMl,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<WaterIntake, int>().AddAsyns(water);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<WaterTodayDto> GetTodayWaterAsync(string userId)
        {
            var today = DateTime.UtcNow.Date;

            var repo = _unitOfWork.GetRepository<WaterIntake, int>();

            var spec = new TodayWaterSpecification(
                     userId,
                     DateTime.UtcNow.Date
                    );

            var waters = await _unitOfWork
                .GetRepository<WaterIntake, int>()
                .GetAllAsync(spec);

            var totalMl = waters.Sum(x => x.AmountInMl);

            var goal = 2000;

            var cups = totalMl / 250;
            var maxCups = goal / 250;

            cups = Math.Min(cups, maxCups);

            string message;

            if (totalMl == 0)
            {
                message = "Start tracking your water today 💧";
            }
            else if (totalMl < goal * 0.5)
            {
                message = "Good start! Keep drinking 💪";
            }
            else if (totalMl < goal)
            {
                message = "You're almost there! 🚀";
            }
            else
            {
                message = "Great job! 🎉 You've reached your goal!";
            }

            return new WaterTodayDto
            {
                TotalMl = totalMl,
                GoalMl = goal,
                Percentage = goal == 0 ? 0 : (totalMl * 100.0 / goal),
                Cups = cups,
                IsGoalCompleted = totalMl >= goal,
                StatusMessage = message
            };
        }

        public async Task<WaterStreakDto> GetStreakAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WaterIntake, int>();

            var spec = new WaterAllSpecification(userId);

            var allWater = await repo.GetAllAsync(spec);
           

            var goal = 2000;
            
            // نجمع per day
            var daily = allWater
                .GroupBy(w => w.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Total = g.Sum(x => x.AmountInMl)
                })
                .OrderByDescending(x => x.Date)
                .ToList();

            int currentStreak = 0;
            int longestStreak = 0;
            int tempStreak = 0;

            foreach (var day in daily)
            {
                if (day.Total >= goal)
                {
                    tempStreak++;
                    longestStreak = Math.Max(longestStreak, tempStreak);
                }
                else
                {
                    tempStreak = 0;
                }
            }

            // حساب current streak من النهارده
            var today = DateTime.UtcNow.Date;
            currentStreak = 0;

            // لو النهارده مش كامل → خلاص 0
            if (!daily.Any(d => d.Date == today && d.Total >= goal))
            {
                currentStreak = 0;
            }
            else
            {
                // نبدأ نحسب من النهارده
                var currentDate = today;

                while (true)
                {
                    var day = daily.FirstOrDefault(d => d.Date == currentDate);

                    if (day != null && day.Total >= goal)
                    {
                        currentStreak++;
                        currentDate = currentDate.AddDays(-1);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var isTodayCompleted = daily.Any(d => d.Date == today && d.Total >= goal);

            return new WaterStreakDto
            {
                CurrentStreak = currentStreak,
                LongestStreak = longestStreak,
                IsTodayCompleted = isTodayCompleted
            };
        }

        public async Task<WeeklyWaterDto> GetWeeklyWaterAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WaterIntake, int>();

            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-6);

            var goal = 2000;

            var spec = new WeeklyWaterSpecification(
                userId,
                startOfWeek,
                today.AddDays(1)
            );

            var waters = await repo.GetAllAsync(spec);

            // group per day
            var dailyData = waters
                .GroupBy(w => w.CreatedAt.Date)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.AmountInMl));

            var result = new WeeklyWaterDto();

            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);

                dailyData.TryGetValue(date, out int total);

                var cappedValue = Math.Min(total, goal);

                result.Days.Add(new DailyWaterDto
                {
                    DayName = date.ToString("ddd"),
                    TotalMl = total,
                    DisplayMl = cappedValue,
                    IsOverGoal = total >= goal
                });
            }

            result.WeeklyTotal = result.Days.Sum(d => d.TotalMl);

            result.DailyAverage = result.WeeklyTotal / 7.0;

            return result;
        }

        public async Task<List<WaterTimelineDto>> GetTodayTimelineAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WaterIntake, int>();

            var today = DateTime.UtcNow.Date;

            var spec = new TodayWaterTimelineSpecification(userId, today);

            var waters = await repo.GetAllAsync(spec);

            return waters.Select(w => new WaterTimelineDto
            {
                Id = w.Id,
                Time = w.CreatedAt.ToLocalTime().ToString("hh:mm tt"),
                AmountInMl = w.AmountInMl
            }).ToList();
        }

        public async Task DeleteWaterAsync(int waterId, string userId)
        {
            var repo = _unitOfWork.GetRepository<WaterIntake, int>();

            var water = await repo.GetByIdAsync(waterId);

            if (water == null)
                throw new WaterNotFoundException(waterId);

            if (water.UserId != userId)
                throw new UnauthorizedAccessException("You can't delete this record.");

            repo.Delete(water);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
