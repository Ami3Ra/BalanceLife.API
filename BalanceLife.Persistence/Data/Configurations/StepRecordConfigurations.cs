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
    public class StepRecordConfigurations : IEntityTypeConfiguration<StepRecord>
    {
        public void Configure(EntityTypeBuilder<StepRecord> builder)
        {
            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.Distance)
                   .HasPrecision(6, 2);
        }
    }
}
