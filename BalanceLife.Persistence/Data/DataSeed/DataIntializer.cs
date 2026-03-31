using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities;
using BalanceLife.Domain.Entities.MealModule;
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

                if (hasMeals && hasRestaurants)
                    return;

                if (!hasMeals)
                {
                  await  SeedDataFromJson<Meal, int>("meals.json", _dbContext.Meals);
                }

                if (!hasRestaurants)
                {
                  await  SeedDataFromJson<Restaurant, int>("restaurants.json", _dbContext.Restaurants);
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

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

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
