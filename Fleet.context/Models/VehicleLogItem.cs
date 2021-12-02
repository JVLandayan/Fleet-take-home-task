namespace Fleet.context.Models;

public class VehicleLogItem
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public Location Location { get; set; }
}