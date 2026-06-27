using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.OnboardingModule;

namespace BalanceLife.Services.Specifications.OnboardingSpecifications
{
    public class UserProfileSpecification : BaseSpecifications<UserProfile, int>
    {
        public UserProfileSpecification(string userId)
            : base(p => p.UserId == userId)
        {
        }
    }
}
