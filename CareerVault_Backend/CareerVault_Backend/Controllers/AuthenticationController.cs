using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
