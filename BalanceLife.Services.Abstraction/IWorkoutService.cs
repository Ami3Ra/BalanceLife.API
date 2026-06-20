using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.SportDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IWorkoutService
    {
        // 🔹 Workouts
        Task<List<WorkoutDTO>> GetAllAsync(string? category, string? level);
        Task<GroupedWorkoutsDto> GetGroupedAsync();
        // 🔹 Session
        Task<WorkoutSessionDto> StartWorkoutAsync(string userId, int workoutId);
        Task<EndWorkoutResultDto> EndWorkoutAsync(string userId, int sessionId);
        Task<ActiveWorkoutSessionDto?> GetActiveSessionAsync(string userId);
        // 🔹 Analytics
        Task<List<WorkoutHistoryDto>> GetHistoryAsync(string userId);
        Task<TodayWorkoutSummaryDto> GetTodaySummaryAsync(string userId);
    }
}
