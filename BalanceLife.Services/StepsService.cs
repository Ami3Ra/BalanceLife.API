using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.StepsCounterDTOs;

namespace BalanceLife.Services
{
    public class StepsService : IStepsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StepsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddStepsAsync(string userId, int steps)
        {
            var repo = _unitOfWork.GetRepository<StepRecord, int>();

            var today = DateTime.UtcNow.Date;

            var records = await repo.GetAllAsync();

            var record = records
                .FirstOrDefault(x => x.UserId == userId && x.Date == today);

            if (record == null)
            {
                record = new StepRecord
                {
                    UserId = userId,
                    Date = today,
                    Steps = 0
                };

                await repo.AddAsyns(record);
            }

            record.Steps += steps;

            // 🧮 الحسابات
            record.Distance = record.Steps * 0.0008m;
            record.Calories = (int)Math.Round(record.Steps * 0.04);
            record.ActiveTime = record.Steps / 100;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TodayStepsDto> GetTodayAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<StepRecord, int>();

            var today = DateTime.UtcNow.Date;

            var records = await repo.GetAllAsync();

            var record = records
                .FirstOrDefault(x => x.UserId == userId && x.Date == today);

            if (record == null)
            {
                return new TodayStepsDto();
            }

            return new TodayStepsDto
            {
                Steps = record.Steps,
                Remaining = Math.Max(0, 10000 - record.Steps),
                Distance = (double)record.Distance,
                Calories = record.Calories,
                ActiveTime = record.ActiveTime
            };
        }

        public async Task ResetTodayAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<StepRecord, int>();

            var today = DateTime.UtcNow.Date;

            var records = await repo.GetAllAsync();

            var record = records
                .FirstOrDefault(x => x.UserId == userId && x.Date == today);

            if (record == null)
                return;

            record.Steps = 0;
            record.Distance = 0;
            record.Calories = 0;
            record.ActiveTime = 0;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<WeeklyStepsDto>> GetWeeklyAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<StepRecord, int>();

            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

            var endOfWeek = startOfWeek.AddDays(7);

            var records = await repo.GetAllAsync();

            var weekData = records
                .Where(x => x.UserId == userId &&
                            x.Date >= startOfWeek &&
                            x.Date < endOfWeek)
                .ToList();

            var result = weekData
                .GroupBy(x => x.Date.DayOfWeek)
                .Select(g => new WeeklyStepsDto
                {
                    Day = g.Key.ToString(),
                    Steps = g.Sum(x => x.Steps)
                })
                .ToList();

            return result;
        }
    }
}
