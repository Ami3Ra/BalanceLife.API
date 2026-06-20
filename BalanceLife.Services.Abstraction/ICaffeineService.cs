using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.CaffeineDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface ICaffeineService
    {
        Task<List<CaffeineTipDto>> GetTipsAsync();
        Task<List<CaffeineGuideDto>> GetGuideAsync();
        Task<List<PreWorkoutCaffeineDto>> GetPreWorkoutAsync();
        Task<List<CaffeineReferenceDto>> GetReferenceAsync();
    }
}
