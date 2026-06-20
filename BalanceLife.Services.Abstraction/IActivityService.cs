using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Shared.DTOs.Activity_TimerDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IActivityService
    {
        Task<int> StartAsync(string userId, string type);
        Task<ActivityResultDto> EndAsync(string userId, int sessionId);
        Task<ActivitySession?> GetActiveAsync(string userId);
        Task<List<ActivityHistoryDto>> GetHistoryAsync(string userId);
        Task<WeeklySummaryDto> GetWeeklySummaryAsync(string userId);
    }
}
