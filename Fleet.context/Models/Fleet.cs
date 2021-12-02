namespace Fleet.context.Models;

public class Fleet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<VehicleFleet> VehicleFleets { get; set; }
}