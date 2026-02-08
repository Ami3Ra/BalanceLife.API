using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.MealModule;
using Microsoft.EntityFrameworkCore;

namespace BalanceLife.Persistence.Data.DbContexts
{
    public class BalanceDbContext:DbContext
    {
        public BalanceDbContext(DbContextOptions<BalanceDbContext> options):base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
