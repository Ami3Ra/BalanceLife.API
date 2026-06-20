using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.BreathingExerciseDTOs;

namespace BalanceLife.Services
{
    public class BreathingExerciseService : IBreathingExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BreathingExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BreathingExerciseDto>> GetAllAsync()
        {
            var repo = _unitOfWork.GetRepository<BreathingExercise, int>();
            var data = await repo.GetAllAsync();
            return data.Select(x => new BreathingExerciseDto
            {
                Id = x.Id,
                Name = x.Name,
                Duration = x.Duration,
                Pattern = x.Pattern,
                Description = x.Description,
                Benefit = x.Benefit
            }).ToList();
        }

        public async Task<BreathingExerciseDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<BreathingExercise, int>();
            var x = await repo.GetByIdAsync(id);

            if (x == null) return null;

            return new BreathingExerciseDto
            {
                Id = x.Id,
                Name = x.Name,
                Duration = x.Duration,
                Pattern = x.Pattern,
                Description = x.Description,
                Benefit = x.Benefit
            };
        }
    }
    }
