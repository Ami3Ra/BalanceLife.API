using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.DashboardDtos
{
    public class DashboardDto
    {
        public string UserName { get; set; } = null!;

        public DateTime Date { get; set; }

        public CaloriesDto Calories { get; set; } = null!;

        public WaterDto Water { get; set; } = null!;

        public ActivityDto Activity { get; set; } = null!;

        public string HealthTip { get; set; } = null!;

        public List<TimelineItemDto> Timeline { get; set; }


    }
}
