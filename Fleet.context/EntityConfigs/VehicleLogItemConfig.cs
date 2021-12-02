using Fleet.context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.context.EntityConfigs;

public class VehicleLogItemConfig : IEntityTypeConfiguration<VehicleLogItem>
{
    public void Configure(EntityTypeBuilder<VehicleLogItem> builder)
    {
        builder.Property(l => l.Id).ValueGeneratedOnAdd();
        builder.OwnsOne(l => l.Location);
    }
}