using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Services.Abstraction
{
    public interface IAICoachService
    {
        Task<string> AskAsync(
            string userId,
            string userName,
            string message);
    }
}
