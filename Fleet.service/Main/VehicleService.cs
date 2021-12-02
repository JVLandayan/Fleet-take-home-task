using Fleet.context.Models;
using Fleet.data.Interfaces;
using Fleet.service.Third_Party;
using Fleet.uow;
using Microsoft.AspNetCore.Hosting;
using static Fleet.dto.VehicleDto;

namespace Fleet.service.Main;

public interface IVehicleService
{
    //Task<GetVehiclesResponse> GetVehiclesAsync(GetVehiclesRequest request);

    //Task<UpdateVehicleLogsResponse> UpdateVehicleLogsAsync(UpdateVehicleLogsRequest request);

    IEnumerable<VehicleResponses.ReadVehicle> GetVehicles(VehicleRequests.GetVehicle payload);
    void UpdateVehicleLogs(string fileName);

}

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleLogItemRepository _vehicleLogItemRepository;
    private readonly IUnitOfWork _uow;
    private readonly ICsvParser _csvParser;
    private readonly IWebHostEnvironment _env;

    public VehicleService(IVehicleRepository vehicleRepository, 
        IVehicleLogItemRepository vehicleLogItemRepository, 
        IUnitOfWork uow, ICsvParser csvParser, IWebHostEnvironment environment)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleLogItemRepository = vehicleLogItemRepository;
        _uow = uow;
        _csvParser = csvParser;
        _env = environment;
    }

    public IEnumerable<VehicleResponses.ReadVehicle> GetVehicles(VehicleRequests.GetVehicle payload)
    {
        if (payload.FleetId.HasValue)
        {
            return GetVehiclesByFleetId(payload.FleetId.Value);
        }

        var vehicles = _vehicleRepository.GetAll().Select(v => new VehicleResponses.ReadVehicle()
        {
            Id = v.Id,
            Name = v.Name,
            LastKnownLocation = v.Log?.LastOrDefault()?.Location,
            Type = v.Type
        });



        return vehicles;

    }

    public void UpdateVehicleLogs(string fileName)
    {

        IEnumerable<VehicleRequests.UpdateVehicle> payload = _csvParser.CsvToJson(fileName);
        var vehicles = new Dictionary<int, Vehicle>();

        foreach (var update in payload)
        {
            Vehicle vehicle;
            if (update.VehicleId.HasValue)
            {
                if (!vehicles.ContainsKey(update.VehicleId.Value))
                {
                    vehicle = _vehicleRepository.Get(update.VehicleId.Value);
                    vehicles.Add(update.VehicleId.Value, vehicle);
                }
                else
                {
                    vehicle = vehicles[update.VehicleId.Value];
                }
            }

            else if (!string.IsNullOrEmpty(update.Name) && update.Type.HasValue)
            {
                vehicle = new Vehicle
                {
                    Name = update.Name,
                    Type = update.Type.Value,
                    Log = new List<VehicleLogItem>(),
                    VehicleFleets = new List<VehicleFleet>()
                };

                _vehicleRepository.Add(vehicle);
            }
            else
            {
                // No vehicle ID, and no name and type, so we just skip since we don't know what this is
                continue;
            }

            var vehicleLogItem = new VehicleLogItem
            {
                Vehicle = vehicle,
                Location = update.Location
            };

            _vehicleLogItemRepository.Add(vehicleLogItem);
            _uow.Commit();

        }

    }

    private IEnumerable<VehicleResponses.ReadVehicle> GetVehiclesByFleetId(int? fleetId)
    {
        var vehicles = _vehicleRepository.Find(v => v.VehicleFleets.Any(vf => vf.FleetId == fleetId));

        IEnumerable<VehicleLogItem> logList = _vehicleLogItemRepository.GetAll();
        var viewModels = vehicles.Select(v => new VehicleResponses.ReadVehicle
        {
            Id = v.Id,
            Name = v.Name,
            Type = v.Type,
            LastKnownLocation = v.Log?.LastOrDefault()?.Location
        });

        foreach (var data in viewModels)
        {
            data.LastKnownLocation = logList.LastOrDefault(l => l.VehicleId == data.Id)?.Location;
        }

        return viewModels;
    }
}