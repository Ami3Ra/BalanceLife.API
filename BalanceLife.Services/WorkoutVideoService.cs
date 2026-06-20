using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.WorkoutVideosDTOs;

namespace BalanceLife.Services
{
    public class WorkoutVideoService : IWorkoutVideoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutVideoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WorkoutVideoDto>> GetAllAsync()
        {
            var repo = _unitOfWork.GetRepository<WorkoutVideo, int>();

            var videos = await repo.GetAllAsync();

            return videos.Select(v => new WorkoutVideoDto
            {
                Id = v.Id,
                Title = v.Title,
                Trainer = v.Trainer,
                Duration = v.Duration,
                Calories = v.Calories,
                Level = v.Level,
                Icon = v.Icon,
                VideoUrl = v.VideoUrl
            }).ToList();
        }

        public async Task<WorkoutVideoDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<WorkoutVideo, int>();

            var v = await repo.GetByIdAsync(id);

            if (v == null) return null;

            return new WorkoutVideoDto
            {
                Id = v.Id,
                Title = v.Title,
                Trainer = v.Trainer,
                Duration = v.Duration,
                Calories = v.Calories,
                Level = v.Level,
                Icon = v.Icon,
                VideoUrl = v.VideoUrl
            };
        }

        public async Task<List<WorkoutVideoDto>> GetFilteredAsync(string? level, int? maxDuration)
        {
            var repo = _unitOfWork.GetRepository<WorkoutVideo, int>();

            var videos = await repo.GetAllAsync();

            if (!string.IsNullOrEmpty(level))
            {
                videos = videos
                    .Where(v => v.Level.Equals(level, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (maxDuration.HasValue)
            {
                videos = videos
                    .Where(v => v.Duration <= maxDuration.Value)
                    .ToList();
            }
            return videos.Select(v => new WorkoutVideoDto
            {
                Id = v.Id,
                Title = v.Title,
                Trainer = v.Trainer,
                Duration = v.Duration,
                Calories = v.Calories,
                Level = v.Level,
                Icon = v.Icon,
                VideoUrl = v.VideoUrl
            }).ToList();
        }
    }
}
