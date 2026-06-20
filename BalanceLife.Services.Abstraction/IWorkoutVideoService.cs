using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.WorkoutVideosDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IWorkoutVideoService
    {
        Task<List<WorkoutVideoDto>> GetAllAsync();

        Task<WorkoutVideoDto?> GetByIdAsync(int id);

        Task<List<WorkoutVideoDto>> GetFilteredAsync(string? level, int? maxDuration);
    }
}
