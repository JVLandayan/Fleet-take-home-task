using Fleet.dto;
using Fleet.service;
using Fleet.service.Main;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IFleetService _fleetService;
        public FleetsController(IFleetService fleetService)
        {
            _fleetService = fleetService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FleetResponse.ReadFleet>> GetFleets()
        {
            return Ok(_fleetService.GetFleets());
        }
        
    }
}