using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class ApplicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
