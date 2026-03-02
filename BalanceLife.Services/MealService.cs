using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Services.Abstraction;
using BalanceLife.Services.Exceptions;
using BalanceLife.Services.Specifications.MealSpecifications;
using BalanceLife.Shared;
using BalanceLife.Shared.CommonResponses;
using BalanceLife.Shared.DTOs.MealDTOs;

namespace BalanceLife.Services
{
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MealService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<MealDTO>> GetAllMealsAsync(MealQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Meal, int>();
            var spec = new MealSpecification(queryParams);
            var meals = await repo.GetAllAsync(spec);

            var mealWithCountSpec = new MealWithCountSpecifications(queryParams); 
            var TotalCount = await repo.CountAsync(mealWithCountSpec);
            var DataToReturn = _mapper.Map<IEnumerable<MealDTO>>(meals);
            
            var countOfReturnData = DataToReturn.Count();

            return new PaginatedResult<MealDTO>(
                queryParams.PageIndex,
                countOfReturnData,
                TotalCount,
                DataToReturn
                );
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync(double userLat, double userLng)
        {
         var restaurants = await _unitOfWork.GetRepository<Restaurant, int>().GetAllAsync();

            var restaurantDTOs = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            foreach (var dto in restaurantDTOs)
            {
                var entity = restaurants.First(r => r.Name == dto.Name);

                var distance = CalculateDistance(userLat, userLng, entity.Latitude, entity.Longitude);

                dto.DistanceInMeters = Math.Round((decimal)distance, 2);
                dto.EstimatedTimeInMinutes = CalculateEstimatedTime(dto.DistanceInMeters);

                dto.DistanceFormatted = FormatDistance(dto.DistanceInMeters);
            }

          
            return restaurantDTOs.OrderBy(r => r.DistanceInMeters);
        }

        public async Task<Result<MealDTO>> GetMealByIdAsync(int id)
        {
            var spec = new MealSpecification(id);
            var meal = await _unitOfWork.GetRepository<Meal, int>().GetByIdAsync(spec);

            if(meal is null)
                return 
                    Error.NotFound($"Meal Not Found",$"Meal With this Id: {id} is not found");

            return _mapper.Map<MealDTO>(meal);
        }

        #region Helper Method

        private double CalculateDistance(double userLat, double userLng, double restaurantLat, double restaurantLng)
        {
            var R = 6371000;
            var dLat = ToRadians(restaurantLat - userLat);
            var dLon = ToRadians(restaurantLng - userLng);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(userLat)) * Math.Cos(ToRadians(restaurantLat)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private int CalculateEstimatedTime(decimal distanceInMeters, decimal speedMetersPerMinute = 80)
        {
            return (int)Math.Ceiling(distanceInMeters / speedMetersPerMinute);
        }

        private double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

        private string FormatDistance(decimal distanceInMeters)
        {
            if (distanceInMeters < 1000)
                return $"{Math.Round(distanceInMeters, 0)} m";

            return $"{Math.Round(distanceInMeters / 1000, 1)} km";
        }


        #endregion
    }
}
