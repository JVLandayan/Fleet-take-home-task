using Fleet.context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.context.EntityConfigs;

public class VehicleFleetConfig : IEntityTypeConfiguration<VehicleFleet>
{
    public void Configure(EntityTypeBuilder<VehicleFleet> builder)
    {
        builder.HasKey(vf => new { vf.VehicleId, vf.FleetId });
        builder.HasOne(vf => vf.Vehicle).WithMany(v => v.VehicleFleets).HasForeignKey(vf => vf.VehicleId);
        builder.HasOne(vf => vf.Fleet).WithMany(f => f.VehicleFleets).HasForeignKey(vf => vf.FleetId);
    }
}