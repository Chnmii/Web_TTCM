using Microsoft.AspNetCore.Mvc;

namespace QLBanCay.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
