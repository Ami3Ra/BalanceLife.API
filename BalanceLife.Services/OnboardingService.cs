using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.DashboardModule;
using BalanceLife.Domain.Entities.OnboardingModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.OnboardingDtos;

namespace BalanceLife.Services
{
    public class OnboardingService : IOnboardingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserGoalsService _userGoalsService;

        public OnboardingService(
            IUnitOfWork unitOfWork,
            IUserGoalsService userGoalsService)
        {
            _unitOfWork = unitOfWork;
            _userGoalsService = userGoalsService;
        }
        public async Task SaveProfileAsync(
            string userId,
            CreateUserProfileDto dto)
        {
            var repo =
                _unitOfWork.GetRepository<UserProfile, int>();

            var profile = (await repo.GetAllAsync())
    .FirstOrDefault(x => x.UserId == userId);

            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId
                };

                await repo.AddAsyns(profile);
            }

            profile.Age = dto.Age;
            profile.Gender = dto.Gender;
            profile.Height = dto.Height;
            profile.Weight = dto.Weight;
            profile.Goal = dto.Goal;
            profile.ActivityLevel = dto.ActivityLevel;
            profile.SleepHours = dto.SleepHours;
            profile.WaterIntake = dto.WaterIntake;
            profile.JobType = dto.JobType;

            await _unitOfWork.SaveChangesAsync();

            double bmr;

            if (dto.Gender == "Male")
            {
                bmr =
                    (10 * dto.Weight)
                    + (6.25 * dto.Height)
                    - (5 * dto.Age)
                    + 5;
            }
            else
            {
                bmr =
                    (10 * dto.Weight)
                    + (6.25 * dto.Height)
                    - (5 * dto.Age)
                    - 161;
            }

            double multiplier = dto.ActivityLevel switch
            {
                "Sedentary" => 1.2,
                "Light Activity" => 1.375,
                "Moderate" => 1.55,
                "Active" => 1.725,
                "Very Active" => 1.9,
                _ => 1.2
            };

            var tdee = bmr * multiplier;
            int caloriesGoal = (int)tdee;

            switch (dto.Goal)
            {
                case "Lose Weight":
                    caloriesGoal -= 500;
                    break;

                case "Gain Muscle":
                    caloriesGoal += 300;
                    break;

                case "Improve Fitness":
                    caloriesGoal += 100;
                    break;

                case "Reduce Stress":
                    caloriesGoal -= 100;
                    break;
            }

            int waterGoal =
    (int)Math.Ceiling(
        dto.Weight * 35 / 250.0);

            int activityGoal =
    dto.ActivityLevel switch
    {
        "Sedentary" => 30,
        "Light Activity" => 45,
        "Moderate" => 60,
        "Active" => 75,
        "Very Active" => 90,
        _ => 60
    };
            if (dto.Goal == "Lose Weight")
            {
                activityGoal += 15;
            }

            await _userGoalsService.CreateGoalsAsync(
     userId,
     caloriesGoal,
     waterGoal,
     activityGoal);
        }

        public async Task<OnboardingResultDto> GetResultAsync(
    string userId,
    string userName)
        {
            var profileRepo =
                _unitOfWork.GetRepository<UserProfile, int>();

            var goalsRepo =
                _unitOfWork.GetRepository<UserGoals, int>();

            var profile =
                (await profileRepo.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            var goals =
                (await goalsRepo.GetAllAsync())
                .FirstOrDefault(x => x.UserId == userId);

            if (profile == null || goals == null)
                throw new Exception("Profile not found");

            return new OnboardingResultDto
            {
                Name = userName,
                Goal = profile.Goal,

                CaloriesGoal = goals.CaloriesGoal,
                WaterGoal = goals.WaterGoal,
                ActivityGoal = goals.ActivityGoal
            };
        }
    }
}
