using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Domain.Entities.WaterModule;
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
        public DbSet<WaterIntake> WaterIntakes { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<ActivitySession> ActivitySessions { get; set; }
        public DbSet<WorkoutVideo> WorkoutVideos { get; set; }
        public DbSet<BreathingExercise> BreathingExercises { get; set; }

    }
}
