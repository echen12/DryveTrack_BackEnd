using Microsoft.AspNetCore.Mvc;

namespace DryveTrack_BackEnd.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
