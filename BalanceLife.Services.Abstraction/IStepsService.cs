using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.StepsCounterDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IStepsService
    {
        Task AddStepsAsync(string userId, int steps);

        Task<TodayStepsDto> GetTodayAsync(string userId);

        Task ResetTodayAsync(string userId);

        Task<List<WeeklyStepsDto>> GetWeeklyAsync(string userId);
    }
}
