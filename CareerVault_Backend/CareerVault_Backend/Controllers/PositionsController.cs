using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class PositionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
