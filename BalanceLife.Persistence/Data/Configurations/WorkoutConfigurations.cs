using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BalanceLife.Domain.Entities.SportModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceLife.Persistence.Data.Configurations
{
    public class WorkoutConfigurations : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.Property(w => w.Name)
                   .HasMaxLength(200);

            builder.Property(w => w.Category)
                   .HasMaxLength(100);

            builder.Property(w => w.Level)
                   .HasConversion<string>(); 

            builder.Property(w => w.DurationInMinutes)
                   .IsRequired();

            builder.Property(w => w.Calories)
                   .IsRequired();
        }
    }
}