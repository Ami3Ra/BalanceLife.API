using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BalanceLife.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            ;
        }
        public async Task IntializeAsync()
        {
            try
            {

                if(!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Amira Amr",
                        UserName = "AmiraAmr",
                        Email = "AmiraAmr@gmail.com",
                        PhoneNumber = "01062938156"
                    };
                    var User02 = new ApplicationUser
                    {
                        DisplayName = "Ahmed Khaled",
                        UserName = "AhmedKhaled",
                        Email = "AhmedKhaled@gmail.com",
                        PhoneNumber = "01062038156"
                    };
                    await _userManager.CreateAsync(User01,"P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");


                    await _userManager.AddToRoleAsync(User01,"SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Error While Seeding Database,{ex.Message} happened");
            }
        }
    }
}
