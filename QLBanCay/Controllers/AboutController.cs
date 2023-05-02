using Microsoft.AspNetCore.Mvc;

namespace QLBanCay.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
