using Fleet.context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.context.EntityConfigs;

public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(v => v.Id).ValueGeneratedOnAdd();
        builder.Property(v => v.Name).IsRequired(true);
        builder.Property(v => v.Type).HasConversion<string>();

        builder.HasData(new Vehicle
        {
            Id = 1,
            Name = "Truck#1",
            Type = VehicleType.Truck
        });
    }
}