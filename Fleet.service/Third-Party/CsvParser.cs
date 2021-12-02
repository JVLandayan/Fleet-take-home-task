using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Fleet.context.Models;
using Fleet.data.Interfaces;
using Fleet.dto;
using Microsoft.AspNetCore.Hosting;

namespace Fleet.service.Third_Party;

public interface ICsvParser
{
    public IEnumerable<VehicleDto.VehicleRequests.UpdateVehicle> CsvToJson(string fileName);
}

public class CsvParser : ICsvParser
{
    private readonly IWebHostEnvironment _env;
    private readonly IVehicleRepository _vehicleRepository;

    public CsvParser(IWebHostEnvironment webHostEnvironment, IVehicleRepository vehicleRepository)
    {
        _env = webHostEnvironment;
        _vehicleRepository = vehicleRepository;
    }

    public IEnumerable<VehicleDto.VehicleRequests.UpdateVehicle> CsvToJson(string fileName)
    {
        var vehicleList = _vehicleRepository.GetAll().ToList();
        var updateList = new List<VehicleDto.VehicleRequests.UpdateVehicle>();
        using (var streamReader = new StreamReader(_env.ContentRootPath + "\\files\\" + fileName))
        {
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<CsvHeaderMap>();
                var records = Enumerable.ToList(csvReader.GetRecords<CsvHeader>());
                foreach (var record in records)
                {
                    updateList.Add(new VehicleDto.VehicleRequests.UpdateVehicle()
                    {
                        VehicleId = record.VehicleId,
                        Name = vehicleList.First(v=>v.Id == record.VehicleId).Name,
                        Type = vehicleList.First(v => v.Id == record.VehicleId).Type,
                        Location = new Location()
                        {
                            Latitude = record.LocationLat,
                            Longitude = record.LocationLong,
                            Timestamp = DateTime.Parse(record.TimeStamp)
                        }
                    });
                }
            }
        }
        return updateList;

    }

    #region CSV Helper Classes

    public class CsvHeader
    {
        public int VehicleId { get; set; }
        public Double LocationLat { get; set; }
        public Double LocationLong { get; set; }
        public string TimeStamp { get; set; }
    }

    public class CsvHeaderMap : ClassMap<CsvHeader>
    {
        public CsvHeaderMap()
        {
            Map(m => m.VehicleId).Name("VehicleId");
            Map(m => m.LocationLat).Name("LocationLat");
            Map(m => m.LocationLong).Name("LocationLong");
            Map(m => m.TimeStamp).Name("TimeStamp");
        }
    }

    #endregion


}