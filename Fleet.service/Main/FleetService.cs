using Fleet.data.Interfaces;
using Fleet.dto;
using Fleet.uow;

namespace Fleet.service.Main;



public interface IFleetService
{
    IEnumerable<FleetResponse.ReadFleet> GetFleets();
}
public class FleetService : IFleetService
{
    private readonly IFleetRepository _fleetRepository;
    private readonly IUnitOfWork _uow;

    public FleetService(IFleetRepository fleetRepository, IUnitOfWork uow)
    {
        _fleetRepository = fleetRepository;
        _uow = uow;
    }
    public IEnumerable<FleetResponse.ReadFleet> GetFleets()
    {
        var fleets = _fleetRepository.GetAll();
        var response = fleets.Select(f => new FleetResponse.ReadFleet
            {
                Id = f.Id,
                Name = f.Name
            });

        return response;
    }
}