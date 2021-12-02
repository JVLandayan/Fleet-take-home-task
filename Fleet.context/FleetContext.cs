using Fleet.context.EntityConfigs;
using Fleet.context.Models;
using Microsoft.EntityFrameworkCore;

namespace Fleet.context;

public class FleetContext : DbContext
{
    public FleetContext(DbContextOptions<FleetContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleFleet> VehicleFleets { get; set; }
    public DbSet<Models.Fleet> Fleets { get; set; }
    public DbSet<VehicleLogItem> VehicleLogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new VehicleConfig());
        builder.ApplyConfiguration(new FleetConfig());
        builder.ApplyConfiguration(new VehicleFleetConfig());
        builder.ApplyConfiguration(new VehicleLogItemConfig());
    }
}