using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.DashboardDtos;

namespace BalanceLife.Services.Abstraction
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync(string userId, string userName);
    }
}
