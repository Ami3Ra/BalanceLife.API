using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.WaterDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IWaterService
    {
        Task AddWaterAsync(string userId, int amountInMl);
        Task<WaterTodayDto> GetTodayWaterAsync(string userId);
        Task<WaterStreakDto> GetStreakAsync(string userId);
        Task<WeeklyWaterDto> GetWeeklyWaterAsync(string userId);
        Task<List<WaterTimelineDto>> GetTodayTimelineAsync(string userId);
        Task DeleteWaterAsync(int waterId, string userId);
    }
}
