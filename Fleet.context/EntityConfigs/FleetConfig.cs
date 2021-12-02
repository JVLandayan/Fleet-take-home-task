using Fleet.context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.context.EntityConfigs;

public class FleetConfig : IEntityTypeConfiguration<Models.Fleet>
{
    public void Configure(EntityTypeBuilder<Models.Fleet> builder)
    {
        builder.Property(f => f.Id).ValueGeneratedOnAdd();
    }
}