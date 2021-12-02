using Fleet.context;
using Fleet.data.Interfaces;

namespace Fleet.data.Repositories;

public class FleetRepository: BaseRepository<context.Models.Fleet>, IFleetRepository
{
    public FleetRepository(FleetContext context) :base(context)
    {
        
    }
}