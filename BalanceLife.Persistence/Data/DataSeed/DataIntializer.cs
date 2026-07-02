using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Domain.Entities.SportModule;
using BalanceLife.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BalanceLife.Persistence.Data.DataSed
{
    public class DataIntializer : IDataIntializer
    {
        private readonly BalanceDbContext _dbContext;

        public DataIntializer(BalanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task IntializeAsync()
        {
            try
            {
                var hasMeals = await _dbContext.Meals.AnyAsync();
                var hasRestaurants = await _dbContext.Restaurants.AnyAsync();
                var hasWorkouts = await _dbContext.Workouts.AnyAsync();
                var hasVideos = await _dbContext.WorkoutVideos.AnyAsync();
                var hasBreathing = await _dbContext.BreathingExercises.AnyAsync();

                if (hasMeals && hasRestaurants && hasWorkouts && hasVideos && hasBreathing)
                    return;

                if (!hasMeals)
                {
                  await  SeedDataFromJson<Meal, int>("meals.json", _dbContext.Meals);
                }

                if (!hasRestaurants)
                {
                  await  SeedDataFromJson<Restaurant, int>("restaurants.json", _dbContext.Restaurants);
                }

                if (!hasWorkouts)
                {
                    await SeedDataFromJson<Workout, int>("workouts.json", _dbContext.Workouts);
                }

                if (!hasVideos)
                {
                    await SeedDataFromJson<WorkoutVideo, int>("workout-videos.json", _dbContext.WorkoutVideos);
                }

                if (!hasBreathing)
                {
                    await SeedDataFromJson<BreathingExercise, int>("breathing-exercises.json", _dbContext.BreathingExercises);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured during data intialization: {ex}");
            }
        }

        private async Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = Path.Combine(basePath, "Data", "DataSeed", "JsonFiles", fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Json file not found at: {filePath}", filePath);

            try
            {
                using var dataStream = File.OpenRead(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                options.Converters.Add(new JsonStringEnumConverter());

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, options);

                if (data is not null && data.Any())
                {
                    await dbset.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While reading data from Json: {ex.Message}");
            }
        }


    }
}
