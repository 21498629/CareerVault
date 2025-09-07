using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
