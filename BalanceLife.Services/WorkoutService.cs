using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Services.Specifications.SportSpecifications;
using BalanceLife.Shared.DTOs.SportDTOs;
using BalanceLife.Domain.Entities.OnboardingModule;
using BalanceLife.Services.Specifications.OnboardingSpecifications;

namespace BalanceLife.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserGoalsService _userGoalsService;

        public WorkoutService(IUnitOfWork unitOfWork, IUserGoalsService userGoalsService)
        {
            _unitOfWork = unitOfWork;
            _userGoalsService = userGoalsService;
        }

        public async Task<List<WorkoutDTO>> GetAllAsync(string? category, string? level)
        {
            var repo = _unitOfWork.GetRepository<Workout, int>();

            var spec = new WorkoutSpecification(category, level);

            var workouts = await repo.GetAllAsync(spec);

            return workouts.Select(w => new WorkoutDTO
            {
                Id = w.Id,
                Name = w.Name,
                Category = w.Category,
                Level = w.Level.ToString(),
                Duration = w.DurationInMinutes,
                Calories = w.Calories
            }).ToList();
        }


        public async Task<GroupedWorkoutsDto> GetGroupedAsync()
        {
            var repo = _unitOfWork.GetRepository<Workout, int>();

            var workouts = await repo.GetAllAsync();

            var mapped = workouts
           .OrderBy(w => w.Level) // 🔥 الترتيب بالـ enum
           .Select(w => new WorkoutDTO
            {
              Id = w.Id,
              Name = w.Name,
              Category = w.Category,
              Level = w.Level.ToString(),
              Duration = w.DurationInMinutes,
              Calories = w.Calories
            })
            .ToList();

            return new GroupedWorkoutsDto
            {
                Cardio = mapped.Where(x => x.Category == "Cardio").ToList(),
                Strength = mapped.Where(x => x.Category == "Strength").ToList(),
                Flexibility = mapped.Where(x => x.Category == "Flexibility").ToList(),
                Home = mapped.Where(x => x.Category == "Home").ToList()
            };
        }

        public async Task<WorkoutSessionDto> StartWorkoutAsync(string userId, int workoutId)
        {
            var repo = _unitOfWork.GetRepository<WorkoutSession, int>();

            var workoutRepo = _unitOfWork.GetRepository<Workout, int>();
            var workout = await workoutRepo.GetByIdAsync(workoutId);
            if (workout == null)
                throw new Exception("Workout not found");

            var existingSessions = await repo.GetAllAsync();

            var active = existingSessions.FirstOrDefault(x =>
                x.UserId == userId && x.EndTime == null);

            if (active != null)
                throw new Exception("You already have an active workout");

            var session = new WorkoutSession
            {
                UserId = userId,
                WorkoutId = workoutId,
                StartTime = DateTime.UtcNow
            };

            await repo.AddAsyns(session);
            await _unitOfWork.SaveChangesAsync();

            return new WorkoutSessionDto
            {
                SessionId = session.Id,
                WorkoutName = workout.Name,
                StartTime = session.StartTime
            };
        }

        public async Task<EndWorkoutResultDto> EndWorkoutAsync(string userId, int sessionId)
        {
            var sessionRepo = _unitOfWork.GetRepository<WorkoutSession, int>();
            var workoutRepo = _unitOfWork.GetRepository<Workout, int>();

            var session = await sessionRepo.GetByIdAsync(sessionId);

            if (session == null || session.UserId != userId)
                throw new Exception("Session not found");

            if (session.EndTime != null)
            {
                return new EndWorkoutResultDto
                {
                    Duration = 0,
                    CaloriesBurned = session.CaloriesBurned,
                    AlreadyEnded = true
                };
            }

            var workout = await workoutRepo.GetByIdAsync(session.WorkoutId);

            if (workout == null)
                throw new Exception("Workout not found");

            if (workout.DurationInMinutes == 0)
                throw new Exception("Invalid workout duration");

            session.EndTime = DateTime.UtcNow;

            var duration = Math.Round((session.EndTime.Value - session.StartTime).TotalMinutes, 2);

            var caloriesPerMinute = (double)workout.Calories / workout.DurationInMinutes;

            session.CaloriesBurned = (int)(duration * caloriesPerMinute);

            await _unitOfWork.SaveChangesAsync();

            return new EndWorkoutResultDto
            {
                Duration = duration,
                CaloriesBurned = session.CaloriesBurned
            };
        }

        public async Task<ActiveWorkoutSessionDto?> GetActiveSessionAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WorkoutSession, int>();

            var sessions = await repo.GetAllAsync();

            var active = sessions
                .Where(s => s.UserId == userId && s.EndTime == null)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefault();

            if (active == null)
                return null;

            var workoutRepo = _unitOfWork.GetRepository<Workout, int>();
            var workout = await workoutRepo.GetByIdAsync(active.WorkoutId);

            return new ActiveWorkoutSessionDto
            {
                SessionId = active.Id,
                WorkoutName = workout?.Name,
                StartTime = active.StartTime
            };
        }

        public async Task<List<WorkoutHistoryDto>> GetHistoryAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WorkoutSession, int>();
            var workoutRepo = _unitOfWork.GetRepository<Workout, int>();

            var sessions = await repo.GetAllAsync();

            var userSessions = sessions
                .Where(s => s.UserId == userId && s.EndTime != null)
                .OrderByDescending(s => s.StartTime)
                .ToList();

            var result = new List<WorkoutHistoryDto>();

            foreach (var s in userSessions)
            {
                var workout = await workoutRepo.GetByIdAsync(s.WorkoutId);

                result.Add(new WorkoutHistoryDto
                {
                    WorkoutName = workout?.Name,
                    Date = s.StartTime,
                    Duration = Math.Round((s.EndTime.Value - s.StartTime).TotalMinutes, 1),
                    CaloriesBurned = s.CaloriesBurned
                });
            }

            return result;
        }

        public async Task<TodayWorkoutSummaryDto> GetTodaySummaryAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<WorkoutSession, int>();

            var today = DateTime.UtcNow.Date;

            var sessions = await repo.GetAllAsync();

            var todaySessions = sessions
               .Where(s => s.UserId == userId &&
                     s.EndTime != null &&
                     s.EndTime.Value.Date == today)
              .ToList();

            return new TodayWorkoutSummaryDto
            {
                TotalWorkouts = todaySessions.Count,
                TotalDuration = Math.Round(todaySessions.Sum(s =>
                 (s.EndTime.Value - s.StartTime).TotalMinutes),1),
                TotalCalories = todaySessions.Sum(s => s.CaloriesBurned)
            };
        }


        public async Task<RecommendedWorkoutDto> GetRecommendedWorkoutAsync(string userId)
        {
            var repo = _unitOfWork.GetRepository<UserProfile, int>();

            var spec = new UserProfileSpecification(userId);

            var profile = await repo.GetByIdAsync(spec);

            if (profile == null)
                throw new Exception("User profile not found");

            if (profile.Goal.Equals("Gain Muscle", StringComparison.OrdinalIgnoreCase))
            {
                return new RecommendedWorkoutDto
                {
                    CurrentGoal = profile.Goal,
                    WorkoutType = "Mixed Cardio & Strength",
                    Duration = 30,
                    ExpectedBurn = 200,
                    Tip = "Balance cardio and strength training for overall wellness."
                };
            }

            if (profile.Goal.Equals("Lose Weight", StringComparison.OrdinalIgnoreCase))
            {
                return new RecommendedWorkoutDto
                {
                    CurrentGoal = profile.Goal,
                    WorkoutType = "Cardio",
                    Duration = 45,
                    ExpectedBurn = 400,
                    Tip = "Focus on cardio to burn more calories."
                };
            }

            if (profile.Goal.Equals("Maintain Weight", StringComparison.OrdinalIgnoreCase))
            {
                return new RecommendedWorkoutDto
                {
                    CurrentGoal = profile.Goal,
                    WorkoutType = "Mixed Training",
                    Duration = 30,
                    ExpectedBurn = 250,
                    Tip = "Balance strength and cardio to maintain your fitness."
                };
            }

            return new RecommendedWorkoutDto
            {
                CurrentGoal = profile.Goal,
                WorkoutType = "General Fitness",
                Duration = 30,
                ExpectedBurn = 200,
                Tip = "Stay active every day."
            };
        }

    }

}


