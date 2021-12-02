using Fleet.dto;
using Fleet.service;
using Fleet.service.Main;
using Fleet.service.Third_Party;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IUploadService _uploadService;

        public VehiclesController(IVehicleService vehicleService, IUploadService uploadService)
        {
            _vehicleService = vehicleService;
            _uploadService = uploadService;
        }

        [HttpGet] 
        public ActionResult<VehicleDto.VehicleResponses> GetVehicles([FromQuery] VehicleDto.VehicleRequests.GetVehicle request)
        {
            return Ok(_vehicleService.GetVehicles(request));
        }

        [Route("logs")]
        [HttpPost]
        public ActionResult UpdateVehicles([FromForm] FileUploads payload)
        {
            string fileName = _uploadService.SubmissionFiles(payload);
            _vehicleService.UpdateVehicleLogs(fileName);
            return NoContent();
        }

        [HttpGet("files")]
        public ActionResult<IEnumerable<string>> GetAllUploads()
        {
            var fileNameList = _uploadService.GetUploadedFiles();
            return Ok(fileNameList);
        }

        [HttpGet("logs/recent")]
        public ActionResult<IEnumerable<VehicleDto.VehicleRequests.UpdateVehicle>> GetRecentUploadData()
        {
            var uploadData = _uploadService.GetRecentUploadData();
            return Ok(uploadData);
        }
    }
}