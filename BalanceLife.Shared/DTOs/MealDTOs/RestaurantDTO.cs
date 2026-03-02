using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.MealDTOs
{
    public class RestaurantDTO
    {
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public double Rating { get; set; }

        public decimal DistanceInMeters { get; set; }
        public string DistanceFormatted { get; set; } = default!;
        public int EstimatedTimeInMinutes { get; set; }
    }
}
