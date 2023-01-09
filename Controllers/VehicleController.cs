using DryveTrack_BackEnd.Data;
using DryveTrack_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

        
        [HttpGet]
        [Route("get-vehicles-by-owner/{id}")]
        public IActionResult GetVehiclesByOwner(Guid id)
        {
            var res = _dbContext.UserVehicle.Where(u => u.User == id).ToList();

            var test = (from o in res
                        join i in _dbContext.Vehicle
                        on o.Vehicle equals i.VehicleId
                        select new
                        {
                            VIN = i.VIN,
                            Make = i.Make,
                            Model = i.Model,
                            Color = i.Color,
                            ModelYear = i.ModelYear,
                            VehicleType = i.VehicleType
                        });

            return Ok(test);
        }

        [HttpGet]
        [Route("get-vehicle-by-owner-vin/{id}/{vin}")]
        public IActionResult GetVehicleByOwner(Guid id, String vin)
        {
            var arlist = new ArrayList();

            var res = _dbContext.UserVehicle.Where(u => u.User == id).ToList();

            var test = (from o in res
                       join i in _dbContext.Vehicle
                       on o.Vehicle equals i.VehicleId
                       select new
                       {
                           VIN = i.VIN,
                           Make = i.Make,
                           Model = i.Model,
                           Color = i.Color,
                           ModelYear = i.ModelYear,
                           VehicleType = i.VehicleType
                       });

            var specificVehicle = test.FirstOrDefault(v => v.VIN == vin);

            return Ok(specificVehicle);
        }

        [HttpPost]
        [Route("add-vehicle-by-owner/{id}")]
        public IActionResult AddVehicleByOwner(String id, Vehicle vehicle)
        {
            var userExists = _dbContext.Users.Any(u => u.UserId == new Guid(id));
            var vehicleId = Guid.NewGuid();

            if (userExists == true)
            {
                var newVehicle = new Vehicle
                {
                    VehicleId = vehicleId,
                    VIN = vehicle.VIN,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Color = vehicle.Color,
                    VehicleType = vehicle.VehicleType,
                    ModelYear = vehicle.ModelYear,
                    //Odometer = vehicle.Odometer
                };

                var newUserVehicle = new UserVehicle
                {
                    User = new Guid(id),
                    Vehicle = vehicleId
                };

                _dbContext.Vehicle.Add(newVehicle);
                _dbContext.UserVehicle.Add(newUserVehicle);
                _dbContext.SaveChanges();

                return Ok(newUserVehicle);

            }

            return BadRequest();
        }


        [HttpPatch]
        [Route("update-vehicle/{id}")]
        public IActionResult UpdateVehicle(String id, [FromBody] Vehicle vehicle)
        {
            var result = _dbContext.Vehicle.Find(new Guid(id));
            if (result != null)
            {
                result.Make = vehicle.Make;
                result.Model = vehicle.Model;
                result.Color= vehicle.Color;
                //result.Odometer = vehicle.Odometer;
            }

            _dbContext.SaveChanges();
            return Ok(id);
        }



        [HttpDelete]
        [Route("delete-vehicle/{id}")]
        public IActionResult DeleteVehicle(String id)
        {
            var vehicle = _dbContext.Vehicle.Find(new Guid(id));
            var user = _dbContext.UserVehicle.Where(v => v.Vehicle == new Guid(id)).First();
            _dbContext.Vehicle.Remove(vehicle);
            _dbContext.UserVehicle.Remove(user);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
