using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	public class HomeAdminController : Controller
	{
		QlbanCayContext csdl = new QlbanCayContext();
		[Route("")]
		[Route("index")]
		public IActionResult Index()
		{
			return View();
		}

        [Route("TimKiem")]
        public IActionResult TimKiem(int? page, string SearchString)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = csdl.DanhMucCays.AsNoTracking().Where(x => x.TenCay.Contains(SearchString));
            PagedList<DanhMucCay> list = new PagedList<DanhMucCay>(lstsanpham, pageNumber, pageSize);
            ViewBag.SearchString = SearchString;
            return View(list);
        }
    }
}
