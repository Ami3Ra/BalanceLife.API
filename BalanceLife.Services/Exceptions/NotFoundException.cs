using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Services.Exceptions
{
    public abstract class NotFoundException(string message):Exception(message)
    {
       
    }

    public sealed class MealNotFoundException(int id)
        : NotFoundException($"Meal with Id: {id} is Not Found") { }
}
