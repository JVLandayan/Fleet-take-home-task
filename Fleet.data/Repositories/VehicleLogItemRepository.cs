using Fleet.context;
using Fleet.context.Models;
using Fleet.data.Interfaces;

namespace Fleet.data.Repositories;

public class VehicleLogItemRepository: BaseRepository<VehicleLogItem>, IVehicleLogItemRepository
{
    public VehicleLogItemRepository(FleetContext context):base(context)
    {
        
    }
}