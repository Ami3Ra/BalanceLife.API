using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.MealModule
{
    public class Meal:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
        public bool IsRecommended { get; set; }

    }
}
