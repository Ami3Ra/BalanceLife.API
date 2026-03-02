using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Contracts
{
    public interface IDataIntializer
    {
        Task IntializeAsync();
    }
}
