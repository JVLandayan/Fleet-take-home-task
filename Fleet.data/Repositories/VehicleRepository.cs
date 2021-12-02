using Fleet.context;
using Fleet.context.Models;
using Fleet.data.Interfaces;

namespace Fleet.data.Repositories;

public class VehicleRepository : BaseRepository<Vehicle>,IVehicleRepository
{
    public VehicleRepository(FleetContext context): base(context)
    {
        
    }
}