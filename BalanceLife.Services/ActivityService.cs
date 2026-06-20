using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.Activity_TimerDTOs;

namespace BalanceLife.Services
{
    public class ActivityService:IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> StartAsync(string userId, string type)
        {
            var repo = _unitOfWork.GetRepository<ActivitySession, int>();
            var existingSessions = await repo.GetAllAsync();

            var active = existingSessions
                .FirstOrDefault(x => x.UserId == userId && x.EndTime == null);

            if (active != null)
                throw new Exception("You already have an active activity");

            var session = new ActivitySession
            {
                UserId = userId,
                ActivityType = type,
                StartTime = DateTime.UtcNow
            };

            await repo.AddAsyns(session);
            await _unitOfWork.SaveChangesAsync();

            return session.Id;
        }

        public async Task<ActivityResultDto> EndAsync(string userId, int sessionId)
        {
            var repo = _unitOfWork.GetRepository<ActivitySession, int>();

            var session = await repo.GetByIdAsync(sessionId);

            if (session == null || session.UserId != userId)
                throw new Exception("Session not found");

            if (session.EndTime != null)
            {
                return new ActivityResultDto
                {
                    Duration = 0,
                    CaloriesBurned = session.CaloriesBurned
                };
            }

            session.EndTime = DateTime.UtcNow;

            var duration = Math.Max(1, (int)Math.Round(
                           (session.EndTime.Value - session.StartTime).TotalMinutes));

            double rate = session.ActivityType switch
            {
                "Cardio" => 8,
                "Strength" => 6,
                "Yoga" => 3,
                "Walking" => 4,
                _ => 5
            };

            session.CaloriesBurned = (int)(duration * rate);

            await _unitOfWork.SaveChangesAsync();

            return new ActivityResultDto
            {
                Duration = duration,
                CaloriesBurned = session.CaloriesBurned
            };
        }

        public async Task<ActivitySession?> GetActiveAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<ActivitySession, int>();

            var sessions = await repo.GetAllAsync();

            return sessions
                .Where(s => s.UserId == userId && s.EndTime == null)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefault();
        }

        public async Task<List<ActivityHistoryDto>> GetHistoryAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<ActivitySession, int>();

            var sessions = await repo.GetAllAsync();

            var result = sessions
                .Where(s => s.UserId == userId && s.EndTime != null)
                .OrderByDescending(s => s.StartTime)
                .Select(s => new ActivityHistoryDto
                {
                    ActivityType = s.ActivityType.ToString(),
                    Date = s.StartTime,
                    Duration = Math.Max(1, (int)Math.Round(
                        (s.EndTime.Value - s.StartTime).TotalMinutes)),
                    CaloriesBurned = (int)s.CaloriesBurned
                })
                .ToList();

            return result;
        }

        public async Task<WeeklySummaryDto> GetWeeklySummaryAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<ActivitySession, int>();

            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

            var sessions = await repo.GetAllAsync();

            var weekSessions = sessions
                .Where(s => s.UserId == userId &&
                            s.EndTime != null &&
                            s.StartTime >= startOfWeek)
                .ToList();

            return new WeeklySummaryDto
            {
                TotalWorkouts = weekSessions.Count,
                TotalDuration = weekSessions.Sum(s =>
                    Math.Max(1, (int)Math.Round(
                        (s.EndTime.Value - s.StartTime).TotalMinutes))),
                TotalCalories = weekSessions.Sum(s => (int)s.CaloriesBurned)
            };
        }
    }
}
