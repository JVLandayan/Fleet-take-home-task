using Fleet.context.Models;

namespace Fleet.dto;

public class VehicleDto
{
    public class VehicleResponses
    {


        public class ReadVehicle
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public VehicleType Type { get; set; }
            public Location? LastKnownLocation { get; set; }
        }
    }
    public class VehicleRequests
    {
        public class GetVehicle
        {
            public int? FleetId { get; set; }
        }
        public class UpdateVehicle
        {
            public int? VehicleId { get; set; }
            public string? Name { get; set; }
            public VehicleType? Type { get; set; }
            public Location Location { get; set; }
        }
    }




}