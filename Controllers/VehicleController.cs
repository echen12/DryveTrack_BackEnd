using DryveTrack_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace DryveTrack_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DryveTrackAPIDBContext _dbContext;
        public VehicleController(DryveTrackAPIDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetVehicles()
        {
            return Ok(_dbContext.Vehicle.ToList());
        }
    }
}
