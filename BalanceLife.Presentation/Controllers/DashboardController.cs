using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class DashboardController : ApiBaseController
    {
            private readonly IDashboardService _dashboardService;

            public DashboardController(IDashboardService dashboardService)
            {
                _dashboardService = dashboardService;
            }
        // ================= GET DASHBOARD =================
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var userName =
                User.FindFirstValue(ClaimTypes.Name) ??
                User.FindFirstValue(ClaimTypes.Email) ??
                "User";

            var result = await _dashboardService.GetDashboardAsync(userId, userName);

            return Ok(result);
        }
    }
}
