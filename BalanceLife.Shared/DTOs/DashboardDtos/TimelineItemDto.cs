using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.DashboardDtos
{
    public class TimelineItemDto
    {
        public string Time { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
