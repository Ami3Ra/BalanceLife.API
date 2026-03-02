using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Shared.DTOs.MealDTOs;

namespace BalanceLife.Services.MappingProfiles
{
    internal class MealProfile:Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, MealDTO>()
                .ForMember(dest => dest.PictureUrl,opt=>opt.MapFrom<MealPictureUrlResolver>());

            CreateMap<Restaurant, RestaurantDTO>()
              .ForMember(dest => dest.DistanceInMeters, opt => opt.Ignore())
              .ForMember(dest => dest.EstimatedTimeInMinutes, opt => opt.Ignore());
        }
    }
}
