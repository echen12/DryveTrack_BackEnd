using DryveTrack_BackEnd.Data;
using DryveTrack_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DryveTrack_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly DryveTrackAPIDBContext _dbContext;
        public InsuranceController(DryveTrackAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetInsurance()
        {
            return Ok(_dbContext.Insurance.ToList());
        }


        [HttpGet]
        [Route("get-insurance-by-vehicle-owner/{id}/{vin}")]
        public IActionResult GetVehicleInsuranceByOwner(Guid id, String vin)
        {
            var res = _dbContext.UserVehicle.Where(u => u.User == id).ToList();
            

            var test = (from o in res
                        join i in _dbContext.Vehicle
                        on o.Vehicle equals i.VehicleId
                        select new
                        {
                            VehicleId = i.VehicleId,
                            VIN = i.VIN,
                            Make = i.Make,
                            Model = i.Model,
                            Color = i.Color,
                            ModelYear = i.ModelYear,
                            VehicleType = i.VehicleType
                        });

            var specificVehicle = test.FirstOrDefault(v => v.VIN == vin);

            var a = _dbContext.VehicleInsurances.Where(i => i.VehicleId == specificVehicle.VehicleId).ToList();

            var b = _dbContext.Insurance.Where(i => i.InsuranceId == a[0].InsuranceId);



            return Ok(b);
        }

        [HttpPost]
        [Route("add-insurance-by-vehicle-owner/{id}/{vin}")]
        public IActionResult AddInsurancePlateProviderByOwner(Guid id, String vin, Insurance insurance)
        {
            var res = _dbContext.UserVehicle.Where(u => u.User == id).ToList();
            var newId = Guid.NewGuid();

            var test = (from o in res
                        join i in _dbContext.Vehicle
                        on o.Vehicle equals i.VehicleId
                        select new
                        {
                            VehicleId = i.VehicleId,
                            VIN = i.VIN,
                            Make = i.Make,
                            Model = i.Model,
                            Color = i.Color,
                            ModelYear = i.ModelYear,
                            VehicleType = i.VehicleType
                        });

            var specificVehicle = test.FirstOrDefault(v => v.VIN == vin);

            var newInsurance = new Insurance
            {
                InsuranceId= newId,
                LicensePlate = insurance.LicensePlate,
                InsuranceExpiryDate = insurance.InsuranceExpiryDate,
                InsuranceProvider = insurance.InsuranceProvider
            };

            var newVehicleInsurance = new VehicleInsurance
            {
                VehicleId = specificVehicle.VehicleId,
                InsuranceId = newId,
            };

            _dbContext.VehicleInsurances.Add(newVehicleInsurance);
            _dbContext.Insurance.Add(newInsurance);
            _dbContext.SaveChanges();



            return Ok(specificVehicle);
        }


        [HttpPatch]
        [Route("update-insurance/{id}")]
        public IActionResult UpdateInsurance(String id, [FromBody] Insurance insurance)
        {
            var result = _dbContext.Insurance.Find(new Guid(id));
            if (result != null)
            {
                result.LicensePlate = insurance.LicensePlate;
                result.InsuranceExpiryDate = insurance.InsuranceExpiryDate;
                result.InsuranceProvider = insurance.InsuranceProvider;
            }

            _dbContext.SaveChanges();
            return Ok(id);
        }

        [HttpDelete]
        [Route("delete-insurance/{id}")]
        public IActionResult DeleteInsurance(String id)
        {
            var insurance = _dbContext.Insurance.Find(new Guid(id));
            var vehicleInsurance = _dbContext.VehicleInsurances.Where(v => v.InsuranceId == new Guid(id)).First();
            _dbContext.Insurance.Remove(insurance);
            _dbContext.VehicleInsurances.Remove(vehicleInsurance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}

