using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class LoginController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
