using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.MealModule
{
    public class Restaurant:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
