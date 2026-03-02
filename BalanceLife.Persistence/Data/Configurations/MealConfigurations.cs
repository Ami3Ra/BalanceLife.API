using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.MealModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceLife.Persistence.Data.Configurations
{
    public class MealConfigurations : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(M => M.Name)
                   .HasMaxLength(200);

            builder.Property(M => M.PictureUrl)
                   .HasMaxLength(200);

            builder.Property(M => M.Calories)
                   .HasPrecision(6, 2); // decimal(6,2) 

            builder.Property(M => M.Protein)
                   .HasPrecision(5, 2); // decimal(5,2)

            builder.Property(M => M.Carbs)
                   .HasPrecision(5, 2);

            builder.Property(M => M.Fats)
                   .HasPrecision(5, 2);

            builder.Property(M => M.IsRecommended)
                   .IsRequired();
        }
    }
}
