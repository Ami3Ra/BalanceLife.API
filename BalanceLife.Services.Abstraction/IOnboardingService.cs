using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.OnboardingDtos;

namespace BalanceLife.Services.Abstraction
{
    public interface IOnboardingService
    {
        Task SaveProfileAsync(string userId,CreateUserProfileDto dto);
        Task<OnboardingResultDto> GetResultAsync(
    string userId,
    string userName);
    }
}
