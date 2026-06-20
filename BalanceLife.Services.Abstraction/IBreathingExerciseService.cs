using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.DTOs.BreathingExerciseDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IBreathingExerciseService
    {
        Task<List<BreathingExerciseDto>> GetAllAsync();
        Task<BreathingExerciseDto?> GetByIdAsync(int id);
    }
}
