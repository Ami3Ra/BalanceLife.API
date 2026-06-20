using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.CaffeineDTOs;

namespace BalanceLife.Services
{
    public class CaffeineService : ICaffeineService
    {
        public async Task<List<CaffeineTipDto>> GetTipsAsync()
        {
            return new List<CaffeineTipDto>
        {
            new CaffeineTipDto
            {
                Title = "Avoid caffeine after 6 PM",
                Description = "Late caffeine disrupts sleep quality and recovery"
            },
            new CaffeineTipDto
            {
                Title = "Caffeine stays 6-8 hours in body",
                Description = "Half-life affects you longer than you think"
            },
            new CaffeineTipDto
            {
                Title = "Try herbal tea instead",
                Description = "Chamomile or peppermint for evening relaxation"
            },
            new CaffeineTipDto
            {
                Title = "Hydrate more, caffeinate less",
                Description = "Water boosts energy naturally"
            }
        };
        }

        public async Task<List<CaffeineGuideDto>> GetGuideAsync()
        {
            return new List<CaffeineGuideDto>
    {
        new CaffeineGuideDto
        {
            Range = "0-200mg",
            Level = "Mild boost",
            Description = "1-2 cups coffee"
        },
        new CaffeineGuideDto
        {
            Range = "200-400mg",
            Level = "Moderate energy",
            Description = "2-4 cups coffee"
        },
        new CaffeineGuideDto
        {
            Range = "400mg+",
            Level = "Too much",
            Description = "May cause jitters"
        }
    };
        }

        public async Task<List<PreWorkoutCaffeineDto>> GetPreWorkoutAsync()
        {
            return new List<PreWorkoutCaffeineDto>
    {
        new PreWorkoutCaffeineDto
        {
            Title = "Good",
            Time = "Morning/Early Afternoon",
            Description = "Consume 30-60 minutes before workout for peak performance",
            Type = "Good"
        },
        new PreWorkoutCaffeineDto
        {
            Title = "Caution",
            Time = "Evening Workouts",
            Description = "Avoid caffeine to ensure better sleep quality",
            Type = "Caution"
        }
    };
        }

        public async Task<List<CaffeineReferenceDto>> GetReferenceAsync()
        {
            return new List<CaffeineReferenceDto>
    {
        new CaffeineReferenceDto
        {
            Name = "Regular Coffee",
            Amount = "~95mg per cup",
            Icon = "☕"
        },
        new CaffeineReferenceDto
        {
            Name = "Green Tea",
            Amount = "~25mg per cup",
            Icon = "🍵"
        },
        new CaffeineReferenceDto
        {
            Name = "Energy Drink",
            Amount = "~80-150mg per can",
            Icon = "🥤"
        },
        new CaffeineReferenceDto
        {
            Name = "Dark Chocolate",
            Amount = "~12mg per oz",
            Icon = "🍫"
        }
    };
        }
    }
}
